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

namespace GorevTakipSistemi
{
    public partial class GorevOlustur : System.Web.UI.Page
    {
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
                }
            }
            
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

                g.ID = -1;
                g.GorevBaslik = txtBaslik.Text;
                g.GorevDetay = meDetay.Text;
                g.OlusturanKullaniciID = ((Kullanici)Session["Kullanici"]).ID; //login olan kullanıcı --> Görevi oluşturan kullanıcı Login olan kullanıcı oluyor
                g.GrupGuid = Guid.NewGuid().ToString(); //benzersiz bir string oluşturmak için guis 
                g.BaslangicTarihi = deStartDate.Date;
                g.BitisTarihi = deEndDate.Date;

                if (g.Kayit(con, tra)) //true ise görevlileri kaydedebiliriz
                {
                    con.ExecNonQuery("UPDATE Tbl_Gorevli SET IsSilindi = 1 WHERE GorevID = " + g.ID, tra);

                    for(int i = 0; i < list.SelectedItems.Count; i++) //seçilen görevlilerin eklenmesi
                    {
                        gorevli.TeknisyenID =Convert.ToInt32(list.SelectedItems[i].Value);
                        gorevli.GorevID = g.ID;

                        if(gorevli.Kayit(con, tra))
                        {
                            gorevli.ID = -1;

                            //mail gonderme işlemi
                        }
                        else
                            throw new Exception("Görevli Kaydı Sırasında Hata Oluştu");
                    }
                }
                else
                    throw new Exception("Görev Kaydı Sırasında Hata Oluştu");

                tra.Commit();
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
    }
}