namespace MusicQuizAPI.Exceptions
{
    public enum ExceptionCode
    {
        // Basic
        Unexcpected,

        // Arguments
        BadArgument,

        // Headers
        MissingHeader = 20,
        BadHeader,

        // Data
        AlreadyExist = 10,
        NotExist,

        // Already defined
        Unauthorized = 401,
        Unavailable = 503,
    }
}