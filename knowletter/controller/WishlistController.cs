using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;
    private readonly IItemService _itemService;

    public WishlistController(IWishlistService wishlistService, IItemService itemService)
    {
        _wishlistService = wishlistService;
        _itemService = itemService;
    }

    [HttpPost]
    public async Task<ActionResult<WishlistResponse>> CreateWishlist(Guid personId, [FromBody] CreateWishlistRequest request)
    {
        var wishlist = await _wishlistService.CreateWishlistAsync(personId, request.Name, request.Description);
        return CreatedAtAction(nameof(GetWishlist), new { id = wishlist.Id }, MapToResponse(wishlist));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WishlistResponse>> GetWishlist(Guid id)
    {
        var wishlist = await _wishlistService.GetWishlistAsync(id);
        var items = await _itemService.GetWishlistItemsAsync(id);
        return Ok(MapToResponse(wishlist, items));
    }

    [HttpGet("person/{personId}")]
    public async Task<ActionResult<List<WishlistResponse>>> GetPersonWishlists(Guid personId)
    {
        var wishlists = await _wishlistService.GetPersonWishlistsAsync(personId);
        var response = new List<WishlistResponse>();

        foreach (var wishlist in wishlists)
        {
            var items = await _itemService.GetWishlistItemsAsync(wishlist.Id);
            response.Add(MapToResponse(wishlist, items));
        }

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WishlistResponse>> UpdateWishlist(Guid id, [FromBody] UpdateWishlistRequest request)
    {
        var wishlist = await _wishlistService.UpdateWishlistAsync(id, request.Name, request.Description);
        var items = await _itemService.GetWishlistItemsAsync(id);
        return Ok(MapToResponse(wishlist, items));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWishlist(Guid id)
    {
        var result = await _wishlistService.DeleteWishlistAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }

    private WishlistResponse MapToResponse(Wishlist wishlist, List<Item> items = null)
    {
        return new WishlistResponse
        {
            Id = wishlist.Id,
            PersonId = wishlist.PersonId,
            Name = wishlist.Name,
            Description = wishlist.Description,
            Items = items?.Select(i => new ItemResponse
            {
                Id = i.Id,
                WishlistId = i.WishlistId,
                Name = i.Name,
                Url = i.Url,
                ImageUrl = i.ImageUrl,
                Price = i.Price
            }).ToList() ?? new List<ItemResponse>()
        };
    }
}
