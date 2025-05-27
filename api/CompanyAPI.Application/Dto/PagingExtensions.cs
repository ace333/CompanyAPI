using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Application.Dto
{
    public static class PagingExtensions
    {
        public static async Task<PaginatedList<TDto>> AsPaginatedList<TDto>(this IQueryable<TDto> queryable, int limit, int offset)
            where TDto : class
        {
            var count = queryable.Count();
            var items = await queryable
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return new PaginatedList<TDto>(items, count, limit, offset);
        }
    }
}
