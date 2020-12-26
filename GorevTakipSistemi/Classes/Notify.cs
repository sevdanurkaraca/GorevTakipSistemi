using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace GorevTakipSistemi.Classes
{
    public class Notify
    {
        public static void ShowSuccess(string mesaj)
        {
            string cleanMessage = mesaj.Replace("'", "\\'");
            string script = "<script> alertify.success(\"" + cleanMessage + "\"); </script>";

            Page page = HttpContext.Current.CurrentHandler as Page;

            if(page != null && !page.ClientScript.IsClientScriptBlockRegistered("ShowSuccess"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(Notify), "ShowSuccess", script);
            }
        }

        public static void ShowError(string mesaj)
        {
            string cleanMessage = mesaj.Replace("'", "\\'");
            string script = "<script> alertify.error(\"" + cleanMessage + "\"); </script>";

            Page page = HttpContext.Current.CurrentHandler as Page;

            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("ShowError"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(Notify), "ShowError", script);
            }
        }

        public static void ShowInfo(string mesaj)
        {
            string cleanMessage = mesaj.Replace("'", "\\'");
            string script = "<script> alertify.log(\"" + cleanMessage + "\"); </script>";

            Page page = HttpContext.Current.CurrentHandler as Page; //on işlem yapılan sayfanın tutulması currentHandler

            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("ShowInfo"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(Notify), "ShowInfo", script);
            }
        }
    }
}