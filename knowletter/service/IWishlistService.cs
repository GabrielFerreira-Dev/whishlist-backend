public interface IWishlistService
{
    Task<Wishlist> CreateWishlistAsync(Guid personId, string name, string description);
    Task<Wishlist> GetWishlistAsync(Guid id);
    Task<List<Wishlist>> GetPersonWishlistsAsync(Guid personId);
    Task<Wishlist> UpdateWishlistAsync(Guid id, string name, string description);
    Task<bool> DeleteWishlistAsync(Guid id);
}
