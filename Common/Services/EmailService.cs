
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using Shared.Interfaces.Services;
using Shared.DTOs.Email;
using Shared.Exceptions;
using Shared.Settings;

namespace Common.Services
{

    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(MailSettings mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings;
            _logger = logger;
        }

        public System.Threading.Tasks.Task SendAsync(EmailRequest request)
        {
            //try
            //{
            //    string exchangeUrl = _mailSettings.ExchangeURL;
            //    ExchangeService ews = new ExchangeService(ExchangeVersion.Exchange2016)
            //    {
            //        Credentials = new WebCredentials(_mailSettings.SmtpUser, _mailSettings.SmtpPass),
            //        Url = new Uri(exchangeUrl),

            //    };

            //    EmailMessage emailMessage = new EmailMessage(ews);
            //    emailMessage.ToRecipients.Add(request.To);
            //    emailMessage.From = request.From ?? _mailSettings.EmailFrom;
            //    emailMessage.Body = request.Body;
            //    emailMessage.Subject = request.Subject;
            //    emailMessage.SendAndSaveCopy();
            //    return System.Threading.Tasks.Task.CompletedTask;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Exception : {ex.Message}" +
            //             $"Inner exception : {ex.InnerException?.Message}" +
            //             $"Nested Inner exception : {ex.InnerException?.InnerException?.Message}", ex);
            //    throw new ApiException($"Exception : {ex.Message}" +
            //             $"Inner exception : {ex.InnerException?.Message}" +
            //             $"Nested Inner exception : {ex.InnerException?.InnerException?.Message}");
            //}
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(request.To));
            msg.From = new MailAddress(_mailSettings.EmailFrom, "No reply");
            msg.Subject = request.Subject;
            msg.Body = request.Body;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            client.Port = _mailSettings.SmtpPort;
            client.Host = _mailSettings.SmtpHost; // "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
                return System.Threading.Tasks.Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApiException($"Exception : {ex.Message}" +
                   $"Inner exception : {ex.InnerException?.Message}" +
                         $"Nested Inner exception : {ex.InnerException?.InnerException?.Message}");
            }
        }

        //public MailSettings _mailSettings { get; }
        //public ILogger<EmailService> _logger { get; }

        //public EmailService(MailSettings mailSettings, ILogger<EmailService> logger)
        //{
        //    _mailSettings = mailSettings;
        //    _logger = logger;
        //}

        //public System.Threading.Tasks.Task SendAsync(EmailRequest request)
        //{
        //    try
        //    {
        //        string exchangeUrl = _mailSettings.ExchangeURL;
        //        ExchangeService ews = new ExchangeService(ExchangeVersion.Exchange2013)
        //        {
        //            Credentials = new WebCredentials(_mailSettings.SmtpUser, _mailSettings.SmtpPass),
        //            Url = new Uri(exchangeUrl),
        //        };

        //        EmailMessage emailMessage = new EmailMessage(ews);
        //        emailMessage.ToRecipients.Add(request.To);
        //        emailMessage.From = request.From ?? _mailSettings.EmailFrom;
        //        emailMessage.Body = request.Body;
        //        emailMessage.Subject = request.Subject;
        //        emailMessage.SendAndSaveCopy();
        //        return System.Threading.Tasks.Task.CompletedTask;

        //    }
        //    catch (System.Exception ex)
        //    {
        //        _logger.LogError(ex.Message, ex);
        //        throw new ApiException(ex.Message);
        //    }
        //}
    }
}
