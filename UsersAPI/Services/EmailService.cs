using MailKit.Net.Smtp;
using MimeKit;
using UsersAPI.Models;

namespace UsersAPI.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(User[] users, string subject, int userId, string emailConfirmationToken)
        {
            var message = new Message(users, subject, userId, emailConfirmationToken);
            var emailMessage = CreateEmailBody(message);
            Send(emailMessage);
        }

        private void Send(MimeMessage emailMessage)
        {
            var client = new SmtpClient();
            
            try
            {
                client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), 
                    _configuration.GetValue<int>("EmailSettings:Port"), true);
                client.AuthenticationMechanisms.Remove("XOUATH2");
                client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                    _configuration.GetValue<string>("EmailSettings:Password"));

                client.Send(emailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        private MimeMessage CreateEmailBody(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:Name"), 
                _configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };

            return emailMessage;
        }
    }
}