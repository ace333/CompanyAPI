using CompanyAPI.Application.Dto;
using MediatR;

namespace CompanyAPI.Application.Query
{
    public sealed class GetCompanyByIsinQuery : IRequest<CompanyDto>
    {
        public string? Isin { get; set; }
    }
}
