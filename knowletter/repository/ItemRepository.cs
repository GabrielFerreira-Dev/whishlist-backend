using MongoDB.Driver;

public class ItemRepository : IItemRepository
{
    private readonly IMongoCollection<Item> _itemCollection;

    public ItemRepository(IMongoDatabase database)
    {
        _itemCollection = database.GetCollection<Item>("items");
    }

    public async Task<Item> CreateAsync(Item item)
    {
        await _itemCollection.InsertOneAsync(item);
        return item;
    }

    public async Task<Item> GetByIdAsync(Guid id)
    {
        return await _itemCollection.Find(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Item>> GetByWishlistIdAsync(Guid wishlistId)
    {
        return await _itemCollection.Find(i => i.WishlistId == wishlistId).ToListAsync();
    }

    public async Task<Item> UpdateAsync(Guid id, Item item)
    {
        var result = await _itemCollection.ReplaceOneAsync(i => i.Id == id, item);
        if (result.ModifiedCount == 0)
            throw new KeyNotFoundException($"Item with id {id} not found.");
        return item;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _itemCollection.DeleteOneAsync(i => i.Id == id);
        return result.DeletedCount > 0;
    }
}
