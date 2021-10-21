using System.Security.Claims;
using System.Security.Principal;

namespace MusicQuizAPI.Extensions
{
    public static class UserExtensions
    {
        public static string GetClaim(this IIdentity identity, string claim)
        {
            var user = identity as ClaimsIdentity;

            if (user != null)
            {
                return user.FindFirst(claim).Value;
            }
            
            return null;
        }
    }
}