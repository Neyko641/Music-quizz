
using Microsoft.AspNetCore.Http;
namespace MusicQuizAPI.Helpers
{
    public static class ClientHelper
    {
        public static string GetClientIPAdress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}