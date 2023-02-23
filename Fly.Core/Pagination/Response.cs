namespace Fly.Core.Pagination;

public class Response<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }

    public Response(T data)
    {
        Succeeded = true;
        Message = string.Empty;
        Data = data;
    }
}
