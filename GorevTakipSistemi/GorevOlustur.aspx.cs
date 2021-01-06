using GorevTakipSistemi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using DevExpress;
using Ionic.Zip;
using System.Configuration;

namespace GorevTakipSistemi
{
    public partial class GorevOlustur : System.Web.UI.Page
    {
  //mail gönderim taslağı -- html
        public static string Body = @"<!DOCTYPE HTML PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta name = 'viewport' content='width=device-width' />
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>Mesaj Kayıtları</title>
</head>
<body style = 'margin: 0; padding: 0; font-family: Helvetica Neue,Helvetica,Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100%!important; height: 100%; line-height: 1.6; background-color: #f6f6f6'>
    <table style='background-color: #f6f6f6; width: 100%;'>
        <tr>
            <td style = 'vertical-align: top;'></td>
            <td style='vertical-align: top; display: block!important; max-width: 600px!important; margin: 0 auto!important; clear: both!important' width='760'>
                <div style = 'max-width: 600px; margin: 0 auto; display: block; padding: 20px'>
                    <table style='background: #fff; border: 1px solid #e9e9e9; border-radius: 30px' width='100%' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td style = 'vertical-align: top; padding: 40px'>
                                <table cellpadding='0' cellspacing='0'>
                                    <tr>
                                        <td style = 'vertical-align: top; font-family: Helvetica Neue,Helvetica,Helvetica,Arial,sans-serif; font-size: 16px; color: #fff; font-weight: 500; padding: 20px; text-align: center; border-radius: 20px; background: #0592CC'>
                                            <strong>Görevlendirme Bildirimi</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style = 'vertical-align: top; padding: 0 0 20px;'>
                                            <h4 style='font-family: Helvetica Neue,Helvetica,Helvetica,Arial,sans-serif; color: #000; margin: 40px 0 0; line-height: 1.2; font-weight: 400; font-size: 18px'>
                                                Sayın, #alici</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style = 'vertical-align: top; padding: 0 0 20px; font-family: Helvetica Neue,Helvetica,Helvetica,Arial,sans-serif;'>
                                            #atayan tarafından, size görev atanmıştır.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style = 'vertical-align: top;'></td>
        </tr>
    </table>
</body>
</html>";

        protected void Page_Load(object sender, EventArgs e)
        {
            //session süresi dolmuş ise giriş sayfasına yönlendirmek için ve tekrardan kalınan yerden devam edebilmek için de returnUrl
            if (Session["Kullanici"] == null)
            {
                Response.Redirect("Giris.aspx?ReturnUrl=" + HttpContext.Current.Request.RawUrl); //hangi sayfada olduğumuz bilgisini tutuyor
            }
            else
            {
                //bu sayfa ilk defa yüklenmiş mi kontrol edilmesi gerekiyor. Sayfa içerisindeki hareketler page load'a gidiyor ve daha önceden giriş yapılmışsa vb için kontrol sağlanmalı
                if (!IsPostBack)
                {
                    DoldurGorevli();

                    if (Request["ID"] != null) //yeni bir görev değilse yani onun kontrolü
                    {
                        Gorev g = new Gorev();

                        if (g.KayitAc(Convert.ToInt32(Request["ID"])))
                        {
                            deStartDate.Date = g.BaslangicTarihi;
                            deEndDate.Date = g.BitisTarihi;
                            txtBaslik.Text = g.GorevBaslik;
                            meDetay.Text = g.GorevDetay;
                        }
                        GorevliIsaretle(Convert.ToInt32(Request["ID"])); //daha önceden atanmış görevlilerin işaretlenmesi --> oluşturulan görevin teknisyenlerine ulaşılması (TeknisyenID)
                    }
                }

                Helpers.DurumToplam();
            }
            
        }

        private void GorevliIsaretle(int v)
        {
            DBConnection con = new DBConnection();

            try
            {
                ASPxListBox list = ((ASPxListBox)deDropDown.FindControl("listbox"));
                DataTable dt = con.GetQuery("SELECT * FROM Tbl_Gorevli WHERE IsSilindi = 0 AND GorevID = " + v);

                if(dt.Rows.Count > 0)
                {
                    string txt = "";
                    ListEditItem teknisyenID;

                    foreach (DataRow dr in dt.Rows)
                    {
                        teknisyenID = list.Items.FindByValue(dr["TeknisyenID"]); //görevlideki id ye ait kaydın bulunması
                        teknisyenID.Selected = true;
                        txt += "; " + teknisyenID.ToString();
                    }

                    deDropDown.Text = txt.Length > 0 ? txt.Substring(2) : ""; //if
                }
            }
            catch(Exception ex)
            {
                Notify.ShowError(ex.Message);
            }
            con.Close();
        }

