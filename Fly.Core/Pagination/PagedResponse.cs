namespace Fly.Core.Pagination;

public class PagedResponse<T> : List<T> 
{
    public MetaData MetaData { get; set; }
    public PagedResponse(List<T> items, MetaData metaData) : base(items)
    {
        MetaData = metaData;
    }
    public PagedResponse(List<T> items, int count, Page page) : base(items)
    {
        MetaData = new MetaData
        {
            TotalCount = count,
            PageSize = page.PageSize,
            CurrentPage = page.PageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)page.PageSize)
        };
    }

    public static PagedResponse<T> ToPagedList(IEnumerable<T> source, Page page)
    {
        var count = source.Count();
        var items = source
        .Skip((page.PageNumber - 1) * page.PageSize)
        .Take(page.PageSize).ToList();
        return new PagedResponse<T>(items, count, page);
    }
}
