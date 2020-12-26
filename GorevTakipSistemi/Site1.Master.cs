using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GorevTakipSistemi
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //session süresi dolmuş ise giriş sayfasına yönlendirmek için ve tekrardan kalınan yerden devam edebilmek için de returnUrl
            if (Session["Kullanici"] == null)
            {
                Response.Redirect("Giris.aspx?ReturnUrl=" + HttpContext.Current.Request.RawUrl); //hangi sayfada olduğumuz bilgisini tutuyor
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
        }


    }
}