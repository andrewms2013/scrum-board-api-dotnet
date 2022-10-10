namespace ScrumBoardAPI.Models.Paging;

public class QueryParameters
{
    private int _pageSize = 15;

    public int PageSize {
        get {
            return _pageSize;
        }

        set {
            _pageSize = value;
        }
    }

    public int PageNumber { get; set; }
}
