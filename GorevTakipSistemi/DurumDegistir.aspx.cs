using GorevTakipSistemi.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GorevTakipSistemi
{
    public partial class DurumDegistir : System.Web.UI.Page
    {

        #region Body
        const string body = @"<!DOCTYPE HTML PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
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
                                            <strong>Mesajınız Var</strong>
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
                                            #gonderen tarafından, #baslik başlıklı görev ile ilgili bilgilendirme notu girilmiştir. Görüntülemek için aşağıdaki butona tıklayınız.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align = 'center' bgcolor='#00B264' colspan='2' style='padding: 7px; border-radius: 20px; color: #FFFFFF; font-weight: bold; vertical-align: top; font-family: Helvetica Neue,Helvetica,Helvetica,Arial,sans-serif;'>
                                            <a href = '#link' style='color: #FFFFFF; text-decoration: none; width: 100%; display: inline-block;'>Tıklayınız</a>
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
        #endregion



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
                    Session["Mesajlar"] = null;
                    DoldurMesaj();
                }
            }
        }

        private void DoldurMesaj()
        {
            DBConnection con = new DBConnection();

            try
            {
                DataTable dt = con.GetQuery("SELECT * FROM VwMesaj WHERE GorevID = " + Request["ID"]);

                if(dt.Rows.Count > 0)
                {
                    txtGorevID.InnerText = dt.Rows[0]["GorevID"].ToString();
                    txtBaslik.InnerText = dt.Rows[0]["GorevBaslik"].ToString();
                    cmbDurum.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Durum"]);

                    Session["Mesajlar"] = dt;

                    rptMesaj.DataSource = dt;
                    rptMesaj.DataBind();
                }
                else
                {
                    DataTable tmp = con.GetQuery("SELECT * FROM Tbl_Gorev WHERE IsSilindi = 0 AND ID = " + Request["ID"]);

                    if (tmp.Rows.Count > 0)
                    {
                        txtGorevID.InnerText = tmp.Rows[0]["ID"].ToString();
                        txtBaslik.InnerText = tmp.Rows[0]["GorevBaslik"].ToString();
                        cmbDurum.SelectedIndex = Convert.ToInt32(tmp.Rows[0]["Durum"]);
                    }

                    
                }
            }
            catch(Exception ex)
            {
                Notify.ShowError(ex.Message);
            }
            con.Close();
        }

        protected void btnDurumDegistir_ServerClick(object sender, EventArgs e)
        {
            Gorev g = new Gorev();

            try
            {
                if (g.KayitAc(Convert.ToInt32(Request["ID"])))
                {
                    g.Durum = cmbDurum.SelectedIndex;
                    g.TamamlayanKullaniciID = ((Kullanici)Session["Kullanici"]).ID;

                    if (g.Kayit())
                        Notify.ShowSuccess("Kayıt başarılı");
                    else
                        throw new Exception("Kayıt sırasında hata oluştu");
                }
            }
            catch(Exception ex)
            {
                Notify.ShowError(ex.Message);
            }

            g = null;
        }

        protected void btnMesaj_ServerClick(object sender, EventArgs e)
        {
            DBConnection con = new DBConnection();

            try
            {
                if (meMesaj.Text == "")
                {
                    Notify.ShowInfo("Mesaj boş olamaz");
                    return;
                }
                Mesajj m = new Mesajj();

                m.Mesaj = meMesaj.Text;
                m.GorevID = Convert.ToInt32(Request["ID"]);
                m.Tarih = DateTime.Now;
                m.KullaniciID = ((Kullanici)Session["Kullanici"]).ID;

                if (m.Kayit())//mesajı kaydetme
                {
                    Notify.ShowSuccess("Mesaj kaydedildi");
                    meMesaj.Text = "";
                    DoldurMesaj();

                    DataTable dt = con.GetQuery(@"
DECLARE @GorevID int = " + m.GorevID + @"
DECLARE @MesajYazanID int = " + m.KullaniciID + @"


SELECT 
orc.*
, g.GorevBaslik
, k.AdSoyad AS Alici
, k.Email
FROM (
		SELECT
		OlusturanKullaniciID AS KullID
		FROM Tbl_Gorev
		WHERE IsSilindi = 0 AND ID = @GorevID AND OlusturanKullaniciID <> @MesajYazanID
		UNION
		SELECT 
		TeknisyenID AS KullID
		FROM Tbl_Gorevli
		WHERE IsSilindi = 0 AND GorevID = @GorevID AND TeknisyenID <> @MesajYazanID
	) AS orc
INNER JOIN Tbl_Gorev g ON g.IsSilindi = 0 AND g.ID = @GorevID 
LEFT JOIN Tbl_Kullanici k ON k.IsSilindi = 0 AND k.ID = orc.KullID");
                    //görevlilere mail gonderilmesi

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)//mesajları satır satır gösterebilmek için
                        {
                            string prmBody = body.Replace("#alici", dr["Alici"].ToString());
                            prmBody = prmBody.Replace("#gonderen", ((Kullanici)Session["Kullanici"]).AdSoyad);
                            prmBody = prmBody.Replace("#baslik", dr["GorevBaslik"].ToString()); //?
                            prmBody = prmBody.Replace("#link", ConfigurationManager.AppSettings["link"] + Request["ID"]);


                            if (!Helpers.MailGonder(dr["Email"].ToString(), prmBody, "Mesajınız Var!"))//prmTo,prmBody,prmSubject
                            {
                                Notify.ShowError("Mail gönderilirken hata oluştu");
                            }
                        }
                    }
                }  
                else
                    throw new Exception("Kayıt sırasında hata oluştu");
            }
            catch (Exception ex)
            {
                Notify.ShowError(ex.Message);
            }
                con.Close();
            }

        protected void btnGeriDon_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("Gorevler.aspx");
        }
    }
}