using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MusicQuizAPI.Models
{
    [NotMapped]
    public class ResultContext<T>
    {
        public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;
        public T Data { get; private set; }
        public string Info { get; set; } = "";
        private readonly List<ExceptionModel> _exceptions = new List<ExceptionModel>();


        public ResultContext(T data)
        {
            Data = data;
        }
        public ResultContext() { }

        

        public bool IsOk() => StatusCode == HttpStatusCode.OK;

        public bool AddData(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (Data == null) 
            {
                Data = data;
                return true;
            }
            return false;
        }

        public void AddException(string message, ExceptionCode exceptionCode, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            StatusCode = statusCode;
            _exceptions.Add(new ExceptionModel
            {
                Code = exceptionCode,
                Message = message,
            });
            
        }

        public IActionResult Result()
        {
            object result;

            if ((int)StatusCode < 400) result = new 
            {
                status = StatusCode,
                result = Data
            };
            else result = new 
            {
                status = StatusCode,
                result = _exceptions
            };

            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(result);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(result);
                case HttpStatusCode.Created:
                    return new CreatedResult(Info, result);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(result);
                // More to be added
                default:
                    return new StatusCodeResult((int)StatusCode);
            }
        }

    }

    public class ResultContext : ResultContext<object> {}
}