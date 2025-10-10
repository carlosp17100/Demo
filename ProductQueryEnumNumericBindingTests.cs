using System.Text.Json;
using FluentAssertions;
using Logica.Models;
using Xunit;

namespace TechTrendEmporium.UnitTests.Validation;

public class ProductQueryEnumNumericBindingTests
{
    private static readonly JsonSerializerOptions _opts = new()
    {
        PropertyNameCaseInsensitive = true
        // NOTA: no agregamos JsonStringEnumConverter.
        // Por defecto System.Text.Json permite números en enums.
    };

    [Fact]
    public void Deserialize_WithNumericEnums_ShouldMapToCorrectValues()
    {
        // Title=0, Price=1, Rating=2
        // Asc=0, Desc=1
        var json = /* language=json */ """
        {
          "title": "zzz",
          "sortBy": 1,
          "sortDir": 1
        }
        """;

        var q = JsonSerializer.Deserialize<ProductQuery>(json, _opts);

        q.Should().NotBeNull();
        q!.SortBy.Should().Be(ProductSortBy.Price);
        q.SortDir.Should().Be(SortDirection.Desc);
        q.Title.Should().Be("zzz");
    }

    [Fact]
    public void Defaults_WhenNothingProvided_ShouldBeTitleAscPage1Size12()
    {
        var q = JsonSerializer.Deserialize<ProductQuery>("{}", _opts);

        q.Should().NotBeNull();
        q!.SortBy.Should().Be(ProductSortBy.Title);
        q.SortDir.Should().Be(SortDirection.Asc);
        q.Page.Should().Be(1);
        q.PageSize.Should().Be(12);
    }
}
