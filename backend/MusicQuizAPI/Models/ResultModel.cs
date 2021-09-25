using System.Collections.Generic;

namespace MusicQuizAPI.Models
{
    public class ResultModel<T>
    {
        public int StatusCode { get; private set; } = 200;
        public T Data { get; private set; }
        private readonly List<string> _exceptionMessages;
        private string _message;

        public ResultModel(T data) : this()
        {
            Data = data;
        }

        public ResultModel()
        {
            _exceptionMessages = new List<string>();
        }

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

        public void AddServiceUnavailableMessage(string message)
        {
            _message = message;
            StatusCode = 503;
        }

        public object Result()
        { 
            switch (StatusCode)
            {
                case 200:
                    return new {
                        status = 200,
                        result = Data
                    };
                case 503:
                    return new {
                        status = 503,
                        result = _message
                    };
                default:
                    return new {
                        status = 400,
                        errors = _exceptionMessages
                    };
            }
        }

    }
}