using System.Collections.Generic;

namespace MusicQuizz_backend.Models
{
    public class ResultModel<T>
    {
        public int StatusCode { get; private set; } = 200;
        public T Data { get; private set; }
        private readonly List<string> _exceptionMessages;

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

        public object Result()
        { 
            if (StatusCode == 200)
            {
                return new {
                    status = 200,
                    result = Data
                };
            }
            else
            {
                return new {
                    status = 400,
                    errors = _exceptionMessages
                };
            }
        }

    }
}