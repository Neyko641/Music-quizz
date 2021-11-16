namespace MusicQuizAPI.Models
{
    public enum ExceptionCode
    {
        // Basic
        Unknown,
        Unauthorized,
        Unavailable,

        // Arguments
        MissingArgument = 10,
        BadArgument,
        AlreadyInOrDoesNotExistArgument,

        // Headers
        MissingHeader = 20,
        BadHeader,

        // Users
        UnknownUser = 30,
        UserTaken
    }
}