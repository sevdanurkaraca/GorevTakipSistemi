using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GorevTakipSistemi.Classes
{
    public class Dosya
    {
        public int ID = -1;
        public bool IsSilindi = false;
        public string GorevGuid = "";
        public string DosyaAdi = "";
        public string Path = "";


        public bool Kayit(DBConnection con, SqlTransaction tra)
        {
            bool ret = false;

            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@ID", this.ID));
                sqlParameters.Add(new SqlParameter("@IsSilindi", this.IsSilindi));
                sqlParameters.Add(new SqlParameter("@GrupGuid", this.GorevGuid));
                sqlParameters.Add(new SqlParameter("@DosyaAdi", this.DosyaAdi));
                sqlParameters.Add(new SqlParameter("@Path", this.Path));

                object o = con.RetStoredProc("sp_TblDosya", sqlParameters, tra);

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
    }
}