
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
    }
}