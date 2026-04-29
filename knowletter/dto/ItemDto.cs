public class CreateItemRequest
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
}

public class UpdateItemRequest
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
}

public class ItemResponse
{
    public Guid Id { get; set; }
    public Guid WishlistId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
}
