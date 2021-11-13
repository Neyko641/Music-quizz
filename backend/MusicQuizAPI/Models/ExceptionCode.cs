namespace MusicQuizAPI.Models
{
    public enum ExceptionCode
    {
        // Basic
        Unknown,
        Unauthorized,
        Unavailable,

        // Arguments
        MissingArgument,
        BadArgument,
        AlreadyInOrDoesNotExistArgument,

        // Headers
        MissingHeader,
        BadHeader,

        // User
        UnknownUser,
        UserTaken
    }
}