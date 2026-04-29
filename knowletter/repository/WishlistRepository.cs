using MongoDB.Driver;

public class WishlistRepository : IWishlistRepository
{
    private readonly IMongoCollection<Wishlist> _wishlistCollection;

    public WishlistRepository(IMongoDatabase database)
    {
        _wishlistCollection = database.GetCollection<Wishlist>("wishlists");
    }

    public async Task<Wishlist> CreateAsync(Wishlist wishlist)
    {
        await _wishlistCollection.InsertOneAsync(wishlist);
        return wishlist;
    }

    public async Task<Wishlist> GetByIdAsync(Guid id)
    {
        return await _wishlistCollection.Find(w => w.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Wishlist>> GetByPersonIdAsync(Guid personId)
    {
        return await _wishlistCollection.Find(w => w.PersonId == personId).ToListAsync();
    }

    public async Task<Wishlist> UpdateAsync(Guid id, Wishlist wishlist)
    {
        var result = await _wishlistCollection.ReplaceOneAsync(w => w.Id == id, wishlist);
        if (result.ModifiedCount == 0)
            throw new KeyNotFoundException($"Wishlist with id {id} not found.");
        return wishlist;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _wishlistCollection.DeleteOneAsync(w => w.Id == id);
        return result.DeletedCount > 0;
    }
}
