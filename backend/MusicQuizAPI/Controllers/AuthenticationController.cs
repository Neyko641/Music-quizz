using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using MusicQuizAPI.Models;

namespace MusicQuizAPI.Controllers
{
    [EnableCors("MusicQuizPolicy")]
    [Route("/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        public AuthenticationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Post()
        {
            /*
            POST Request is required to have 'Authorization' header with value
            of "Basic [username].[password]" in base64 string.
            */

            ResultContext result = new ResultContext();
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            string key = "";

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                result.AddExceptionMessage("Authorization header is required!");
            }
            else
            {
                try 
                {
                    key = authorizationHeader.Split(' ')[1];
                    var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(key)).Split(':');

                    //credentials[0] is the username
                    //credentials[1] is the password
                    if (true) // TODO: Add correct validation
                    {
                        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));
                        
                        result.AddData(new 
                        {
                            token = GenerateToken(secret)
                        });

                        return Ok(result.Result());
                    }
                    result.AddExceptionMessage("Wrong 'username' or 'password'!");
                }
                catch (Exception)
                {
                    result.AddExceptionMessage("Bad payload!");
                }
            }

            return BadRequest(result.Result());
        }

        private string GenerateToken(SecurityKey key)
        {
            var now = DateTime.UtcNow;
            var issuer = Configuration["JWT:Issuer"];
            var audience = Configuration["JWT:Audience"];
            var identity = new ClaimsIdentity();
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(issuer, audience, identity, now, 
                now.Add(TimeSpan.FromHours(1)), now, signingCredentials);
            var encodedJwt = handler.WriteToken(token);
            return encodedJwt;
        }
    }

    
}