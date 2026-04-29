public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person> CreatePersonAsync(string name, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        var person = new Person
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            Wishlists = new List<Wishlist>()
        };

        return await _personRepository.AddPersonAsync(person);
    }

    public async Task<Person> GetPersonAsync(Guid id)
    {
        var person = await _personRepository.GetPersonByIdAsync(id);
        if (person == null)
            throw new KeyNotFoundException($"Person with id {id} not found.");

        return person;
    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        return await _personRepository.GetAllPersonsAsync();
    }

    public async Task<Person> UpdatePersonAsync(Guid id, string name, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        var person = await GetPersonAsync(id);
        person.Name = name;
        person.Email = email;
        person.PhoneNumber = phoneNumber;

        return await _personRepository.UpdatePersonAsync(person);
    }

    public async Task<bool> DeletePersonAsync(Guid id)
    {
        return await _personRepository.DeletePersonAsync(id);
    }
}
