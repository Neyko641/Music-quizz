using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MusicQuizAPI.Models;
using System.Net;

namespace MusicQuizAPI.Middleware
{
    public class ControllersCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        ResultContext Result { get; set; }

        public ControllersCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
            Result = new ResultContext();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!Settings.AreControllersAvailable)
            {
                Result.AddException("MusicQuiz API cannot be used right now. Please try again in a minute.", 
                    ExceptionCode.Unavailable, HttpStatusCode.ServiceUnavailable);

                var json = JsonConvert.SerializeObject(Result.Result());
                
                context.Response.StatusCode = (int)Result.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            else
            {
                await _next(context);
            }
        }
    }
}