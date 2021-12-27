namespace UsersAPI.Models
{
    public class Message
    {
        public List<User> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<User> users, string subject, int userId, string content)
        {
            To = new List<User>();
            To.AddRange(users);
            Subject = subject;
            Content = $"http://localhost:7165/activates?userId={userId}&activationCode={content}";
        }
    }
}
