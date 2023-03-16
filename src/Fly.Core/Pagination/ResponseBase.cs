namespace Fly.Core.Pagination;

public class ResponseBase<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }

    public ResponseBase(T data)
    {
        Succeeded = true;
        Message = string.Empty;
        Data = data;
    }
}
