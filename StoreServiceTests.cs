using System.Threading;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using FluentAssertions;
using Logica.Models;
using Logica.Models.Products;
using Logica.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TechTrendEmporium.UnitTests.Services;

public class StoreServiceTests
{
    private static AppDbContext BuildDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var db = new AppDbContext(options);
        db.Products.AddRange(
            new Product { Id = Guid.NewGuid(), Title = "Alpha", Price = 50, Category = new Category { Name = "A" } },
            new Product { Id = Guid.NewGuid(), Title = "Beta", Price = 20, Category = new Category { Name = "B" } },
            new Product { Id = Guid.NewGuid(), Title = "Gamma", Price = 80, Category = new Category { Name = "A" } }
        );
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task GetProducts_ShouldFilterAndSort_AscByPrice()
    {
        using var db = BuildDb();
        var svc = new StoreService(db);

        var q = new ProductQuery
        {
            Title = "a",             // contiene "a"
            Price = 60,              // <= 60
            SortBy = ProductSortBy.Price,
            SortDir = SortDirection.Asc,
            Page = 1,
            PageSize = 10
        };

        var result = await svc.GetProductsAsync(q, CancellationToken.None);

        result.Items.Should().HaveCount(2);
        result.Items.Select(i => i.Title).Should().ContainInOrder("Beta", "Alpha");
    }

    [Fact]
    public async Task GetProducts_ShouldSort_DescByTitle_WhenNoFilters()
    {
        using var db = BuildDb();
        var svc = new StoreService(db);

        var q = new ProductQuery
        {
            SortBy = ProductSortBy.Title,
            SortDir = SortDirection.Desc,
            Page = 1,
            PageSize = 10
        };

        var result = await svc.GetProductsAsync(q, CancellationToken.None);

        result.Items.Select(i => i.Title).Should().ContainInOrder("Gamma", "Beta", "Alpha");
    }
}
