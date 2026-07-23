namespace OrnivaApi.Responses
{
    public class PagedResponse<T> : ApiResponse<T>
    {
        public PaginationMetadata Pagination { get; set; }

        public PagedResponse(bool success, string message, T data, PaginationMetadata pagination) : base(success, message, data)
        {
            Pagination = pagination;
        }
    }
}