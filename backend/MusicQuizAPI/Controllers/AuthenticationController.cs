using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Helpers;
using MusicQuizAPI.Models.Parameters;


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
        of "[email]:[password]" in base64 string.
        */
    


        // If body isn't provided for username it gives status code 415 unsupported media type
        // in the future add this to the documentation


        [HttpPost("register")]
        public IActionResult Register([FromHeader] AuthorizationParamModel authParams,
            [FromBody] RegisterUserParamModel regParams)
        {
            string token = SecurityHelper.RegisterUser(
                userService: UserService, 
                authorizationHeader: authParams.Authorization, 
                secret: Configuration["JWT:Secret"],
                username: regParams.Username
            );

            ResponseContext.AddData(new 
            {
                token = token
            }, HttpStatusCode.Created);
            
            return Created("", ResponseContext.Body);
        }

        [HttpPost("login")]
        public IActionResult Login([FromHeader] AuthorizationParamModel parameters)
        {
            string token = SecurityHelper.LoginUser(
                userService: UserService, 
                authorizationHeader: parameters.Authorization, 
                secret: Configuration["JWT:Secret"]
            );

            ResponseContext.AddData(new 
            {
                token = token
            });
            
            return Ok(ResponseContext.Body);
        }
    }

    
}