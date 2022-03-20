using DemoKhaled.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Helper
{
    public static class MailSender
    {
        

        public static string SendMail(MailMV model)
        {
            try

            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);//this is the host which we will use to send mails
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("khaledahmed20170196@gmail.com", "khaledahmed0111");
                //                                    model.Mail===> default value in MailVM
                smtp.Send("khaledahmed20170196@gmail.com", model.Mail, model.Title, model.Message);

                return "Email Sent Succefully";
            }
            catch (Exception ex)
            {
               
                return "Failed";
            }
          
        }
    }
}
