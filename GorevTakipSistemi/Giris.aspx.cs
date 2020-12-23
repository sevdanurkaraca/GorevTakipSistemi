﻿using GorevTakipSistemi.Classes;
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
                Response.Redirect("Gorevler.aspx");
            }
            else
            {
                throw new Exception("Giriş Sırasında Hata Oluştu");
            }
        }
    }
}