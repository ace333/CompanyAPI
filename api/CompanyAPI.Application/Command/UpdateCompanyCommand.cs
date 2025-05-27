using MediatR;

namespace CompanyAPI.Application.Command
{
    public sealed class UpdateCompanyCommand : CompanyCommandBase, IRequest
    {
        public int? Id { get; set; }
    }
}
