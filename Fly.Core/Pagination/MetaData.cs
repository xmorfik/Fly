namespace Fly.Core.Pagination;

public class MetaData
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public Page ToPage()
    {
        return new Page(CurrentPage, PageSize);
    }
}
