using FluentAssertions;
using Logica.Mappers;
using Logica.Models;
using Logica.Models.Products;
using Data.Entities;
using Xunit;

namespace TechTrendEmporium.UnitTests.Mappers;

public class ProductMapperTests
{
    [Fact]
    public void ToProductDto_WhenRatingCountIsZero_ShouldSetRatingNull()
    {
        var p = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Test",
            Price = 10,
            Description = null,
            Category = new Category { Name = "cat" },
            ImageUrl = null,
            RatingAverage = 0,
            RatingCount = 0
        };

        var dto = p.ToProductDto();

        dto.Rating.Should().BeNull();
        dto.Description.Should().BeEmpty();
        dto.Image.Should().BeEmpty();
        dto.Category.Should().Be("cat");
    }

    [Fact]
    public void ToProductDto_WhenHasRatings_ShouldMapRating()
    {
        var p = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Test",
            Price = 10,
            RatingAverage = 4.5m,
            RatingCount = 3
        };

        var dto = p.ToProductDto();

        dto.Rating.Should().NotBeNull();
        dto.Rating!.Rate.Should().Be(4.5);
        dto.Rating!.Count.Should().Be(3);
    }
}
