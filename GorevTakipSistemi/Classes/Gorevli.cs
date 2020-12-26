using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace GorevTakipSistemi.Classes
{
    public class Gorevli
    {
        public int ID = -1;
        public bool IsSilindi = false;
        public int TeknisyenID = -1;
        public int GorevID = -1;

        public Gorevli()
        {

        }

        #region Fonk
        public bool KayitAc(int prmID)
        {
            bool ret = false;

            try
            {
                DBConnection con = new DBConnection();
                string prmQuery = "SELECT * FROM Tbl_Gorevli IsSilindi = 0 AND ID = " + prmID;
                DataTable dt = con.GetQuery(prmQuery);

                if (dt.Rows.Count > 0)
                {
                    ret = true;

                    this.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    this.IsSilindi = Convert.ToBoolean(dt.Rows[0]["IsSilindi"]);
                    this.TeknisyenID = Convert.ToInt32(dt.Rows[0]["TeknisyenID"]);
                    this.GorevID = Convert.ToInt32(dt.Rows[0]["GorevID"]);
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
                sqlParameters.Add(new SqlParameter("@TeknisyenID", this.TeknisyenID));
                sqlParameters.Add(new SqlParameter("@GorevID", this.GorevID));

                object o = con.RetStoredProc("sp_TblGorevli", sqlParameters);

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


        public bool Kayit(DBConnection con, SqlTransaction tra)
        {
            bool ret = false;

            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@ID", this.ID));
                sqlParameters.Add(new SqlParameter("@IsSilindi", this.IsSilindi));
                sqlParameters.Add(new SqlParameter("@TeknisyenID", this.TeknisyenID));
                sqlParameters.Add(new SqlParameter("@GorevID", this.GorevID));

                object o = con.RetStoredProc("sp_TblGorevli", sqlParameters, tra);

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