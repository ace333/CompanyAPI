using CompanyAPI.Application.Command;
using FluentValidation;

namespace CompanyAPI.Application.Validation
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator(IIsinValidator isinValidator)
        {
            RuleFor(c => c)
                .MustAsync(async (c, cancellationToken) => await isinValidator.ValidateUniquenessAsync(c.Isin, c.Id))
                .When(c => !string.IsNullOrEmpty(c.Isin))
                .WithMessage("Isin already taken, please provide unique value");
        }
    }
}
