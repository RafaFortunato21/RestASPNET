using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestASPNET.API.Data.DTO;
using RestASPNET.API.Services;

namespace RestASPNET.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        [Route("signin")]
        
        public IActionResult Signin([FromBody] UserDto model)
        {
            if (model == null) return BadRequest("Invalid Client request");

            var token = _loginService.ValidateCredentials(model.UserName,model.Password); ;

            if (token == null) return Unauthorized();


            return Ok(token);
        }

    }
}
