using System.ComponentModel.DataAnnotations;

namespace LoopApi.DTO
{
    public class UserDTO
    {
        public long id { get; set; }
        public string login { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string birthDate { get; set; }
        public string langKey { get; set; }
    }
}
