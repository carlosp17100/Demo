using System;
using System.Linq;
using Data.Entities;
using Logica.Models;                 // RatingDto, ProductListItemDto, ProductSummaryDto
using Logica.Models.Products;       // ProductDto, ProductCreateDto, ProductUpdateDto
using Logica.Models.Carts;          // CartDto

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
                    // El DTO de vitrina siempre espera Rating (no-null)
                    Rate = (double)product.RatingAverage,
                    Count = product.RatingCount
                }
            };
        }

        // ================== CARRITO (mínimo para compilar) ==================
        // Si tu CartDto tiene más propiedades, agrégalas aquí cuando las definas.
        public static CartDto ToCartDto(this Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                // Si CartDto.UserId fuera Guid, cambia a: UserId = cart.UserId
                UserId = cart.UserId.ToString()
                // (Intencionalmente sin Items/Products ni CartItemDto para evitar los errores actuales)
            };
        }
    }
}
