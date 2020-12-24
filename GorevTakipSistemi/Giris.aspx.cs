using GorevTakipSistemi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GorevTakipSistemi
{
    public partial class Giris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGiris_ServerClick(object sender, EventArgs e)
        {
            Kullanici k = new Kullanici();

            if(k.KayitAc(txtEmail.Value, txtParola.Value))
            {
                //bütün sayfalarda login olmuş kullanıcının bilgilerine erişebilmek için
                Session["Kullanici"] = k;

                if (Request["ReturnUrl"] == null)
                {
                    Response.Redirect("Gorevler.aspx");
                }
                else
                {
                    Response.Redirect("ReturnUrl");
                }

                Response.Redirect("Gorevler.aspx");
            }
            else
            {
               Notify.ShowError(this, "Giriş Sırasında Hata Oluştu");
            }
        }
    }
}