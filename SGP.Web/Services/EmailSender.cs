using Microsoft.Extensions.Options;
using MimeKit;
using SGP.Core.Interfaces.IServices;
using MailKit.Net.Smtp;

namespace SGP.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(List<string> emails, string subject, string message)
        {
            try
            {
                var devEmail = _configuration.GetSection("EmailSendConfig:Email").Value;

                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.FromEmail));


                if (_emailSettings.Environment == "Production")
                {
                    foreach (var email in emails)
                        mimeMessage.To.Add(new MailboxAddress(email, email));
                }
                else
                {
                    string emailIds = "";
                    foreach (var email in emails)
                    {
                        emailIds += $"<span>{email}</span>, ";
                    }

                    string ToCc = $"<p>To: {emailIds.TrimEnd(' ', ',')} </p><br> ";
                    message = message.Insert(0, ToCc);
                }

                mimeMessage.Bcc.Add(new MailboxAddress(devEmail, devEmail));
                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                        //await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                    }
                    else
                    {
                        //await client.ConnectAsync(_emailSettings.MailServer);
                        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    if (_emailSettings.Environment == "Test")
                    {
                        await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
                    }

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task SendEmailAsync(List<string> emails, string subject, string message, List<string> ccemails)
        {
            try
            {
                var devEmail = _configuration.GetSection("EmailSendConfig:Email").Value;

                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.FromEmail));


                if (_emailSettings.Environment == "Production")
                {
                    foreach (var email in emails)
                        mimeMessage.To.Add(new MailboxAddress(email, email));
                    if (ccemails != null)
                    {
                        foreach (var ccemail in ccemails)
                            mimeMessage.Cc.Add(new MailboxAddress(ccemail, ccemail));
                    }
                }
                else
                {
                    string emailIds = "";
                    string ccIds = "";
                    foreach (var email in emails)
                    {
                        emailIds += $"<span>{email}</span>, ";
                    }

                    if (ccemails != null)
                    {
                        foreach (var ccemail in ccemails)
                        {
                            ccIds += $"<span>{ccemail}</span>, ";
                        }
                    }

                    string ToCc = $"<p>To: {emailIds.TrimEnd(' ', ',')} </p><p>cc: {ccIds.TrimEnd(' ', ',')}</p><br> ";
                    message = message.Insert(0, ToCc);
                }

                //  mimeMessage.Cc.Add(new MailboxAddress(devEmail, devEmail));
                mimeMessage.Bcc.Add(new MailboxAddress(devEmail, devEmail));
                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                        //await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                    }
                    else
                    {
                        //await client.ConnectAsync(_emailSettings.MailServer);
                        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                    }


                    // Note: only needed if the SMTP server requires authentication
                    if (_emailSettings.Environment == "Test")
                    {
                        await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
                    }



                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string Environment { get; set; }
        public string SenderName { get; set; }
        public string FromEmail { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }
}
