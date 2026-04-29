using MongoDB.Driver;

public class PersonRepository : IPersonRepository
{
    private readonly IMongoCollection<Person> _personCollection;

    public PersonRepository(IMongoDatabase database)
    {
        _personCollection = database.GetCollection<Person>("persons");
    }

    public async Task<Person> AddPersonAsync(Person person)
    {
        await _personCollection.InsertOneAsync(person);
        return person;
    }

    public async Task<bool> DeletePersonAsync(Guid id)
    {
        var result = await _personCollection.DeleteOneAsync(p => p.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        return await _personCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Person> GetPersonByIdAsync(Guid id)
    {
        return await _personCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var result = await _personCollection.ReplaceOneAsync(p => p.Id == person.Id, person);
        if (result.ModifiedCount == 0)
            throw new KeyNotFoundException($"Person with id {person.Id} not found.");
        return person;
    }
}
