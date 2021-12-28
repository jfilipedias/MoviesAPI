using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Data.Requests
{
    public class RequireResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
