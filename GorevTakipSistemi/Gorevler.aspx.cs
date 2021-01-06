using GorevTakipSistemi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress;
using DevExpress.Web;
using DevExpress.Data;
using DevExpress.Export;
using System.Data;
using Ionic.Zip;
using System.Drawing;

namespace GorevTakipSistemi
{
    public partial class Gorevler : System.Web.UI.Page
    {
        public object ZipOption { get; private set; }

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
                    DoldurListe();
                }
                else
                {
                    grid.DataSource = Session["GorevListe"] as System.Data.DataTable;
                    grid.DataBind();
                }

                Helpers.DurumToplam();
            }
        }

        private void DoldurListe()
        {
            DBConnection con = new DBConnection();

            string str = "SELECT * FROM VwGorev";

            if(cmbDurum.SelectedIndex != 0)
            {
                str += " WHERE Durum = " + cmbDurum.SelectedIndex;
            }

            Session["GorevListe"] = con.GetQuery(str);
            grid.DataSource = Session["GorevListe"] as System.Data.DataTable;
            grid.DataBind();

            con.Close();
        }

        /*protected void grid_FillContextMenuItems(object sender, DevExpress.Web.ASPxGridViewContextMenuItemClickEventArgs e)
        {   
        }*/

            //gorev listesindeki sağ click olayları= düzenle, refresh, sil
        protected void grid_ContextMenuInitialize(object sender, ASPxGridViewContextMenuInitializeEventArgs e)
        {
              if (e.MenuType == GridViewContextMenuType.Rows)
            {
                var duzenle = e.CreateItem("Düzenle", "Duzenle");
                //var duzenle = e.ContextMenu.Items.Add("Düzenle", "Duzenle");
                duzenle.BeginGroup = true;
                duzenle.Image.IconID = "actions_edit_16x16devav"; //codecentral.devexpress.com icon için
                e.ContextMenu.Items.Insert(e.ContextMenu.Items.IndexOfCommand(DevExpress.Web.GridViewContextMenuCommand.Refresh), duzenle);
            }
        }

        protected void grid_ContextMenuItemClick(object sender, DevExpress.Web.ASPxGridViewContextMenuItemClickEventArgs e)
        {
            if (e.Item.Name == "Duzenle")
               Response.Redirect("GorevOlustur.aspx?ID=" + grid.GetRowValues(grid.FocusedRowIndex, "ID"));
            else if (e.Item.Name == "Refresh")
                DoldurListe();
        }

        protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Gorev g = new Gorev();

            try
            {
                if(grid.FocusedRowIndex > -1)
                {
                    int id = Convert.ToInt32(grid.GetRowValues(grid.FocusedRowIndex, "ID"));

                    g.ID = id;
                    g.IsSilindi = true;

                    if (g.Kayit())
                    {
                        DoldurListe();
                    }
                    else
                        throw new Exception("Kayıt sırasında hata oluştu");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            g = null;
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void btnIndir_Click(object sender, EventArgs e)
        {
            Dosya d = new Dosya();

            string guid = grid.GetRowValues(grid.FocusedRowIndex, "GrupGuid").ToString();

            DBConnection con = new DBConnection();
            DataTable dt = con.GetQuery("SELECT * FROM Tbl_Dosya WHERE IsSilindi = 0 AND GorevGuid = '" + guid + "'");

            if (dt.Rows.Count > 1)//birden fazla dosya indirmek için
            {
                ZipFile zip = new ZipFile();

                zip.AlternateEncodingUsage = Ionic.Zip.ZipOption.AsNecessary;

                foreach (DataRow dr in dt.Rows)
                {
                    zip.AddFile(dr["Path"].ToString()).FileName = dr["DosyaAdi"].ToString();
                }

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = string.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MM-dd"));
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();

            }
            else if (dt.Rows.Count == 1)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + dt.Rows[0]["DosyaAdi"]);
                Response.TransmitFile(Server.MapPath("~/Dosya/" + System.IO.Path.GetExtension(dt.Rows[0]["DosyaAdi"].ToString())));
                Response.End();
            }
            else
                Notify.ShowInfo("İndirilecek Dosya Bulunamadı.");
        }

        //durum değiştirme sayfasına yönlendirme
        protected void btnTamamla_Click(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('DurumDegistir.aspx?ID=" + grid.GetRowValues(grid.FocusedRowIndex, "ID") + "', '_parent');", true);
            }
            catch(Exception ex)
            {
                Notify.ShowError(ex.Message);
            }
            
        }

        protected void grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if(e.DataColumn.FieldName == "DurumBilgisi")
            {
                e.Cell.ForeColor = Color.White;

                switch (e.GetValue("DurumBilgisi").ToString())
                {
                    case "Başlandı":
                        e.Cell.BackColor = Color.FromArgb(89, 89, 89);
                        break;
                    case "Tamamlandı":
                        e.Cell.BackColor = Color.FromArgb(64, 176, 101);
                        break;
                    case "Beklemede":
                        e.Cell.BackColor = Color.FromArgb(230, 162, 0);
                        break;
                    case "Tamamlanmadı":
                        e.Cell.BackColor = Color.FromArgb(241, 60, 60);
                        break;
                }
            }
        }

        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            DoldurListe();
        }

        protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

            
                Response.Redirect("GorevOlustur.aspx?ID=" + grid.GetRowValues(grid.FocusedRowIndex, "ID"));
            
        }
    }
}