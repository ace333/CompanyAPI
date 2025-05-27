using CompanyAPI.Application.Dto;
using CompanyAPI.Infrastructure.Repositories;
using MediatR;

namespace CompanyAPI.Application.Query
{
    public sealed class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, PaginatedList<CompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompaniesQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<PaginatedList<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Query is null");
            }

            return await _companyRepository.GetAll()
                .Select(c => new CompanyDto(c.Id, c.Isin, c.Name, c.Exchange, c.Ticker, c.Website))
                .AsPaginatedList(request.Limit, request.Offset);
        }
    }
}
