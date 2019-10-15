using Architecture.Base;
using Architecture.Common.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Architecture.Helper
{
    public class EMailClass: ObjectHelperBase
    {
        private const string className = "Architecture.CommonMethods.EMailClass";
        public GenericResponse<bool> SendEmail(string fromEmail, string password, string host, int port, string ToEmail, string subject, string body)
        {
            GenericResponse<bool> returnObject;
            returnObject = InitializeGenericResponse<bool>(className + ".GetTextFileRead");

            try
            {
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail);
                mailMessage.To.Add(new MailAddress(ToEmail));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Credentials = new NetworkCredential(fromEmail, password);
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);

                return returnObject;
            }
            catch (Exception e)
            {                
                returnObject.Results.Add(e);
                return returnObject;
            }
        }

        public   GenericResponse<Boolean> SendEmail(string fromEmail, string password, string host, int port, List<string> ToEmailList, string subject, string body)
        {
            GenericResponse<Boolean> returnObject;
            returnObject = InitializeGenericResponse<Boolean>(className + ".GetTextFileRead");

            try
            {
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail);
                foreach (var item in ToEmailList)
                {
                    mailMessage.To.Add(item);
                }

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Credentials = new NetworkCredential(fromEmail, password);
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);

                return returnObject;
            }
            catch (Exception e)
            {
                returnObject.Results.Add(e);
                return returnObject;
            }



        }

    }
}
