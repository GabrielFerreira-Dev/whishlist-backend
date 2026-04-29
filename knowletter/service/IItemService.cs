public interface IItemService
{
    Task<Item> AddItemToWishlistAsync(Guid wishlistId, string name, string url, string imageUrl, double price);
    Task<Item> GetItemAsync(Guid id);
    Task<List<Item>> GetWishlistItemsAsync(Guid wishlistId);
    Task<Item> UpdateItemAsync(Guid id, string name, string url, string imageUrl, double price);
    Task<bool> DeleteItemAsync(Guid id);
}
