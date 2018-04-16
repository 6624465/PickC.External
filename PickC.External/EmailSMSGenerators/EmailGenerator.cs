using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using System.Web.Configuration;
using System.Net;
using System.Text;
using System.IO;

namespace PickC.External.EmailSMSGenerators
{
    public class EmailGenerator
    {
        Configuration config;
        public EmailGenerator()
        {
            config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        }

        public bool SendMail(MailMessage msg)
        {
            SmtpSection settings = (SmtpSection)ConfigurationManager.GetSection("mailSettings/smtp_1");
            if (msg.CC[0].ToString().ToLower() == "contact@pickcargo.in")
            {
                settings = (SmtpSection)ConfigurationManager.GetSection("mailSettings/smtp_2");
            }
            else if (msg.CC[0].ToString().ToLower() == "feedback@pickcargo.in")
            {
                settings = (SmtpSection)ConfigurationManager.GetSection("mailSettings/smtp_3");
            }

            msg.From = new MailAddress(settings.From, "PickC");
            SmtpClient client = new SmtpClient(settings.Network.Host);
            client.Port = settings.Network.Port;
            client.EnableSsl = settings.Network.EnableSsl;
            client.Timeout = 900000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = settings.Network.DefaultCredentials;
            client.Credentials = new NetworkCredential(settings.Network.UserName, settings.Network.Password);
            client.Send(msg);
            return true;


            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            //msg.From = new MailAddress(settings.Smtp.From, "PickC");
            //SmtpClient client = new SmtpClient(settings.Smtp.Network.Host);
            //client.Port = settings.Smtp.Network.Port;
            //client.EnableSsl = settings.Smtp.Network.EnableSsl;
            //client.Timeout = 900000;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = settings.Smtp.Network.DefaultCredentials;
            //client.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
            //client.Send(msg);
            //return true;
        }

        public bool ConfigMail(string to, string cc, string bcc, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.CC.Add(new MailAddress(cc));
            msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);
        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body, byte[] attBytes)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            Attachment att = new Attachment(new MemoryStream(attBytes), "PickInvoice.pdf");
            msg.Attachments.Add(att);

            return SendMail(msg);
        }

        public bool ConfigMail(string to, string bcc, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));

            msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string cc, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.CC.Add(new MailAddress(cc));

            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

    }
}