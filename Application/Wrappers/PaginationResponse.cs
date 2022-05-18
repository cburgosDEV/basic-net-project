namespace Application.Wrappers
{
    public class PaginationResponse<T> : CustomResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationResponse(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = string.Empty;
            Succeeded = true;
            Errors = new List<string>();
        }
    }
}
