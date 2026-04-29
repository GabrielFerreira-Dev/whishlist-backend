public interface IPersonRepository
{
    Task<Person> GetPersonByIdAsync(Guid id);
    Task<Person> AddPersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Person person);
    Task<bool> DeletePersonAsync(Guid id);
    Task<List<Person>> GetAllPersonsAsync();
}
