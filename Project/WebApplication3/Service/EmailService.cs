using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CodeFirst;
using WebApplication3.App_Code;

namespace WebApplication3.Service
{
    public class EmailService : IIdentityMessageService
    {  
        public Task SendAsync(IdentityMessage message)
        {
            MailMessage m = new System.Net.Mail.MailMessage(
            new MailAddress(ConfigurationManager.AppSettings["mailAdmin"], "Moodle"),
            new MailAddress(message.Destination))
            {  
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };
            SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["mail"], 587);
            smtp.Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["mailAdmin"],
                ConfigurationManager.AppSettings["mailPassword"]);
            smtp.EnableSsl = true;
            smtp.Send(m);

            return Task.FromResult(0);
        }
    }
}