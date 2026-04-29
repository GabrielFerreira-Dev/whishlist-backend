public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;

    public WishlistService(IWishlistRepository wishlistRepository)
    {
        _wishlistRepository = wishlistRepository;
    }

    public async Task<Wishlist> CreateWishlistAsync(Guid personId, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Wishlist name cannot be empty.");

        var wishlist = new Wishlist
        {
            PersonId = personId,
            Name = name,
            Description = description
        };

        return await _wishlistRepository.CreateAsync(wishlist);
    }

    public async Task<Wishlist> GetWishlistAsync(Guid id)
    {
        var wishlist = await _wishlistRepository.GetByIdAsync(id);
        if (wishlist == null)
            throw new KeyNotFoundException($"Wishlist with id {id} not found.");

        return wishlist;
    }

    public async Task<List<Wishlist>> GetPersonWishlistsAsync(Guid personId)
    {
        return await _wishlistRepository.GetByPersonIdAsync(personId);
    }

    public async Task<Wishlist> UpdateWishlistAsync(Guid id, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Wishlist name cannot be empty.");

        var wishlist = await GetWishlistAsync(id);
        wishlist.Name = name;
        wishlist.Description = description;

        return await _wishlistRepository.UpdateAsync(id, wishlist);
    }

    public async Task<bool> DeleteWishlistAsync(Guid id)
    {
        return await _wishlistRepository.DeleteAsync(id);
    }
}
