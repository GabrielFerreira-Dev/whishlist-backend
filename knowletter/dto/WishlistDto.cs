public class CreateWishlistRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateWishlistRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class WishlistResponse
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ItemResponse> Items { get; set; } = new();
}
