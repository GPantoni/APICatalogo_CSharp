namespace APICatalogo.Pagination;

public abstract class QueryStringParameters
{
    private const int maxPageSize = 50;
    private int _pageSize = maxPageSize;
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > maxPageSize ? maxPageSize : value;
    }
}