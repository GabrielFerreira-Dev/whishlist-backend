public interface IWishlistRepository
{
    Task<Wishlist> CreateAsync(Wishlist wishlist);
    Task<Wishlist> GetByIdAsync(Guid id);
    Task<List<Wishlist>> GetByPersonIdAsync(Guid personId);
    Task<Wishlist> UpdateAsync(Guid id, Wishlist wishlist);
    Task<bool> DeleteAsync(Guid id);
}
