using CompanyAPI.Application.Dto;
using MediatR;

namespace CompanyAPI.Application.Query
{
    public sealed class GetCompaniesQuery : PageableQuery, IRequest<PaginatedList<CompanyDto>>
    {
    }
}
