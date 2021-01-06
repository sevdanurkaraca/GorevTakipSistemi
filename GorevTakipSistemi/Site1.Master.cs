using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GorevTakipSistemi.Classes;
using Ionic.Zip;

namespace GorevTakipSistemi
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string LabelTamamlandi
        {
            get { return this.lblTamamlanan.InnerText; }
            set {  this.lblTamamlanan.InnerText = value; }
        }

        public string LabelTamamlanmadi
        {
            get { return this.lblTamamlanmayan.InnerText; }
            set { this.lblTamamlanmayan.InnerText = value; }
        }

        public string LabelBeklemede
        {
            get { return this.lblBeklemede.InnerText; }
            set { this.lblBeklemede.InnerText = value; }
        }

        public string LabelBaslandi
        {
            get { return this.lblBaslanan.InnerText; }
            set { this.lblBaslanan.InnerText = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //session süresi dolmuş ise giriş sayfasına yönlendirmek için ve tekrardan kalınan yerden devam edebilmek için de returnUrl
            if (Session["Kullanici"] == null)
            {
                Response.Redirect("Giris.aspx?ReturnUrl=" + HttpContext.Current.Request.RawUrl); //hangi sayfada olduğumuz bilgisini tutuyor
            }
            else//kullanıcı login olduğunda email ve ad soyadının sağ üst köşede gözükmesi için
            {
                adSoyad.InnerText = ((Kullanici)Session["Kullanici"]).AdSoyad;
                email.InnerText = ((Kullanici)Session["Kullanici"]).Email;
            }
           SetCurrentPage();
        }

        public void SetCurrentPage()//navbardaki linklerin aktifliği ve dizaynı
        {
            string activePage = Request.RawUrl;//basılan link

            if (activePage.Contains("Gorevler.aspx"))
            {
                liGorevler.Attributes.Add("class", "active");
                pageTitle.InnerText = "Görevler";
            }
            else if (activePage.Contains("GorevOlustur.aspx"))
            {
                liGorevOlustur.Attributes.Add("class", "active");
                pageTitle.InnerText = "Görev Oluştur";
            }
            else if (activePage.Contains("KullanicilarEkrani.aspx"))
            {
                liKullanici.Attributes.Add("class", "active");
                pageTitle.InnerText = "Kullanıcı Listesi";
            }
            else if (activePage.Contains("GanttCizelgesi.aspx"))
            {
                liGantt.Attributes.Add("class", "active");
                pageTitle.InnerText = "Gantt Çizelgesi";
            }
        }

        protected void btnCikis_ServerClick(object sender, EventArgs e)
        {
            //güvenli çıkış
            Session["Kullanici"] = null;
            Response.Redirect("Giris.aspx");
        }
    }
}