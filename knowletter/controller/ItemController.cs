using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/wishlists/{wishlistId}/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpPost]
    public async Task<ActionResult<ItemResponse>> CreateItem(Guid wishlistId, [FromBody] CreateItemRequest request)
    {
        var item = await _itemService.AddItemToWishlistAsync(wishlistId, request.Name, request.Url, request.ImageUrl, request.Price);
        return CreatedAtAction(nameof(GetItem), new { wishlistId = wishlistId, id = item.Id }, MapToResponse(item));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemResponse>> GetItem(Guid wishlistId, Guid id)
    {
        var item = await _itemService.GetItemAsync(id);
        if (item.WishlistId != wishlistId)
            return BadRequest("Item does not belong to this wishlist.");

        return Ok(MapToResponse(item));
    }

    [HttpGet]
    public async Task<ActionResult<List<ItemResponse>>> GetWishlistItems(Guid wishlistId)
    {
        var items = await _itemService.GetWishlistItemsAsync(wishlistId);
        return Ok(items.Select(MapToResponse).ToList());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ItemResponse>> UpdateItem(Guid wishlistId, Guid id, [FromBody] UpdateItemRequest request)
    {
        var item = await _itemService.GetItemAsync(id);
        if (item.WishlistId != wishlistId)
            return BadRequest("Item does not belong to this wishlist.");

        var updatedItem = await _itemService.UpdateItemAsync(id, request.Name, request.Url, request.ImageUrl, request.Price);
        return Ok(MapToResponse(updatedItem));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid wishlistId, Guid id)
    {
        var item = await _itemService.GetItemAsync(id);
        if (item.WishlistId != wishlistId)
            return BadRequest("Item does not belong to this wishlist.");

        var result = await _itemService.DeleteItemAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }

    private ItemResponse MapToResponse(Item item)
    {
        return new ItemResponse
        {
            Id = item.Id,
            WishlistId = item.WishlistId,
            Name = item.Name,
            Url = item.Url,
            ImageUrl = item.ImageUrl,
            Price = item.Price
        };
    }
}
