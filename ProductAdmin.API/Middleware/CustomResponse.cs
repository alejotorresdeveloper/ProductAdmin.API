namespace ProductAdmin.API.Middleware;

public class CustomResponse<T>
{
    private CustomResponse(int errorCode, string message, T data)
    {
        ErrorCode = errorCode;
        Message = message;
        Data = data;
    }

    public int ErrorCode { get; }
    public string Message { get; }
    public T Data { get; }

    public static CustomResponse<T> BuildSuccess(T data)
    {
        return new CustomResponse<T>(0, string.Empty, data);
    }

    public static CustomResponse<T> BuildError(int errorCode, string message, T data)
    {
        return new CustomResponse<T>(errorCode, message, data);
    }
}