using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Helpers;

namespace MusicQuizAPI.Controllers
{
    [EnableCors("MusicQuizPolicy")]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger, 
            IConfiguration configuration, UserService userService)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = userService;
        }

        /*
        POST Requests are required to have 'Authorization' header with value
        of "[username]:[password]" in base64 string.
        */
            

        [HttpPost("register")]
        public IActionResult Register()
        {
            string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            string secret = _configuration["JWT:Secret"];
            
            ResultContext result = SecurityHelper.RegisterUser(_userService, authorizationHeader, secret);
            
            if (result.StatusCode == 200)
            {
                return Ok(result.Result());
            }

            return BadRequest(result.Result());
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            string secret = _configuration["JWT:Secret"];
            
            ResultContext result = SecurityHelper.LoginUser(_userService, authorizationHeader, secret);
            
            if (result.StatusCode == 200)
            {
                return Ok(result.Result());
            }

            return BadRequest(result.Result());
        }
    }

    
}