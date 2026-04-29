public class Wishlist
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }

    public Wishlist()
    {
        Id = Guid.NewGuid();
        Items = new List<Item>();
    }
}
