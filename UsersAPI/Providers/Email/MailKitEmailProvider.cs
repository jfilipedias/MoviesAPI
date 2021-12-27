using MailKit.Net.Smtp;
using MimeKit;
using UsersAPI.Models;

namespace UsersAPI.Providers
{
    public class MailKitEmailProvider : IEmailProvider
    {
        private IConfiguration _configuration;

        public MailKitEmailProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(Message message)
        {
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
            var from = new MailboxAddress(_configuration.GetValue<string>("EmailSettings:Name"), 
                _configuration.GetValue<string>("EmailSettings:From"));
            
            var to = message.To.Select(user => new MailboxAddress(user.Username, user.Email));
            
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(from);
            emailMessage.To.AddRange(to);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };

            return emailMessage;
        }
    }
}
