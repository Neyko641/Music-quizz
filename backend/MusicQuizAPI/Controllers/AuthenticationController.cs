using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Models.Parameters;
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
        ResponseContext ResponseContext { get; set; }

        public AuthenticationController(ILogger<AuthenticationController> logger, 
            IConfiguration configuration, UserService userService)
        {
            Logger = logger;
            Configuration = configuration;
            UserService = userService;
            ResponseContext = new ResponseContext();
        }

        /*
        POST Requests are required to have 'Authorization' header with value
        of "[username]:[password]" in base64 string.
        */
            

        [HttpPost("register")]
        public IActionResult Register([FromHeader] AuthorizationParamModel parameters)
        {
            SecurityHelper.RegisterUser(ResponseContext, UserService, parameters.Authorization, 
                Configuration["JWT:Secret"]);
            
            return Ok(ResponseContext.Body);
        }

        [HttpPost("login")]
        public IActionResult Login([FromHeader] AuthorizationParamModel parameters)
        {
            SecurityHelper.LoginUser(ResponseContext, UserService, parameters.Authorization, 
                Configuration["JWT:Secret"]);
            
            return Ok(ResponseContext.Body);
        }
    }

    
}