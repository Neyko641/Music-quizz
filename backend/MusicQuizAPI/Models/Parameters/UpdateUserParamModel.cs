using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class UpdateUserParamModel
    {
        [RegularExpression("^[a-zA-Z0-9]([._-](?![._-])|[a-zA-Z0-9]){3,18}[a-zA-Z0-9]$")] 
        /*
        - Username consists of alphanumeric characters (a-zA-Z0-9), lowercase, or uppercase.
        - Username allowed of the dot (.), underscore (_), and hyphen (-).
        - The dot (.), underscore (_), or hyphen (-) must not be the first or last character.
        - The dot (.), underscore (_), or hyphen (-) does not appear consecutively, e.g., java..regex
        - The number of characters must be between 5 to 20.

        https://mkyong.com/regular-expressions/how-to-validate-username-with-regular-expression/
        */
        public string Username { get; set; }

        [Url]
        public string Avatar { get; set; }

        [Required]
        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
}