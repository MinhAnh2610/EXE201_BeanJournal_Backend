using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;
using System.Text.Json;

namespace CleanArchitecture.Application.Services;

public class ClerkSyncService : IClerkSyncService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILogger<ClerkSyncService> _logger;

  public ClerkSyncService(IUnitOfWork unitOfWork, ILogger<ClerkSyncService> logger)
  {
    _unitOfWork = unitOfWork;
    _logger = logger;
  }

  public async Task HandleUserCreatedAsync(JsonElement userData)
  {
    if (!userData.TryGetProperty("id", out var idElement) || idElement.ValueKind != JsonValueKind.String)
    {
      _logger.LogWarning("ClerkSyncService: Received user.created event without a valid 'id'.");
      return;
    }
    var clerkId = idElement.GetString()!;

    // Idempotency Check
    if (await _unitOfWork.Users.UserExistsByClerkIdAsync(clerkId))
    {
      _logger.LogWarning("ClerkSyncService: User with Clerk ID {ClerkId} already exists. Skipping creation.", clerkId);
      return;
    }

    string? email = GetPrimaryEmail(userData);
    string? username = userData.TryGetProperty("username", out var uElement) ? uElement.GetString() : null;
    // Extract other fields as needed (first_name, last_name, etc.)

    var newUser = new User
    {
      ClerkUserId = clerkId,
      Email = email!,
      Username = username,
      // Set other defaults if necessary
    };

    try
    {
      await _unitOfWork.Users.CreateAsync(newUser);
      _logger.LogInformation("ClerkSyncService: Created local user for Clerk ID {ClerkId}", clerkId);

      //await _unitOfWork.CompleteAsync();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ClerkSyncService: Error creating local user for Clerk ID {ClerkId}", clerkId);
      // Decide if you need to re-throw or handle differently
    }
  }

  public async Task HandleUserUpdatedAsync(JsonElement userData)
  {
    if (!userData.TryGetProperty("id", out var idElement) || idElement.ValueKind != JsonValueKind.String)
    {
      _logger.LogWarning("ClerkSyncService: Received user.updated event without a valid 'id'.");
      return;
    }
    var clerkId = idElement.GetString()!;

    var userToUpdate = await _unitOfWork.Users.GetUserByClerkIdAsync(clerkId);
    if (userToUpdate == null)
    {
      _logger.LogWarning("ClerkSyncService: Received user.updated for unknown Clerk ID {ClerkId}. Attempting to create.", clerkId);
      // Optional Resilience: Create if not found
      await HandleUserCreatedAsync(userData);
      return;
    }

    // Extract updated fields
    string? email = GetPrimaryEmail(userData);
    string? username = userData.TryGetProperty("username", out var uElement) ? uElement.GetString() : null;
    // Extract other fields as needed

    bool needsUpdate = false;
    if (userToUpdate.Email != email) { userToUpdate.Email = email!; needsUpdate = true; }
    if (userToUpdate.Username != username) { userToUpdate.Username = username; needsUpdate = true; }
    // ... check other synced fields ...

    if (needsUpdate)
    {
      try
      {
        _unitOfWork.Users.Update(userToUpdate);
        _logger.LogInformation("ClerkSyncService: Updated local user for Clerk ID {ClerkId}", clerkId);

        //await _unitOfWork.CompleteAsync();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "ClerkSyncService: Error updating local user for Clerk ID {ClerkId}", clerkId);
        // Decide if you need to re-throw or handle differently
      }
    }
    else
    {
      _logger.LogInformation("ClerkSyncService: Received user.updated for Clerk ID {ClerkId}, but no changes needed locally.", clerkId);
    }
  }

  public async Task HandleUserDeletedAsync(string clerkId)
  {
    if (string.IsNullOrEmpty(clerkId))
    {
      _logger.LogWarning("ClerkSyncService: Received user.deleted event with null or empty Clerk ID.");
      return;
    }

    try
    {
      // Repository implementation handles logging success/failure finding the user
      await _unitOfWork.Users.DeleteUserByClerkIdAsync(clerkId);
      _logger.LogInformation("ClerkSyncService: Attempted deletion for Clerk ID {ClerkId} (Check repository logs for details).", clerkId);

      //await _unitOfWork.CompleteAsync();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ClerkSyncService: Error deleting local user for Clerk ID {ClerkId}", clerkId);
      // Decide if you need to re-throw or handle differently
    }
  }


  // Helper to extract primary email address (same as before)
  private static string? GetPrimaryEmail(JsonElement data)
  {
    if (data.TryGetProperty("email_addresses", out var emailsElement) && emailsElement.ValueKind == JsonValueKind.Array)
    {
      string? primaryEmailId = null;
      if (data.TryGetProperty("primary_email_address_id", out var primaryIdElement) && primaryIdElement.ValueKind == JsonValueKind.String)
      {
        primaryEmailId = primaryIdElement.GetString();
      }

      if (primaryEmailId != null)
      {
        foreach (var emailElement in emailsElement.EnumerateArray())
        {
          if (emailElement.TryGetProperty("id", out var idElement) && idElement.ValueKind == JsonValueKind.String && idElement.GetString() == primaryEmailId)
          {
            if (emailElement.TryGetProperty("email_address", out var emailAddrElement) && emailAddrElement.ValueKind == JsonValueKind.String)
            {
              return emailAddrElement.GetString();
            }
          }
        }
      }
      // Optional Fallback
    }
    return null;
  }
}
