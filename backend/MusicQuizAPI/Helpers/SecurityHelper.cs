using System;
using System.Net;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Exceptions;

namespace MusicQuizAPI.Helpers
{
    public static class SecurityHelper
    {
        public static string RegisterUser(UserService userService, string authorizationHeader, 
            string secret, string username)
        {
            try 
            {
                // email = credentials[0]   password = credentials[1]
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader)).Split(':');

                if (!IsValidEmail(credentials[0]))
                {
                    throw new BadHeaderException("Email is not valid!");
                }
                else if (string.IsNullOrEmpty(credentials[1]))
                {
                    throw new BadHeaderException("Password is not valid!");
                }

                if (userService.RegisterUser(
                    credentials[0].ToLower(),                       // Email
                    BCrypt.Net.BCrypt.HashPassword(credentials[1]), // Hashed Password
                    username))
                {
                    int userID = userService.GetIDByEmail(credentials[0]);

                    if (userID != -1)
                    {
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

                        string token = GenerateToken(key, userID.ToString());

                        return token;
                    }
                    else
                    {
                        throw new UnexcpectedException("Something went wrong after the registration.");
                    }
                }
                else
                {
                    throw new UnexcpectedException("Something went wrong before the registration.");
                }
            }
            catch (FormatException)
            {
                throw new UnexcpectedException("Bad payload!");
            }
        }

        public static string LoginUser(UserService userService, string authorizationHeader, string secret)
        {
            try 
            {
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader)).Split(':');

                if (string.IsNullOrEmpty(credentials[1]))
                {
                    throw new BadHeaderException("Password is not valid!");
                }

                var user = userService.GetByEmail(credentials[0]);

                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(credentials[1], user.Password))
                    {
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

                        var token = GenerateToken(key, user.UserID.ToString());

                        return token;
                    }
                    else
                    {
                        throw new BadHeaderException("Wrong password or email!");
                    }
                }
                else
                {
                    throw new NotExistException("This user doesn't exist.");
                }
            }
            catch (FormatException)
            {
                throw new UnexcpectedException("Bad payload!");
            }
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

        private static bool IsValidEmail(string email)
        {
            // https://emailregex.com/
            string exp = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""
                (?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*""
                )@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25
                [0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]
                *[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";

            Regex regex = new Regex(exp, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }
    }
}