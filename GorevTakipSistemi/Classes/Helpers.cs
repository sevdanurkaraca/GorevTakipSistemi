using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using GorevTakipSistemi;

namespace GorevTakipSistemi.Classes
{
    public class Helpers
    {
        //Mail gonderme metodu
        public static bool MailGonder(string prmTo, string prmBody, string prmSubject)
        {
            bool kontrol = true; //mail gönderiminin başarılı mı başarısız mı olduğu anlayabilmek için. Bşarısız olursa catch a düşecek

            try
            {
                MailMessage mail = new MailMessage(); //system.net.mail; kullanıyoruz

                mail.From = new MailAddress("nurkaracasevda@outlook.com");//mailin kimden gittiği --> From : Mailin kimden gönderilecegi bilgisini tutar. MailAddress türünden bir degisken istemektedir.
                mail.To.Add(prmTo);//mail kime gidecek
                mail.Subject = prmSubject; //mail konu
                mail.Body = prmBody; //mail içerik kısmı
                mail.IsBodyHtml = true; //uygulamada gönderilecek olan mailin içeriği bir HTML form görüntüsü şeklinde olacapı için IsBodyHtml = true seçiyoruz

                //SMTPClient: E-Posta'nin gönderilecegi SMTP sunucu ve gönderen kullanıcının bilgilerinin yazılıp, MailMessage türünde oluşturulan mailin gönderildiği sınıftır
                //Credentials: E-Posta'yı gönderen kullanıcının kimlik bilgilerini tutar. -->gerçek bir mail ve şifresini yazıyoruz ve bunların yanlşsız yazılması gerekir
                SmtpClient smtp = new SmtpClient("smtp.outlook.com", 587); //587 port ve outlook smtp ayarları kullanılıyor
                smtp.Credentials = new NetworkCredential("nurkaracasevda@outlook.com", "sevda3636");

                smtp.EnableSsl = true;//outlook ta SSL kontrolü olduğu için True seçiyoruz

                smtp.Send(mail);//mailin gönderilmesi

            }
            catch(Exception ex)
            { 
                kontrol = false;
            }

            return kontrol;
        }

        //Dosya yükleme metodu
        public static bool DosyaYukle(FileUpload prmFileUpload, HttpServerUtility prmServer, string prmGuid, DBConnection con, SqlTransaction tra)
        {
            bool ret = true;

            try
            {
                IList<HttpPostedFile> SecilenDosyalar = prmFileUpload.PostedFiles;

                Dosya d = new Dosya();

                for(int i = 0; i < SecilenDosyalar.Count; i++)
                {
                    d.GorevGuid = prmGuid;
                    d.DosyaAdi = prmFileUpload.PostedFiles[i].FileName;
                    d.Path = prmServer.MapPath("Dosya/") + System.IO.Path.GetExtension(prmFileUpload.PostedFiles[i].FileName);

                    //dosya kayıt
                    if (d.Kayit(con, tra))
                    { 
                        d.ID = -1;
                        prmFileUpload.PostedFiles[i].SaveAs(d.Path); //dosyanun servera uploadı
                    }
                }
            }
            catch
            {
                ret = false;
            }


            return ret;
        }

        //Görev durumlarının hesaplandığı metot
        public static void DurumToplam()
        {
            System.Web.UI.Page page = HttpContext.Current.CurrentHandler as System.Web.UI.Page; //hangi sayfada olunduğunun bulunması

            DBConnection con = new DBConnection();

            try
            {
                //görevlerdeki durum sayılarının hesabı
                DataTable dt = con.GetQuery(@"
SELECT 
g.Durum
, gl.TeknisyenID 
FROM Tbl_Gorev g 
LEFT JOIN Tbl_Gorevli gl ON gl.IsSilindi = 0 AND gl.GorevID = g.ID
WHERE g.IsSilindi = 0 AND gl.TeknisyenID =  " + ((Kullanici)page.Session["Kullanici"]).ID);

                if (dt.Rows.Count > 0)//master page deki labellerin değerlerinin değiştirilmesi
                {
                    ((Site1)page.Master).LabelBaslandi = dt.Compute("COUNT(Durum)", "Durum = 1").ToString();//durum değiştir.aspx teki value değerlerine göre yerleştiriyoruz:
                    ((Site1)page.Master).LabelTamamlandi = dt.Compute("COUNT(Durum)", "Durum = 2").ToString();
                    ((Site1)page.Master).LabelBeklemede = dt.Compute("COUNT(Durum)", "Durum = 3").ToString();
                    ((Site1)page.Master).LabelTamamlanmadi = dt.Compute("COUNT(Durum)", "Durum = 4").ToString();
                }
            }
            catch(Exception ex)
            {
                Notify.ShowError(ex.Message);
            }

            con.Close();
        }
    }
}