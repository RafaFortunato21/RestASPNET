using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestASPNET.API.Model;
using RestASPNET.API.Services;

namespace RestASPNET.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
            
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindByID(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return NotFound();

            return Ok(_personService.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return NotFound();

            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var person = _personService.FindByID(id);

            if (person == null) return NotFound();

            _personService.Delete(person.Id);

            return Ok();
        }
    }
}
