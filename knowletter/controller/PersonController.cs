using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    public async Task<ActionResult<PersonResponse>> CreatePerson([FromBody] CreatePersonRequest request)
    {
        var person = await _personService.CreatePersonAsync(request.Name, request.Email, request.PhoneNumber);
        return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, MapToResponse(person));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonResponse>> GetPerson(Guid id)
    {
        var person = await _personService.GetPersonAsync(id);
        return Ok(MapToResponse(person));
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonResponse>>> GetAllPersons()
    {
        var persons = await _personService.GetAllPersonsAsync();
        return Ok(persons.Select(MapToResponse).ToList());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PersonResponse>> UpdatePerson(Guid id, [FromBody] UpdatePersonRequest request)
    {
        var person = await _personService.UpdatePersonAsync(id, request.Name, request.Email, request.PhoneNumber);
        return Ok(MapToResponse(person));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var result = await _personService.DeletePersonAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }

    private PersonResponse MapToResponse(Person person)
    {
        return new PersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Email = person.Email,
            PhoneNumber = person.PhoneNumber,
            WishlistCount = person.Wishlists?.Count ?? 0
        };
    }
}
