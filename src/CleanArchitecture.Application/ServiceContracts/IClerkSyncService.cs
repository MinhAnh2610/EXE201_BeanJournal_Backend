using System.Text.Json;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IClerkSyncService
{
  /// <summary>
  /// Handles the logic for when a user.created event is received from Clerk.
  /// </summary>
  /// <param name="userData">The 'data' object from the Clerk webhook payload.</param>
  /// <returns>Task representing the asynchronous operation.</returns>
  Task HandleUserCreatedAsync(JsonElement userData);

  /// <summary>
  /// Handles the logic for when a user.updated event is received from Clerk.
  /// </summary>
  /// <param name="userData">The 'data' object from the Clerk webhook payload.</param>
  /// <returns>Task representing the asynchronous operation.</returns>
  Task HandleUserUpdatedAsync(JsonElement userData);

  /// <summary>
  /// Handles the logic for when a user.deleted event is received from Clerk.
  /// </summary>
  /// <param name="clerkId">The Clerk User ID of the deleted user.</param>
  /// <returns>Task representing the asynchronous operation.</returns>
  Task HandleUserDeletedAsync(string clerkId);
}
