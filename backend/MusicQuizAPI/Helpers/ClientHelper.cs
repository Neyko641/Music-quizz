
using Microsoft.AspNetCore.Http;
using MusicQuizAPI.Extensions;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Services;

namespace MusicQuizAPI.Helpers
{
    public static class ClientHelper
    {
        public static string GetClientIPAdress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString();
        }

        /// <summary>
        /// Returns the User which ID is equal to the one set in the JWT Token payload
        /// </summary>
        public static User GetUserFromHttpContext(HttpContext context, UserService service)
        {
            string strId = context.User.Identity.GetClaim("id");
            int id;

            if (int.TryParse(strId, out id)) return service.GetByID(id);

            return null;
        }
    }
}