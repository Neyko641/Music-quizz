using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Helpers;

namespace MusicQuizAPI.Filters
{
    public class LogFilter : TypeFilterAttribute
    {
        public LogFilter() : base(typeof(LogFilterChild)) {}

        private class LogFilterChild : IActionFilter
        {
            private readonly ILogger _logger;

            public LogFilterChild(ILoggerFactory loggerFactory)
            {
                _logger = loggerFactory.CreateLogger<LogFilter>();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var address = ClientHelper.GetClientIPAdress(context.HttpContext);
                _logger.LogTrace($"[GET] request from {address}!");
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                var address = ClientHelper.GetClientIPAdress(context.HttpContext);
                
                string status = ((context.Result as ObjectResult)?.StatusCode == 200) ? "OK" : "BAD";
                
                _logger.LogTrace($"[GET] ({status}) response to {address}!");
            }
        }
    }
}