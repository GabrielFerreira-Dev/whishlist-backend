public interface IItemRepository
{
    Task<Item> CreateAsync(Item item);
    Task<Item> GetByIdAsync(Guid id);
    Task<List<Item>> GetByWishlistIdAsync(Guid wishlistId);
    Task<Item> UpdateAsync(Guid id, Item item);
    Task<bool> DeleteAsync(Guid id);
}
