using Microsoft.AspNetCore.Identity;

namespace UsersAPI.Models
{
    public class CustomIdentityUser<TKey> : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public DateTime DateOfBirth { get; set; }
    }
}
