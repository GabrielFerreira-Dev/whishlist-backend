public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<Item> AddItemToWishlistAsync(Guid wishlistId, string name, string url, string imageUrl, double price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Item name cannot be empty.");

        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");

        var item = new Item
        {
            WishlistId = wishlistId,
            Name = name,
            Url = url,
            ImageUrl = imageUrl,
            Price = price
        };

        return await _itemRepository.CreateAsync(item);
    }

    public async Task<Item> GetItemAsync(Guid id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null)
            throw new KeyNotFoundException($"Item with id {id} not found.");

        return item;
    }

    public async Task<List<Item>> GetWishlistItemsAsync(Guid wishlistId)
    {
        return await _itemRepository.GetByWishlistIdAsync(wishlistId);
    }

    public async Task<Item> UpdateItemAsync(Guid id, string name, string url, string imageUrl, double price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Item name cannot be empty.");

        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");

        var item = await GetItemAsync(id);
        item.Name = name;
        item.Url = url;
        item.ImageUrl = imageUrl;
        item.Price = price;

        return await _itemRepository.UpdateAsync(id, item);
    }

    public async Task<bool> DeleteItemAsync(Guid id)
    {
        return await _itemRepository.DeleteAsync(id);
    }
}
