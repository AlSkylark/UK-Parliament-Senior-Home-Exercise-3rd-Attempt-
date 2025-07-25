﻿using FakeItEasy;
using FluentValidation;
using FluentValidation.Results;
using UKParliament.CodeTest.Data.Models;
using UKParliament.CodeTest.Services.Mappers;
using UKParliament.CodeTest.Services.Services.Interfaces;
using Xunit;

namespace UKParliament.CodeTest.Tests;

public class EmployeeMapperTests
{
    [Fact]
    public void Map_ShouldMapBasics_Correctly()
    {
        var fakeLookupService = A.Fake<ILookUpService>();
        var fakeEmployeeValidator = A.Fake<IValidator<Employee>>();

        var mapper = new EmployeeMapper(fakeLookupService, fakeEmployeeValidator);
        var employee = new Employee { FirstName = "Alex", LastName = "Castro" };

        A.CallTo(() => fakeEmployeeValidator.Validate(employee))
            .ReturnsLazily(() => new ValidationResult());

        var result = mapper.Map(employee);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Equal("Alex", result.FirstName);
            Assert.Equal("Castro", result.LastName);
        });
    }
}
