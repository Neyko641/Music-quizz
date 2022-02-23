
// Represents a list of classes which hold different types of exceptions

namespace MusicQuizAPI.Exceptions
{
    #region Basic
    public class UnexpectedException : BaseException
    {
        public UnexpectedException(string msg) : base((int)ExceptionCode.Unexcpected, msg) { }
    }

    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string msg) : base((int)ExceptionCode.Unauthorized, msg) { }
    }

    public class UnavailableException : BaseException
    {
        public UnavailableException(string msg) : base((int)ExceptionCode.Unavailable, msg) { }
    }
    #endregion



    #region Arguments
    public class BadArgumentException : BaseException
    {
        public BadArgumentException(string msg) : base((int)ExceptionCode.BadArgument, msg) { }
    }
    #endregion



    #region Headers
    public class MissingHeaderException : BaseException
    {
        public MissingHeaderException(string msg) : base((int)ExceptionCode.MissingHeader, msg) { }
    }

    public class BadHeaderException : BaseException
    {
        public BadHeaderException(string msg) : base((int)ExceptionCode.BadHeader, msg) { }
    }
    #endregion



    #region Data
    public class AlreadyExistException : BaseException
    {
        public AlreadyExistException(string msg) : base((int)ExceptionCode.AlreadyExist, msg) { }
    }

    public class NotExistException : BaseException
    {
        public NotExistException(string msg) : base((int)ExceptionCode.NotExist, msg) { }
    }
    #endregion
}