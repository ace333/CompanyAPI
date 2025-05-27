using CompanyAPI.Application.Dto;
using CompanyAPI.Infrastructure.Repositories;
using MediatR;

namespace CompanyAPI.Application.Query
{
    public sealed class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto>
    {
        private readonly ICompanyRepository _repository;

        public GetCompanyByIdQueryHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanyDto> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Query is null");
            }

            if (!request.Id.HasValue)
            {
                throw new ArgumentNullException("Isin cannot be empty");
            }

            var result = await _repository.GetByIdAsync(request.Id.Value);

            if (result == null)
            {
                return CompanyDto.Empty();
            }

            return new CompanyDto(result.Id, result.Isin, result.Name, result.Exchange, result.Ticker, result.Website);
        }
    }
}
