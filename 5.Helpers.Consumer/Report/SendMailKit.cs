using _5.Helpers.Consumer;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace _4.Helpers.Consumer.Report
{
    public class SendMailKit
    {
        private readonly IWebHostEnvironment _env;

        public SendMailKit(IWebHostEnvironment env)
        {
            _env = env;
        }

        internal async Task<string> SendMail(EmailModel model)
        {
            string result = "";
            var countTry = 0;
            bool isSuccess;
            var random = new Random();
            do
            {
                countTry++;
                try
                {
                    result = await TrySendEmail(model);
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.StackTrace + " " + ex.Message;
                    isSuccess = false;
                    int num = random.Next(500, 1000);
                    await Task.Delay(num);
                    if (countTry >= 3)
                    {
                        throw ex;
                    }
                }
            } while (!isSuccess && countTry < 3);

            return "SUCCESS " + result;
        }

        private async Task<string> TrySendEmail(EmailModel model)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(model.FromName, model.FromAddress);
            message.From.Add(from);

            foreach (var mailInfo in model.To)
            {
                message.To.Add(new MailboxAddress(mailInfo.Name, mailInfo.Address));
            }

            if (model.Cc != null)
            {
                foreach (var mailInfo in model.Cc)
                {
                    message.Cc.Add(new MailboxAddress(mailInfo.Name, mailInfo.Address));
                }
            }

            message.Subject = model.Subject;

            BodyBuilder bodyBuilder = new BodyBuilder
            {
                HtmlBody = model.HtmlBody
            };

            if (model.ListFileAttach != null)
            {
                foreach (var item in model.ListFileAttach)
                {
                    var fileAttach = Path.Combine(item);
                    bodyBuilder.Attachments.Add(fileAttach);
                }
            }

            message.Body = bodyBuilder.ToMessageBody();
            var result = "";
            using (var smtp = new SmtpClient())
            {
                smtp.MessageSent += (sender, args) =>
                {
                    result = args.Response;
                };
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                //if (model.secureSocket == 0)
                //{
                //    model.secureSocket = 1;
                //}
                //if (smtp.Capabilities.HasFlag(SmtpCapabilities.Authentication))
                //{
                //    await smtp.AuthenticateAsync(model.Username, model.Password);
                //}
                await smtp.ConnectAsync(model.Host, model.Port, (SecureSocketOptions)model.secureSocket);
                if (model.isAuth)
                {
                    await smtp.AuthenticateAsync(model.Username, model.Password);
                }
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }

            return result;
        }
    }
}
