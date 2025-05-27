using CompanyAPI.Infrastructure.Repositories;
using MediatR;

namespace CompanyAPI.Application.Command
{
    public sealed class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly ICompanyRepository _repository;

        public UpdateCompanyCommandHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Command is null");
            }

            if (!request.Id.HasValue)
            {
                throw new ArgumentException("Id cannot be null");
            }

            var company = await _repository.GetByIdAsync(request.Id.Value);
            company.Update(request.Isin, request.Name, request.Exchange, request.Ticker, request.Website);

            await _repository.CommitAsync();
        }
    }
}
