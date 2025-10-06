using Logica.Models.Products;

namespace Logica.Models.Wishlist
{
    public class WishlistResponseDto
    {
        public Guid UserId { get; set; }
        public IEnumerable<Guid> Wishlist { get; set; } = new List<Guid>();
        
        // Versi�n extendida con informaci�n de productos (opcional)
        public IEnumerable<WishlistItemDto>? WishlistItems { get; set; }
    }
}