using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace MusicQuizAPI.Models.Parameters
{
    [NotMapped]
    public class AuthorizationParamModel
    {
        [Required]
        [FromHeader]
        public string Authorization { get; set; }
    }
}