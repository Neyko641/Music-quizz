using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;

namespace MusicQuizAPI.Helpers
{
    public static class SecurityHelper
    {
        public static ResultContext RegisterUser(ResultContext result, UserService userService, 
            string authorizationHeader, string secretKey)
        {
            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                result.AddException("Authorization header is required!", ExceptionCode.MissingHeader);
            }
            else
            {
                try 
                {
                    var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader)).Split(':');

                    if (userService.RegisterUser(credentials[0], credentials[1]))
                    {
                        int userID = userService.GetIDByUsername(credentials[0]);

                        if (userID != -1)
                        {
                            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                            var token = GenerateToken(secret, userID.ToString());

                            result.AddData(new 
                            {
                                token = token
                            }, HttpStatusCode.Created);
                        }
                        else
                        {
                            result.AddException("Something went wrong with the registration.", 
                                ExceptionCode.Unknown);
                        }
                    }
                    else
                    {
                        result.AddException($"Username '{credentials[0]}' is already taken.", 
                            ExceptionCode.UserTaken);
                    }
                }
                catch (Exception)
                {
                    result.AddException("Bad payload!", ExceptionCode.Unknown);
                }
            }

            return result;
        }

        public static ResultContext LoginUser(ResultContext result, UserService userService, 
            string authorizationHeader, string secretKey)
        {
            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                result.AddException("Authorization header is required!", ExceptionCode.MissingHeader);
            }
            else
            {
                try 
                {
                    var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader)).Split(':');

                    var user = userService.GetByUsername(credentials[0]);

                    if (user != null)
                    {
                        if (user.Password == credentials[1])
                        {
                            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                            var token = GenerateToken(secret, user.UserID.ToString());

                            result.AddData(new 
                            {
                                token = token
                            });
                        }
                        else
                        {
                            result.AddException("Wrong password!", ExceptionCode.BadHeader);
                        }
                    }
                    else
                    {
                        result.AddException("This user doesn't exist.", ExceptionCode.UnknownUser);
                    }
                }
                catch (Exception)
                {
                    result.AddException("Bad payload!", ExceptionCode.Unknown);
                }
            }

            return result;
        }

        private static string GenerateToken(SecurityKey key, string id)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", id) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            });

            return handler.WriteToken(token);
        }
    }
}