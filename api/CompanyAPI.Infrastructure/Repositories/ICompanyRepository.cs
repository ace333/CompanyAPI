using CompanyAPI.Domain.Entities;
using CompanyAPI.Infrastructure.Core;

namespace CompanyAPI.Infrastructure.Repositories
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        IQueryable<Company> GetAll();
        Task<Company> GetByIdAsync(int id);
        Task<Company> GetByIsinAsync(string isin);
    }
}
