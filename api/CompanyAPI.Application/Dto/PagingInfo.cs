namespace CompanyAPI.Application.Dto
{
    public sealed class PagingInfo
    {
        public PagingInfo(int totalRecords, int limit, int offset)
        {
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)limit);
            HasPreviousPage = offset > 0;
            HasNextPage = offset + limit < totalRecords;
        }

        public int TotalRecords { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
    }
}
