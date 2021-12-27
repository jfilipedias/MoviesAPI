using UsersAPI.Models;

namespace UsersAPI.Providers
{
    public interface IEmailProvider
    {
        public void SendEmail(Message message);
    }
}
