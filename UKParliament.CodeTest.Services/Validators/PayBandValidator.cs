using FluentValidation;
using UKParliament.CodeTest.Data.Models;

namespace UKParliament.CodeTest.Services.Validators;

public class PayBandValidator : AbstractValidator<PayBand>
{
    public PayBandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2);
        RuleFor(p => p.MinPay)
            .NotNull()
            .NotEqual(0)
            .LessThan(p => p.MaxPay)
            .WithMessage("Minimum pay must be smaller than maximum pay");
        RuleFor(p => p.MaxPay)
            .NotNull()
            .NotEqual(0)
            .GreaterThan(p => p.MinPay)
            .WithMessage("Maximum pay must be bigger than minimum pay");
    }
}
