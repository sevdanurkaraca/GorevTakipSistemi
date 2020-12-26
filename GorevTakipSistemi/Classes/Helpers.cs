using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
namespace GorevTakipSistemi.Classes
{
    public class Helpers
    {
        public static bool MailGonder(string prmTo, string prmBody, string prmSubject)
        {
            bool kontrol = true;

            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("nurkaracasevda@gmail.com");//mailin kimden gittiği
                mail.To.Add(prmTo);//mail kime gidecek
                mail.Subject = prmSubject; //mail konu
                mail.Body = prmBody; //mail içerik kısmı
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); //587 port
                smtp.Credentials = new NetworkCredential("nurkaracasevda@gmail.com", "sevda3636");

                smtp.EnableSsl = true;

                smtp.Send(mail);

            }
            catch(Exception ex)
            { 
                kontrol = false;
            }

            return kontrol;
        }
    }
}