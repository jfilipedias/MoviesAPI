using MimeKit;

namespace UsersAPI.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<User> users, string subject, int userId, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(users.Select(user => new MailboxAddress(user.Username, user.Email)));
            Subject = subject;
            Content = $"http://localhost:7165/activates?userId={userId}&activationCode={content}";
        }
    }
}
