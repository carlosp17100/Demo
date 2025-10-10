using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Logica.Models.Products;
using Xunit;

namespace TechTrendEmporium.UnitTests.Validation;

public class ProductQueryValidationTests
{
    private static IList<ValidationResult> Validate(object instance)
    {
        var ctx = new ValidationContext(instance);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(instance, ctx, results, validateAllProperties: true);
        return results;
    }

    [Fact]
    public void SortDir_WhenIsNumber_ShouldFailValidation()
    {
        var q = new ProductQuery { SortBy = "title", SortDir = "1" };
        var results = Validate(q);
        results.Should().NotBeEmpty();
        results.Should().Contain(r => r.MemberNames.Contains(nameof(ProductQuery.SortDir)));
    }

    [Theory]
    [InlineData("asc")]
    [InlineData("desc")]
    public void SortDir_WhenValid_ShouldPass(string ok)
    {
        var q = new ProductQuery { SortBy = "title", SortDir = ok };
        var results = Validate(q);
        results.Should().BeEmpty();
    }
}
