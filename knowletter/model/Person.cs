public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<Wishlist> Wishlists { get; set; }

    public Person()
    {
        Id = Guid.NewGuid();
    }

    public Wishlist CreateWishlist(string name, string description)
    {
        var wishlist = new Wishlist
        {
            PersonId = Id,
            Name = name,
            Description = description
        };
        Wishlists.Add(wishlist);
        return wishlist;
    }
}
