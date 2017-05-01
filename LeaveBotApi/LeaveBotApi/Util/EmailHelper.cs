using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace LeaveBotApi.Util
{
    public class EmailHelper
    {
        public string To { get; set; }
        public string From { get; set; }

        public void send(string Subject, string Message)
        {
            try
            {
                if (To == null || To == String.Empty || From == null || From == String.Empty)
                {
                    throw new Exception("To address not configured");
                }
                int Port = int.Parse(ConfigurationManager.AppSettings["MailPort"] ?? "25");
                string Host = ConfigurationManager.AppSettings["MailHost"] ?? "smtp.google.com";
                MailMessage mail = new MailMessage(To, From);
                SmtpClient client = new SmtpClient();
                client.Port = Port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailUsername"], ConfigurationManager.AppSettings["MailPassword"]);
                client.Host = Host;
                mail.Subject = Subject;
                mail.Body = Message;
                client.Send(mail);
            }
            catch
            {
                throw;
            }
        }
    }
}