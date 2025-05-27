using CompanyAPI.Infrastructure.Repositories;
using System.Text.RegularExpressions;

namespace CompanyAPI.Application.Validation
{
    public sealed class IsinValidator : IIsinValidator
    {
        private readonly ICompanyRepository _companyRepository;

        public IsinValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public bool ValidateFormat(string isin)
        {
            var pattern = @"^[A-Z]{2}[0-9]{10}$";
            return Regex.IsMatch(isin, pattern);
        }

        public async Task<bool> ValidateUniquenessAsync(string isin, int? id = null)
        {
            if (!string.IsNullOrEmpty(isin))
            {
                var company = await _companyRepository.GetByIsinAsync(isin);
                return !(company != null && company?.Id != id);
            }

            return true;
        }
    }
}
