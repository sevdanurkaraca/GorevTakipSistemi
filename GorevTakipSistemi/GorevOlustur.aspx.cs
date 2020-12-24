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
            DoldurGorevli();
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
    }
}