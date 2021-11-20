using System;

namespace MusicQuizAPI.Exceptions
{
    public class BaseException : Exception
    {
        public int Status { get; }

        public BaseException(int status, string msg) : base(msg) 
        {
            Status = status;
        }
    }
}