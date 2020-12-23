using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace GorevTakipSistemi.Classes
{
    public class Kullanici
    {
        public int ID = -1;
        public bool IsSilindi = false;
        public string AdSoyad = "";
        public string KullaniciAdi = "";
        public string Email = "";
        public string Parola = "";

        public Kullanici()
        {

        }
        #region Fonk
        public bool KayitAc(int prmID)
        {
            bool ret = false;

            try
            {
                DBConnection con = new DBConnection();
                string prmQuery = "SELECT * FROM Tbl_Kullanici IsSilindi = 0 AND ID = " + prmID;
                DataTable dt = con.GetQuery(prmQuery);

                if(dt.Rows.Count > 0)
                {
                    ret = true;

                    this.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    this.IsSilindi = Convert.ToBoolean(dt.Rows[0]["IsSilindi"]);
                    this.AdSoyad = dt.Rows[0]["AdSoyad"].ToString();
                    this.Email = dt.Rows[0]["Email"].ToString();
                    this.Parola = dt.Rows[0]["Parola"].ToString();
                }

                dt.Rows.Clear();
                dt.Clear();
                dt.Dispose();
                dt = null;
                con.Close();
            }
            catch
            {
                ret = false;
            }
            GC.Collect();
            return ret;
        }

        //login ekranı için
        public bool KayitAc(string prmMail, string prmParola)
        {
            bool ret = false;

            try
            {
                DBConnection con = new DBConnection();

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@Mail", prmMail));
                sqlParameters.Add(new SqlParameter("@Parola", prmParola));

                DataTable dt = con.RetStoredProcDataTable("KullBul", sqlParameters);
                
               

                if (dt.Rows.Count > 0)
                {
                    ret = true;

                    this.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    this.IsSilindi = Convert.ToBoolean(dt.Rows[0]["IsSilindi"]);
                    this.AdSoyad = dt.Rows[0]["AdSoyad"].ToString();
                    this.Email = dt.Rows[0]["Email"].ToString();
                    this.Parola = dt.Rows[0]["Parola"].ToString();
                }

                dt.Rows.Clear();
                dt.Clear();
                dt.Dispose();
                dt = null;
                con.Close();
            }
            catch
            {
                ret = false;
            }
            GC.Collect();
            return ret;
        }

        public bool Kayit()
        {
            bool ret = false;

            try
            {
                DBConnection con = new DBConnection();
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@ID", this.ID));
                sqlParameters.Add(new SqlParameter("@IsSilindi", this.IsSilindi));
                sqlParameters.Add(new SqlParameter("@AdSoyad", this.AdSoyad));
                sqlParameters.Add(new SqlParameter("@Email", this.Email));
                sqlParameters.Add(new SqlParameter("@Parola", this.Parola));

                object o = con.RetStoredProc("sp_TblKullanici", sqlParameters);

                if(o == null)
                {
                    ret = false;
                }
                else
                {
                    ret = true;
                    this.ID = Convert.ToInt32(o);
                }

                o = null;
                sqlParameters.Clear();
                sqlParameters = null;
                con.Close();
            }
            catch
            {
                ret = false;
            }
            GC.Collect();
            return ret;

        }
        #endregion
    }
}