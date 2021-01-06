using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GorevTakipSistemi.Classes
{
    public class Mesajj
    {
        public int ID = -1;
        public bool IsSilindi = false;
        public string Mesaj = "";
        public int GorevID = -1;
        public DateTime Tarih = DateTime.Now.Date;
        public int KullaniciID = -1;
        

        public Mesajj()
        {

        }

        #region Fonk
        public bool KayitAc(int prmID)
        {
            bool ret = false;

            try
            {
                DBConnection con = new DBConnection();
                string prmQuery = "SELECT * FROM Tbl_Mesaj WHERE IsSilindi = 0 AND ID = " + prmID;
                DataTable dt = con.GetQuery(prmQuery);

                if (dt.Rows.Count > 0)
                {
                    ret = true;

                    this.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    this.IsSilindi = Convert.ToBoolean(dt.Rows[0]["IsSilindi"]);
                    this.Mesaj = dt.Rows[0]["Mesaj"].ToString();
                    this.GorevID = Convert.ToInt32(dt.Rows[0]["GorevID"]);
                    this.Tarih = Convert.ToDateTime(dt.Rows[0]["Tarih"]);
                    this.KullaniciID = Convert.ToInt32(dt.Rows[0]["KullaniciID"]);
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
                sqlParameters.Add(new SqlParameter("@Mesaj", this.Mesaj));
                sqlParameters.Add(new SqlParameter("@GorevID", this.GorevID));
                sqlParameters.Add(new SqlParameter("@Tarih", this.Tarih));
                sqlParameters.Add(new SqlParameter("@KullaniciID", this.KullaniciID));
                object o = con.RetStoredProc("sp_TblMesaj", sqlParameters);

                if (o == null)
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



        //public bool Kayit(DBConnection con, SqlTransaction tra)
        //{
        //    bool ret = false;

        //    try
        //    {
        //        List<SqlParameter> sqlParameters = new List<SqlParameter>();

        //        sqlParameters.Add(new SqlParameter("@ID", this.ID));
        //        sqlParameters.Add(new SqlParameter("@IsSilindi", this.IsSilindi));
        //        sqlParameters.Add(new SqlParameter("@Mesaj", this.Mesaj));
        //        sqlParameters.Add(new SqlParameter("@GorevID", this.GorevID));
        //        sqlParameters.Add(new SqlParameter("@Tarih", this.Tarih));
        //        sqlParameters.Add(new SqlParameter("@KullaniciID", this.KullaniciID));
        //        object o = con.RetStoredProc("sp_TblMesaj", sqlParameters, tra);

        //        if (o == null)
        //        {
        //            ret = false;
        //        }
        //        else
        //        {
        //            ret = true;
        //            this.ID = Convert.ToInt32(o);
        //        }

        //        o = null;
        //        sqlParameters.Clear();
        //        sqlParameters = null;
        //    }
        //    catch
        //    {
        //        ret = false;
        //    }
        //    GC.Collect();
        //    return ret;

        //}
        #endregion


    }
}