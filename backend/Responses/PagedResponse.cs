namespace WebFootballApp.Responses;

public class PagedResponse<T>
{
    public PagedResponse(T data, int totalItems, int totalPages)
    {
        Data = data;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }

    public T Data { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}