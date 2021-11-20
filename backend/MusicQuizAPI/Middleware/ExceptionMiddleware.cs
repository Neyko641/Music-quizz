using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Exceptions;

namespace MusicQuizAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UnexcpectedException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (UnauthorizedException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (UnavailableException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (BadArgumentException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (MissingHeaderException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (BadHeaderException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (AlreadyExistException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (NotExistException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleDefaultExceptionAsync(httpContext, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, BaseException exception)
        {
            context.Response.StatusCode = exception.Status;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails(context.Response.StatusCode,
                exception.Message).ToString());
        }

        private async Task HandleDefaultExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError("Unexpected Error happened!\n" + exception.Message);

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails(context.Response.StatusCode,
                "Unexpected Error happened!").ToString());
        }
    }
}