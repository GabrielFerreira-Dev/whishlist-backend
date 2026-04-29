public interface IPersonService
{
    Task<Person> CreatePersonAsync(string name, string email, string phoneNumber);
    Task<Person> GetPersonAsync(Guid id);
    Task<List<Person>> GetAllPersonsAsync();
    Task<Person> UpdatePersonAsync(Guid id, string name, string email, string phoneNumber);
    Task<bool> DeletePersonAsync(Guid id);
}
