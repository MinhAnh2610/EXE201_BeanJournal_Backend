namespace CleanArchitecture.Application.Common;

public class ApiResponse<T>(bool isSuccess, T? data, string message, List<Error>? errors)
{
  public bool IsSuccess { get; set; } = isSuccess;
  public T? Data { get; set; } = data;
  public string Message { get; set; } = message;
  public List<Error>? Errors { get; set; } = errors ?? [];

  public static ApiResponse<T> SuccessResponse(T? data, string message)
    => new(true, data, message, []);

  public static ApiResponse<T> FailureResponse(List<Error> errors, string message)
    => new(false, default, message, errors);
}
