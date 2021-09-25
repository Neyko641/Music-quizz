using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MusicQuizAPI.Models.Middleware
{
    public class ControllersCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        public ControllersCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!Settings.AreControllersAvailable)
            {
                var result = new ResultModel<object>();
                result.AddServiceUnavailableMessage("MusicQuiz API cannot be used right now. Please try again in a minute.");

                var json = JsonConvert.SerializeObject(result.Result());
                
                context.Response.StatusCode = result.StatusCode;
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