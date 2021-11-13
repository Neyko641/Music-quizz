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
        IConfiguration Configuration { get; set; }
        UserService UserService { get; set; }
        ILogger<AuthenticationController> Logger { get; set; }
        ResultContext Result { get; set; }

        public AuthenticationController(ILogger<AuthenticationController> logger, 
            IConfiguration configuration, UserService userService)
        {
            Logger = logger;
            Configuration = configuration;
            UserService = userService;
            Result = new ResultContext();
        }

        /*
        POST Requests are required to have 'Authorization' header with value
        of "[username]:[password]" in base64 string.
        */
            

        [HttpPost("register")]
        public IActionResult Register()
        {
            string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            string secret = Configuration["JWT:Secret"];
            
            SecurityHelper.RegisterUser(Result, UserService, authorizationHeader, secret);
            
            return Result.Result();
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            string secret = Configuration["JWT:Secret"];
            
            SecurityHelper.LoginUser(Result, UserService, authorizationHeader, secret);
            
            return Result.Result();
        }
    }

    
}