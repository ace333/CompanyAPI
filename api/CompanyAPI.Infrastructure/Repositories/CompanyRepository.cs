using CompanyAPI.Domain.Entities;
using CompanyAPI.Infrastructure.Context;
using CompanyAPI.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly CompanyDbContext _companyContext;

        public CompanyRepository(CompanyDbContext context) 
            : base(context)
        {
            _companyContext = context;
        }

        public IQueryable<Company> GetAll()
        {
            return _companyContext.Companies.AsQueryable();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _companyContext.Companies.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company> GetByIsinAsync(string isin)
        {
            return await _companyContext.Companies.SingleOrDefaultAsync(c => c.Isin == isin);
        }
    }
}
