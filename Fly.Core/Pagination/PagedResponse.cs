namespace Fly.Core.Pagination;

public class PagedResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PagedResponse(T data, Page page) : base(data)
    {
        if (page == null)
        {
            page = new Page();
        }
        PageNumber = page.PageNumber;
        PageSize = page.PageSize;
    }
}
