using CompanyAPI.Application.Core;
using CompanyAPI.Domain.Entities;
using CompanyAPI.Infrastructure.Repositories;
using MediatR;

namespace CompanyAPI.Application.Command
{
    public sealed class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException("Command is null");
            }

            var company = new Company(request.Isin, request.Name, request.Exchange, request.Ticker, request.Website);

            _companyRepository.Add(company);
            await _companyRepository.CommitAsync();
        }
    }
}
