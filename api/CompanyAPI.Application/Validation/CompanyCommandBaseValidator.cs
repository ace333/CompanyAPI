using CompanyAPI.Application.Command;
using FluentValidation;

namespace CompanyAPI.Application.Validation
{
    public class CompanyCommandBaseValidator : AbstractValidator<CompanyCommandBase>
    {
        private const string RequiredErrorMessage = "{0} is required";
        private const string LengthErrorMessage = "Maximum characters count is 255";

        public CompanyCommandBaseValidator(IIsinValidator isinValidator)
        {
            RuleFor(c => c.Isin)
                .NotEmpty()
                .WithMessage(string.Format(RequiredErrorMessage, "Isin"));

            RuleFor(c => c.Isin)
                .Must(isinValidator.ValidateFormat)
                .When(c => !string.IsNullOrEmpty(c.Isin))
                .WithMessage("Isin format is not valid");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(string.Format(RequiredErrorMessage, "Name"))
                .MaximumLength(255)
                .WithMessage(LengthErrorMessage);

            RuleFor(c => c.Ticker)
                .NotEmpty()
                .WithMessage(string.Format(RequiredErrorMessage, "Ticker"))
                .MaximumLength(255)
                .WithMessage(LengthErrorMessage);

            RuleFor(c => c.Exchange)
                .NotEmpty()
                .WithMessage(string.Format(RequiredErrorMessage, "Exchange"))
                .MaximumLength(255)
                .WithMessage(LengthErrorMessage);

            RuleFor(c => c.Website)
                .MaximumLength(255)
                .When(c => !string.IsNullOrEmpty(c.Website))
                .WithMessage(LengthErrorMessage);
        }
    }
}
