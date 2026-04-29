using MongoDB.Driver;

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}

public static class MongoDbServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();

        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        services.AddSingleton(database);
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();

        return services;
    }
}
