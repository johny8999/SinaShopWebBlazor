using FrameWork.Application.Services.Email;
using FrameWork.Consts;
using FrameWork.Infrastructure;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SinaShop.Infrastructure.Services.Email
{
    public class GmailSender : IEmailSender
    {
        #region EmailDetails
        private readonly string _SendTitle;
        private readonly string _UserName;
        private readonly string _Password;
        private readonly int _Port;
        private readonly bool _UseSsl;
        #endregion EmailDetails

        private readonly ILogger _logger;

        public GmailSender(ILogger logger)
        {
            _SendTitle = SiteSettingConst.SiteName;
            _UserName = "sinaalipour77@gmail.com";
            _Password = "123sinaSINA";
            _Port = 587;
            _UseSsl = true;

            _logger = logger;
        }

        public bool Send(string to, string subject, string message)
        {
            try
            {
                MailMessage Mail = new();
                Mail.From = new MailAddress(_UserName, _SendTitle, Encoding.UTF8);
                Mail.Subject = subject;
                Mail.To.Add(new MailAddress(to));
                Mail.Body = message;
                Mail.IsBodyHtml = true;
                Mail.BodyEncoding = Encoding.UTF8;
                Mail.Priority = MailPriority.Normal;

                SmtpClient Smtp = new("smtp.gmail.com", _Port);
                Smtp.EnableSsl = _UseSsl;
                Smtp.Credentials = new NetworkCredential(_UserName, _Password);
                Smtp.Send(Mail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public async Task SendAsync(string to, string subject, string message)
        {
            try
            {
                MailMessage Mail = new();
                Mail.From = new MailAddress(_UserName, _SendTitle, Encoding.UTF8);
                Mail.Subject = subject;
                Mail.To.Add(new MailAddress(to));
                Mail.Body = message;
                Mail.IsBodyHtml = true;
                Mail.BodyEncoding = Encoding.UTF8;
                Mail.Priority = MailPriority.Normal;

                SmtpClient Smtp = new("smtp.gmail.com", _Port);
                Smtp.EnableSsl = _UseSsl;
                Smtp.Credentials = new NetworkCredential(_UserName, _Password);

                Smtp.SendCompleted += new SendCompletedEventHandler(SendCompleteCallBack);

                Smtp.SendAsync(Mail, to);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void SendCompleteCallBack(object sender, AsyncCompletedEventArgs args)
        {
            try
            {
                string Token = args.UserState.ToString();
                if (args.Cancelled)
                {

                }
                else if (args.Error is not null)
                {
                    throw new Exception($"Token:[{Token}], Error[{args.Error.Message}]");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
