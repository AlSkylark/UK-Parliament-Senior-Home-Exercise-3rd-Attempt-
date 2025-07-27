using FluentValidation;
using UKParliament.CodeTest.Data.Models;

namespace UKParliament.CodeTest.Services.Validators;

public class DepartmentValidator : AbstractValidator<Department>
{
    public DepartmentValidator()
    {
        RuleFor(d => d.Name).NotEmpty().MinimumLength(2);
    }
}
