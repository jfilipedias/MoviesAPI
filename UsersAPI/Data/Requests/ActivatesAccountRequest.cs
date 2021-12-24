using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Data.Requests
{
    public class ActivatesAccountRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ActivationCode { get; set; }
    }
}
