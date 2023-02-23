namespace Fly.Core.Pagination;

public class Page
{
    private int _pageNumber;
    private int _pageSize;
    public int PageNumber
    {
        get => _pageNumber;
        set
        {
            _pageNumber = value < 1 ? 1 : value;
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = value < 1 ? 30 : value;
        }
    }

    public Page()
    {
        PageNumber = 1;
        PageSize = 30;
    }

    public Page(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

}
