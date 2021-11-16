using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class UnrequiredUserIDParamModel
    {
        [Range(-1, 1000000, ErrorMessage = "'id' must be positive number!")] 
        public int ID { get; set; } = -1;
    }
}