using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class LoginDto
    {
        [Required]
        public string  username { get; set; }
        [Required]
        public string password {get;set;}
    }
}