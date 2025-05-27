using CompanyAPI.Application.Dto;
using MediatR;

namespace CompanyAPI.Application.Query
{
    public sealed class GetCompanyByIdQuery : IRequest<CompanyDto>
    {
        public int? Id { get; set; }
    }
}
