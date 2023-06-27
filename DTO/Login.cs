using System.ComponentModel.DataAnnotations;

namespace LoopApi.DTO
{
    public class Login
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public bool rememberMe { get; set; } = false;
        [Required]
        public string language { get; set; }
    }
}
