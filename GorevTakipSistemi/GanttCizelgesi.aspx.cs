using GorevTakipSistemi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web.DemoUtils;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxGantt;

namespace GorevTakipSistemi
{
    public partial class GanttCizelgesi : System.Web.UI.Page
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
                    //DoldurGorevli();

                    ASPxGantt1.DataBind();
                }

                Helpers.DurumToplam();
            }
        }
    }
}