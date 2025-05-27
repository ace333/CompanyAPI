using CompanyAPI.Application.Command;
using CompanyAPI.Application.Dto;
using CompanyAPI.Application.Query;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CompanyAPI.Api.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CompanyCommandBase> _companyBaseValidator;
        private readonly IValidator<CreateCompanyCommand> _createCompanyCommandValidator;
        private readonly IValidator<UpdateCompanyCommand> _updateCompanyCommandValidator;

        public CompanyController(IMediator mediator, 
            IValidator<CompanyCommandBase> companyBaseValidator,
            IValidator<CreateCompanyCommand> createCompanyCommandValidator,
            IValidator<UpdateCompanyCommand> updateCompanyCommandValidator) 
        {
            _mediator = mediator;
            _companyBaseValidator = companyBaseValidator;
            _createCompanyCommandValidator = createCompanyCommandValidator;
            _updateCompanyCommandValidator = updateCompanyCommandValidator;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<string>))]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<string>))]
        [SwaggerOperation("Creates a new company entity and returns 201 success code")]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            var validationResults = new ValidationResult[] { _companyBaseValidator.Validate(command), await _createCompanyCommandValidator.ValidateAsync(command) };
            if(validationResults.Any(x => !x.IsValid))
            {
                return BadRequest(validationResults.SelectMany(x => x.Errors.Select(e => e.ToString())));
            }

            await _mediator.Send(command, cancellationToken);
            return Created();
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<string>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<string>))]
        [SwaggerOperation("Updates a company entity and returns 204 success code")]
        public async Task<IActionResult> UpdateCompanyAsync(int id, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var validationResults = new ValidationResult[] { _companyBaseValidator.Validate(command), await _updateCompanyCommandValidator.ValidateAsync(command) };
            if (validationResults.Any(x => !x.IsValid))
            {
                return BadRequest(validationResults.SelectMany(x => x.Errors.Select(e => e.ToString())));
            }

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<CompanyDto>))]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerOperation("Returns an object with pageable list of companies with 200 success code")]
        public async Task<IActionResult> GetComapniesAsync([FromQuery] GetCompaniesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Company not found")]
        [SwaggerOperation("Returns company filtered by id 200 (or 404 null when not found)")]
        public async Task<IActionResult> GetCompanyByIdAsync(int id, [FromQuery] GetCompanyByIdQuery query)
        {
            query.Id = id;
            var result = await _mediator.Send(query);

            if (result.Id == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("isin/{isin}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Company not found")]
        [SwaggerOperation("Returns company filtered by id 200 (or 404 null when not found)")]
        public async Task<IActionResult> GetCompanyByIsinAsync(string isin, [FromQuery] GetCompanyByIsinQuery query)
        {
            query.Isin = isin;
            var result = await _mediator.Send(query);

            if (result.Id == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
