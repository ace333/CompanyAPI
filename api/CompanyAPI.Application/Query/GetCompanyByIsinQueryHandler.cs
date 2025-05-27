using CompanyAPI.Application.Dto;
using CompanyAPI.Infrastructure.Repositories;
using MediatR;

namespace CompanyAPI.Application.Query
{
    public sealed class GetCompanyByIsinQueryHandler : IRequestHandler<GetCompanyByIsinQuery, CompanyDto>
    {
        private readonly ICompanyRepository _repository;

        public GetCompanyByIsinQueryHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanyDto> Handle(GetCompanyByIsinQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Query is null");
            }

            if(string.IsNullOrEmpty(request.Isin))
            {
                throw new ArgumentNullException("Isin cannot be empty");
            }

            var result = await _repository.GetByIsinAsync(request.Isin);

            if(result == null)
            {
                return CompanyDto.Empty();
            }

            return new CompanyDto(result.Id, result.Isin, result.Name, result.Exchange, result.Ticker, result.Website);
        }
    }
}
