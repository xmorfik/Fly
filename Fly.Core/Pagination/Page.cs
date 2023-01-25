namespace Fly.Core.Pagination;

public class Page
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public Page()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public Page(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;
    }
}
