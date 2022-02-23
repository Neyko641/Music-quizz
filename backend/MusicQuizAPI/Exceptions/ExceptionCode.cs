namespace MusicQuizAPI.Exceptions
{
    public enum ExceptionCode
    {
        // Basic
        Unexcpected,

        // Arguments
        BadArgument = 10,

        // Headers
        MissingHeader = 20,
        BadHeader,

        // Data
        AlreadyExist = 30,
        NotExist,

        // Already defined
        Unauthorized = 401,
        Unavailable = 503,
    }
}