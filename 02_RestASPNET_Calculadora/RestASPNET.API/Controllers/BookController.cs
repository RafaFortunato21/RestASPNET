using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestASPNET.API.Model;
using RestASPNET.API.Services;

namespace RestASPNET.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var book = _bookService.FindByID(id);
                return Ok(book);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book model) 
        {
            var book = _bookService.FindByID(model.Id);

            if (book == null) return NotFound();

            _bookService.Update(model);

            return Ok(model);


        }

        [HttpPost]
        public IActionResult Post([FromBody] Book model)
        {
            _bookService.Create(model);

            return Ok(model);
        }


        [HttpDelete("{personId}")]
        public IActionResult Delete(long personId)
        {
            var book = _bookService.FindByID(personId);

            if (book == null) return NotFound();

            _bookService.Delete(personId);

            return Ok("Deleted successfully");
        }



    }
}
