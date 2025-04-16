using Microsoft.AspNetCore.Mvc;
using Svix;
using System.Text.Json;
using System.Text;

namespace CleanArchitecture.Presentation.Endpoints;

[Route("api/webhooks/clerk")] // Matches the URL you give to Clerk
[ApiController]
public class ClerkWebhooksController(
    IConfiguration configuration,
    IClerkSyncService clerkSyncService, // Inject the service
    ILogger<ClerkWebhooksController> logger) : ControllerBase
{
  private readonly IConfiguration _configuration = configuration;
  private readonly IClerkSyncService _clerkSyncService = clerkSyncService;
  private readonly ILogger<ClerkWebhooksController> _logger = logger;

  [HttpPost]
  public async Task<IActionResult> Post() 
  {
    // --- 1. Verify Signature (Same as before) ---
    var secret = _configuration["Clerk:WebhookSecret"];
    if (string.IsNullOrEmpty(secret))
    {
      _logger.LogError("Clerk Webhook Secret is not configured.");
      return BadRequest("Server configuration error.");
    }
    var headers = Request.Headers;
    string body;
    using (var reader = new StreamReader(Request.Body, Encoding.UTF8)) { body = await reader.ReadToEndAsync(); }
    try
    {
      var wh = new Webhook(secret);
      wh.Verify(body, (System.Net.WebHeaderCollection)headers);
      _logger.LogInformation("Clerk Webhook signature verified successfully.");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Clerk Webhook signature verification failed.");
      return BadRequest("Webhook signature verification failed.");
    }

    // --- 2. Process Verified Payload ---
    _logger.LogDebug("Raw Webhook Body: {WebhookBody}", body);

    try
    {
      using var jsonDoc = JsonDocument.Parse(body);
      if (!jsonDoc.RootElement.TryGetProperty("type", out var typeElement) || typeElement.ValueKind != JsonValueKind.String)
      {
        _logger.LogWarning("Clerk Webhook payload missing 'type' property.");
        return BadRequest("Invalid payload structure: Missing 'type'.");
      }
      if (!jsonDoc.RootElement.TryGetProperty("data", out var dataElement) || dataElement.ValueKind != JsonValueKind.Object)
      {
        _logger.LogWarning("Clerk Webhook payload missing 'data' object.");
        return BadRequest("Invalid payload structure: Missing 'data'.");
      }

      var eventType = typeElement.GetString();
      var data = dataElement;

      _logger.LogInformation("Processing verified Clerk webhook. Type: {EventType}", eventType);

      // --- 3. Delegate to Service Layer ---
      switch (eventType)
      {
        case "user.created":
          // Call the service method
          await _clerkSyncService.HandleUserCreatedAsync(data);
          break;
        case "user.updated":
          // Call the service method
          await _clerkSyncService.HandleUserUpdatedAsync(data);
          break;
        case "user.deleted":
          // Extract ID and call the service method
          if (data.TryGetProperty("id", out var deletedIdElement) && deletedIdElement.ValueKind == JsonValueKind.String)
          {
            await _clerkSyncService.HandleUserDeletedAsync(deletedIdElement.GetString()!);
          }
          else
          {
            _logger.LogWarning("Could not parse ID from user.deleted event data in controller.");
          }
          break;
        default:
          _logger.LogInformation("Ignoring Clerk webhook event type: {EventType}", eventType);
          break;
      }

      return Ok(); // Acknowledge receipt
    }
    catch (JsonException jsonEx)
    {
      _logger.LogError(jsonEx, "Failed to parse Clerk webhook JSON payload in controller.");
      return BadRequest("Invalid JSON payload.");
    }
    catch (Exception ex) // Catch exceptions potentially thrown by the service layer
    {
      _logger.LogError(ex, "Error during webhook processing delegated to service for type {EventType}",
         JsonDocument.Parse(body).RootElement.GetProperty("type").GetString() ?? "UNKNOWN");
      return Ok(); // Or return 500 if service layer indicates retriable error
    }
  }
}
