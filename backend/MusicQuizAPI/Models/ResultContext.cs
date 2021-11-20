using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json;

namespace MusicQuizAPI.Models
{
    [NotMapped]
    public class ResponseContext<T>
    {
        public T Data { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }


        public ResponseContext(T data)
        {
            Data = data;
        }
        public ResponseContext() { }

        

        public bool AddData(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (Data == null) 
            {
                StatusCode = statusCode;
                Data = data;
                return true;
            }
            return false;
        }

        public object Body => new 
        {
            status = StatusCode,
            result = Data
        };

    }

    public class ResponseContext : ResponseContext<object> {}
}