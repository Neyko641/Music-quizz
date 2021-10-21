using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models
{
    [NotMapped]
    public class ResultContext<T>
    {
        public int StatusCode { get; private set; } = 200;
        public T Data { get; private set; }
        private readonly List<string> _exceptionMessages;

        public ResultContext(T data) : this()
        {
            Data = data;
        }

        public ResultContext()
        {
            _exceptionMessages = new List<string>();
        }

        public bool IsOk() => StatusCode == 200;

        public bool AddData(T data)
        {
            if (Data == null) 
            {
                Data = data;
                return true;
            }
            return false;
        }

        public void AddExceptionMessage(string message)
        {
            _exceptionMessages.Add(message);
            StatusCode = 400;
        }

        public bool AddServiceUnavailableMessage(string message)
        {
            if (StatusCode == 503 || StatusCode == 200)
            {
                _exceptionMessages.Add(message);
                StatusCode = 503;
                return true;
            }
            return false;
        }

        public object Result()
        { 
            switch (StatusCode)
            {
                case 200:
                    return new {
                        status = StatusCode,
                        result = Data
                    };
                case 503:
                    return new {
                        status = StatusCode,
                        result = _exceptionMessages[0]
                    };
                default:
                    return new {
                        status = 400,
                        errors = _exceptionMessages
                    };
            }
        }

    }

    public class ResultContext : ResultContext<object> {}
}