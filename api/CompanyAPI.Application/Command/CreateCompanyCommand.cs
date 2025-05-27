using MediatR;

namespace CompanyAPI.Application.Command
{
    public sealed class CreateCompanyCommand : CompanyCommandBase, IRequest
    {
    }
}
