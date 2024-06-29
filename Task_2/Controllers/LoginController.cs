using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_2.Services;

namespace Task_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenGenerator _tokengen;

        public LoginController(TokenGenerator tokenGen)
        {
            _tokengen = tokenGen;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLogin login)
        {
            if (login.Username == "user" && login.Password == "1234")
            {
                var token = _tokengen.GenerateToken(login.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }

    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}

