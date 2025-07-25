﻿using FluentValidation;
using UKParliament.CodeTest.Data.ViewModels;
using UKParliament.CodeTest.Services.Services.Interfaces;

namespace UKParliament.CodeTest.Services.Validators;

public class EmployeeCreateValidator : AbstractValidator<EmployeeViewModel>
{
    public EmployeeCreateValidator(ILookUpService lookUpService)
    {
        RuleFor(p => p.FirstName).NotEmpty().MinimumLength(2);

        RuleFor(p => p.LastName).NotEmpty().MinimumLength(2);

        RuleFor(e => e.PayBand)
            .Must(pb => lookUpService.SearchPayBands(pb).Any())
            .When(e => !string.IsNullOrWhiteSpace(e.PayBand))
            .WithMessage("The PayBand you've assigned couldn't be found");

        RuleFor(e => e.PayBand)
            .Must(pb => !string.IsNullOrWhiteSpace(pb))
            .When(e => e.Salary is not null)
            .WithMessage("You need to assign a PayBand to set a salary");

        RuleFor(e => e.Salary)
            .Must(
                (vm, salary) =>
                {
                    var payBand = lookUpService.SearchPayBands(vm.PayBand).FirstOrDefault();

                    return salary >= payBand!.MinPay && salary <= payBand!.MaxPay;
                }
            )
            .When(vm => !string.IsNullOrWhiteSpace(vm.PayBand))
            .WithMessage(vm =>
            {
                var payBand = lookUpService.SearchPayBands(vm.PayBand).FirstOrDefault();
                return $"Salary must be between PayBand minimum pay ({payBand!.MinPay:C}) and maximum pay ({payBand!.MaxPay:C})";
            });
    }
}