        //Görevli Listboxını doldurmak için kullanılan fonk
        private void DoldurGorevli()
        {
            DBConnection con = new DBConnection();
            ASPxListBox list = deDropDown.FindControl("listBox") as ASPxListBox;

            list.DataSource = con.GetQuery("SELECT * FROM Tbl_Kullanici WHERE IsSilindi = 0");
            list.DataBind();

            con.Close();
        }

        private void Temizle() //yeni kayıt eklendikten sonra formun temizlenmesi için
        {
            deStartDate.Value = DateTime.Now.Date;
            deEndDate.Value = DateTime.Now.Date;
            ASPxListBox list = deDropDown.FindControl("listBox") as ASPxListBox;
            list.UnselectAll();
            deDropDown.Text = "";
            txtBaslik.Text = "";
            meDetay.Text = "";
            fuDosya = null;
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            ASPxListBox list = deDropDown.FindControl("listBox") as ASPxListBox;

            if(list.SelectedItems.Count == 0)
            {
                Notify.ShowInfo("Atanacak Görevliyi Seçiniz!");
                return;
            }

            if (deStartDate.Value == null)
            {
                Notify.ShowInfo("Proje Başlangıç Tarihini Giriniz!");
                return;
            }

            if (deEndDate.Value == null)
            {
                Notify.ShowInfo("Proje Bitiş Tarihini Giriniz!");
                return;
            }

            //hem görev hem görevli tablosuna kayıt için
            DBConnection con = new DBConnection();
            //görev tablosuna yanlış atılan bir kaydı geriye alabilmek için transaction
            SqlTransaction tra = con.sqlBaglanti.BeginTransaction();

            try
            {
                Gorev g = new Gorev();
                Gorevli gorevli = new Gorevli();
                Kullanici kullanici = new Kullanici();

                g.ID = Request["ID"] == null ? -1 : Convert.ToInt32(Request["ID"]);
                g.GorevBaslik = txtBaslik.Text;
                g.GorevDetay = meDetay.Text;
                g.OlusturanKullaniciID = ((Kullanici)Session["Kullanici"]).ID; //login olan kullanıcı --> Görevi oluşturan kullanıcı Login olan kullanıcı oluyor
                g.GrupGuid = Guid.NewGuid().ToString(); //benzersiz bir string oluşturmak için guis 
                g.BaslangicTarihi = deStartDate.Date;
                g.BitisTarihi = deEndDate.Date;

                if (g.Kayit(con, tra)) //true ise görevlileri kaydedebiliriz
                {
                    con.ExecNonQuery("UPDATE Tbl_Gorevli SET IsSilindi = 1 WHERE GorevID = " + g.ID, tra); //düzeltme kaydı yapıldığında

                    for(int i = 0; i < list.SelectedItems.Count; i++) //seçilen görevlilerin eklenmesi
                    {
                        gorevli.TeknisyenID =Convert.ToInt32(list.SelectedItems[i].Value);
                        gorevli.GorevID = g.ID;

                        if(gorevli.Kayit(con, tra))
                        {
                            gorevli.ID = -1;//insert yapabilmek için -1, prosedürün ıd i bulmaması için 

                            if (Request["ID"] == null) //düzenleme yapılan görevde tekrardan mail göndermemek için, ıd null ise gönderecek sadece yani
                            {
                                //mail gonderme işlemi -- görevi atayan kişi yani kullanıcı(görevi oluşturan yani)
                                string prmBody = Body.Replace("#atayan", ((Kullanici)Session["Kullanici"]).AdSoyad);

                                if (kullanici.KayitAc(gorevli.TeknisyenID))//görev atanan kişilere mail gönderilmesi
                                {
                                    prmBody = prmBody.Replace("#alici", kullanici.AdSoyad);

                                    if (!Helpers.MailGonder(kullanici.Email, prmBody, "Görevlendirme Bildirimi "))
                                    {
                                        throw new Exception("Mail gönderimi sırasında hata oluştu");
                                    }
                                }
                            }
                        }
                        else
                            throw new Exception("Görevli Kaydı Sırasında Hata Oluştu");
                    }
                    if (fuDosya.HasFile)
                    {
                        if (!Helpers.DosyaYukle(fuDosya, Server, g.GrupGuid, con, tra ))
                        {
                            throw new Exception("Dosya kaydı sırasında hata oluştu");

                        }
                    }
                }
                else
                    throw new Exception("Görev Kaydı Sırasında Hata Oluştu");

                tra.Commit();
                Helpers.DurumToplam();
                Notify.ShowSuccess("Kayıt İşlemi Başarılı");
                Temizle();
            }
            catch(Exception ex)
            {
                tra.Rollback();
                Notify.ShowError(ex.Message);
            }

            tra.Dispose();
            con.Close();
        }

        protected void deStartDate_Init(object sender, EventArgs e)
        {
            deStartDate.Date= DateTime.Now.Date;
        }
    }
}