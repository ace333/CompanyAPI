using CompanyAPI.Application.Command;
using FluentValidation;

namespace CompanyAPI.Application.Validation
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator(IIsinValidator isinValidator)
        {
            RuleFor(c => c.Isin)
                .MustAsync(async (isin, cancellationToken) => await isinValidator.ValidateUniquenessAsync(isin))
                .When(c => !string.IsNullOrEmpty(c.Isin))
                .WithMessage("Isin already taken, please provide unique value");
        }
    }
}
