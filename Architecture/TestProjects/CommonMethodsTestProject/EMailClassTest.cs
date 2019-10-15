using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Helper;

namespace Architecture.Test.Common
{
    [TestClass()]
    public class EMailClassTest
    {
        [TestMethod()]
        public void SendEmail()
        {
            /*
             SMTP Host: smtp.gmail.com
             SMTP Port: 587
             SSL Protocol: OFF
             TLS Protocol: ON
             */
            /*
             SMTP Host: smtp-mail.outlook.com
             SMTP Port: 587
             SSL Protocol: OFF
             TLS Protocol: ONsmtp-mail.outlook.com 
            */
            var bodyText = "<p>Email From: CoNetINF Application </p><p>Message:</p><p>Nasılsın</p>";
            var body=string.Format(bodyText, "elalmis.ne@gmail.com", "", "Merhaba Nasılsın");
           
            var mailList = new List<string> {
                "elalmis.ne@gmail.com",
                "nvzt_23@outlook.com"

            };
            EMailClass e = new EMailClass();
            var acctual=e.SendEmail("elalmis.ne@gmail.com", "parola", "smtp.gmail.com", 587, mailList,"Konu",body);
            if (!acctual.Success)
                Assert.Fail(acctual.Results.FirstOrDefault().Message);
            
            Assert.AreEqual(true, acctual.Success);

        }
    }
}