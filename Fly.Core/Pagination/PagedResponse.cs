namespace Fly.Core.Pagination;

public class PagedResponse<T> : Response<T>
{
    public MetaData MetaData { get; set; }
    public PagedResponse(T items, int count, Page page) : base(items)
    {
        MetaData = new MetaData
        {
            TotalCount = count,
            PageSize = page.PageSize,
            CurrentPage = page.PageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)page.PageSize)
        };
    }

    public PagedResponse(T items, MetaData metaData) : base(items)
    {
        MetaData = metaData;
    }
}
