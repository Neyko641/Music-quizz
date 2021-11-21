using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MusicQuizAPI.Models;
using MusicQuizAPI.Exceptions;


namespace MusicQuizAPI.Middleware
{
    public class ControllersCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        ResponseContext Result { get; set; }

        public ControllersCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
            Result = new ResponseContext();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!Settings.AreControllersAvailable)
            {
                context.Response.StatusCode = (int)ExceptionCode.Unavailable;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new ErrorDetails((int)ExceptionCode.Unavailable,
                    "API cannot be used right now. Please try again later.").ToString());
            }
            else
            {
                await _next(context);
            }
        }
    }
}