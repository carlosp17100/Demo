using System;
using System.Linq;
using Data.Entities;
using Logica.Models;                 // RatingDto, ProductSummaryDto
using Logica.Models.Category;
using Logica.Models.Products;       // ProductDto, ProductCreateDto, ProductUpdateDto, CartDto, CartItemDto

namespace Logica.Mappers
{
    /// <summary>
    /// Mapper entre entidades de dominio y DTOs.
    /// </summary>
    public static class ProductMapper
    {
        // ================== PRODUCTO ==================

        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description ?? string.Empty,
                Category = product.Category?.Name ?? string.Empty,
                Image = product.ImageUrl ?? string.Empty,
                Rating = product.RatingCount > 0
                                ? new RatingDto
                                {
                                    Rate = (double)product.RatingAverage,
                                    Count = product.RatingCount
                                }
                                : null
            };
        }

        public static Product ToProduct(this ProductCreateDto createDto)
        {
            return new Product
            {
                Title = createDto.Title,
                Price = createDto.Price,
                Description = createDto.Description,
                ImageUrl = createDto.Image,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateProduct(this Product product, ProductUpdateDto updateDto)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.Title))
                product.Title = updateDto.Title;

            if (updateDto.Price.HasValue)
                product.Price = updateDto.Price.Value;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                product.Description = updateDto.Description;

            if (!string.IsNullOrWhiteSpace(updateDto.Image))
                product.ImageUrl = updateDto.Image;

            // Rating opcional
            if (updateDto.Rating is not null)
            {
                product.RatingAverage = (decimal)updateDto.Rating.Rate;
                product.RatingCount = updateDto.Rating.Count;
            }

            // Inventario opcional
            if (updateDto.Inventory is not null)
            {
                product.InventoryTotal = updateDto.Inventory.Total;
                product.InventoryAvailable = updateDto.Inventory.Available;
            }

            product.UpdatedAt = DateTime.UtcNow;
        }

        // ================== RESUMEN ==================

        /// <summary>Resumen corto para listados por usuario.</summary>
        public static ProductSummaryDto ToSummaryDto(this Product product)
        {
            return new ProductSummaryDto
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Category = product.Category?.Name ?? "Sin categoría"
            };
        }

        // ================== CARRITO (si aplica) ==================

        public static CartDto ToCartDto(this Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Status = cart.Status.ToString(),
                TotalBeforeDiscount = cart.TotalBeforeDiscount,
                DiscountAmount = cart.DiscountAmount,
                ShippingCost = cart.ShippingCost,
                FinalTotal = cart.FinalTotal,
                CreatedAt = cart.CreatedAt,
                CouponCode = cart.AppliedCoupon?.Code,
                Products = cart.CartItems?.Select(ci => new CartItemDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductTitle = ci.TitleSnapshot,
                    UnitPrice = ci.UnitPriceSnapshot,
                    ProductImage = ci.ImageUrlSnapshot,
                    TotalPrice = ci.UnitPriceSnapshot * ci.Quantity
                }).ToList() ?? new System.Collections.Generic.List<CartItemDto>()
            };
        }

        // ===== LIST ITEM (para vitrina / store) =====
        public static ProductListItemDto ToListItemDto(this Product product)
        {
            return new ProductListItemDto
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description ?? string.Empty,
                Category = product.Category?.Name ?? string.Empty,
                Image = product.ImageUrl ?? string.Empty,
                Rating = new RatingDto
                {
                    Rate = (double)product.RatingAverage, // decimal(2,1) -> double
                    Count = product.RatingCount
                }
            };
        }

    }
}
