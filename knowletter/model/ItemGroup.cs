public class ItemGroup
{
    public Guid Id { get; set; }
    public Guid WishlistId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<Item> Items { get; set; } = new();

    public double AveragePrice => Items.Count > 0 ? Items.Average(i => i.Price) : 0;
    public int ItemCount => Items.Count;

    public void AddItem(Item item)
    {
        if (Items.Count >= 3)
        {
            throw new InvalidOperationException("Maximum of 3 items allowed per group.");
        }
        Items.Add(item);
    }

    public ItemGroup()
    {
        Id = Guid.NewGuid();
    }
}
