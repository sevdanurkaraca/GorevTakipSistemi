using GorevTakipSistemi.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GorevTakipSistemi
{
    public partial class KullanicilarEkrani : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Kullanici"] == null)
            {
                Response.Redirect("Giris.aspx?ReturnUrl=" + HttpContext.Current.Request.RawUrl); //hangi sayfada olduğumuz bilgisini tutuyor
            }
            else
            {
                //bu sayfa ilk defa yüklenmiş mi kontrol edilmesi gerekiyor. Sayfa içerisindeki hareketler page load'a gidiyor ve daha önceden giriş yapılmışsa vb için kontrol sağlanmalı
                if (!IsPostBack)
                {
                    DoldurListe();
                }
                else
                {
                    grid.DataSource = Session["KullaniciListe"] as System.Data.DataTable;
                    grid.DataBind();
                }

                Helpers.DurumToplam();
            }
        }

        private void DoldurListe()
        {
            DBConnection con = new DBConnection();

            Session["KullaniciListe"] = con.GetQuery("SELECT * FROM Tbl_Kullanici WHERE IsSilindi = 0");

            grid.DataSource = Session["KullaniciListe"] as DataTable;
            grid.DataBind();
        }

        //protected void grid_ContextMenuInitialize(object sender, DevExpress.Web.ASPxGridViewContextMenuInitializeEventArgs e)
        //{

        //}

            //refresh özelliği
        protected void grid_ContextMenuItemClick(object sender, DevExpress.Web.ASPxGridViewContextMenuItemClickEventArgs e)
        {
            if (e.Item.Name == "Refresh")
            {
                DoldurListe();
            }
        }

        //delete
        protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Kullanici k = new Kullanici();

            try
            {
                k.ID = Convert.ToInt32(grid.GetRowValues(grid.FocusedRowIndex, "ID"));
                k.IsSilindi = true;

                if (k.Kayit())
                {
                    DoldurListe();
                }
                else
                    throw new Exception("Kayıt sırasında hata oluştu");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            k = null;
            e.Cancel = true;
            grid.CancelEdit();
        }

        //update
        protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            Kullanici k = new Kullanici();

            try
            {
                if (k.KayitAc(Convert.ToInt32(e.Keys["ID"])))
                {
                    //k.ID = Convert.ToInt32(e.Keys["ID"]);//seçilen satırın ıd sini veriyor
                    k.KullaniciAdi = e.NewValues["KullaniciAdi"].ToString();

                    if (e.NewValues["Parola"] != null)
                    {
                        k.Parola = e.NewValues["Parola"].ToString();
                    }

                    k.AdSoyad = e.NewValues["AdSoyad"].ToString();
                    k.Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();

                    if (k.Kayit())
                    {
                        DoldurListe();
                    }
                    else
                        throw new Exception("Kayıt sırasında hata oluştu");
                }
                else
                    throw new Exception("Kullanıcı bulunamadı");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            k = null;
            e.Cancel = true;
            grid.CancelEdit();

        }

            //Kullanici k = new Kullanici();

            //try
            //{
            //    if (k.KayitAc(Convert.ToInt32(e.Keys["ID"])))//bulunduğumuz satırın ıd sini verir --> keyfieldname = ıd den yakalayıp
            //    {
            //        k.KullaniciAdi = e.NewValues["KullaniciAdi"].ToString();

            //        if (e.NewValues["Parola"] != null)
            //        {
            //            k.Parola = e.NewValues["Parola"].ToString();
            //        }

            //        k.AdSoyad = e.NewValues["AdSoyad"].ToString();
            //        k.Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();

            //        if (k.Kayit())
            //        {
            //            DoldurListe();
            //        }
            //        else
            //            throw new Exception("Kayıt sırasında hata oluştu");
            //    }
            //    else
            //        throw new Exception("Kullanıcı bulunamadı");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

            //k = null;
            //e.Cancel = true;
            //grid.CancelEdit();
        

        protected void btnKaydet_ServerClick(object sender, EventArgs e)
        {
            if (txtAdsoyad.Text == "" || txtKullAd.Text == "")
            {
                Notify.ShowInfo("Bilgileri eksiksiz doldurunuz!");
                return;
            }

            Kullanici k = new Kullanici();

            try
            {
                k.KullaniciAdi = txtKullAd.Text;
                k.AdSoyad = txtAdsoyad.Text;
                k.Parola = txtParola.Text;
                k.Email = txtEmail.Text;

                if (k.Kayit())//kayıt başarılıysa listemizi doldursun
                {
                    Notify.ShowSuccess("Kayıt başarılı");
                    DoldurListe();
                    Temizle();
                }
                else
                    throw new Exception("Kayıt sırasında hata oluştu");
            }
            catch(Exception ex)
            {
                Notify.ShowError(ex.Message);
            }
            k = null;
        }

        private void Temizle()
        {
            txtAdsoyad.Text = "";
            txtKullAd.Text = "";
            txtEmail.Text = "";
            txtParola.Text = "";
        }
    }
}