namespace CompanyAPI.Application.Dto
{
    public sealed class PaginatedList<TDto> where TDto : class
    {
        public PaginatedList(IReadOnlyCollection<TDto> items, int totalRecords, int limit, int offset)
        {
            PageInfo = new PagingInfo(totalRecords, limit, offset);
            Items = items;
        }

        public PagingInfo PageInfo { get;set; }
        public IReadOnlyCollection<TDto> Items { get; set; }
    }
}
