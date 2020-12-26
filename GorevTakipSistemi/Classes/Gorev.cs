using GorevTakipSistemi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace GorevTakipSistemi
{
    public class Gorev
    {
        public int ID = -1;
        public bool IsSilindi = false;
        public int Durum = -1;
        public string GorevBaslik = "";
        public string GorevDetay = "";
        public DateTime BaslangicTarihi = DateTime.Now.Date;
        public DateTime BitisTarihi = DateTime.Now.Date;
        public int OlusturanKullaniciID = -1;
        public string GrupGuid = "";
        public string YapilanIs = "";
        public int TamamlayanKullaniciID = -1;

        public Gorev()
        {

        }

        #region Fonk
        public bool KayitAc(int prmID)
        {
            bool ret = false;

            try
            {
                DBConnection con = new DBConnection();
                string prmQuery = "SELECT * FROM Tbl_Gorev IsSilindi = 0 AND ID = " + prmID;
                DataTable dt = con.GetQuery(prmQuery);

                if (dt.Rows.Count > 0)
                {
                    ret = true;

                    this.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    this.IsSilindi = Convert.ToBoolean(dt.Rows[0]["IsSilindi"]);
                    this.Durum = Convert.ToInt32(dt.Rows[0]["Durum"]);
                    this.GorevBaslik = dt.Rows[0]["GorevBaslik"].ToString();
                    this.GorevDetay = dt.Rows[0]["GorevDetay"].ToString();
                    this.BaslangicTarihi = Convert.ToDateTime(dt.Rows[0]["BaslangicTarihi"]);
                    this.BitisTarihi = Convert.ToDateTime(dt.Rows[0]["BitisTarihi"]);
                    this.OlusturanKullaniciID = Convert.ToInt32(dt.Rows[0]["OlusturanKullaniciID"]);
                    this.GrupGuid = dt.Rows[0]["GrupGuid"].ToString();
                    this.YapilanIs = dt.Rows[0]["YapilanIs"].ToString();
                    this.TamamlayanKullaniciID = Convert.ToInt32(dt.Rows[0]["TamamlayanKullaniciID"]);

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
                sqlParameters.Add(new SqlParameter("@Durum", this.Durum));
                sqlParameters.Add(new SqlParameter("@GorevBaslik", this.GorevBaslik));
                sqlParameters.Add(new SqlParameter("@GorevDetay", this.GorevDetay));
                sqlParameters.Add(new SqlParameter("@BaslangicTarihi", this.BaslangicTarihi));
                sqlParameters.Add(new SqlParameter("@BitisTarihi", this.BitisTarihi));
                sqlParameters.Add(new SqlParameter("@OlusturanKullaniciID", this.OlusturanKullaniciID));
                sqlParameters.Add(new SqlParameter("@GrupGuid", this.GrupGuid));
                sqlParameters.Add(new SqlParameter("@YapilanIs", this.YapilanIs));
                sqlParameters.Add(new SqlParameter("@TamamlayanKullaniciID", this.TamamlayanKullaniciID));

                object o = con.RetStoredProc("sp_TblGorev", sqlParameters);

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
                sqlParameters.Add(new SqlParameter("@Durum", this.Durum));
                sqlParameters.Add(new SqlParameter("@GorevBaslik", this.GorevBaslik));
                sqlParameters.Add(new SqlParameter("@GorevDetay", this.GorevDetay));
                sqlParameters.Add(new SqlParameter("@BaslangicTarihi", this.BaslangicTarihi));
                sqlParameters.Add(new SqlParameter("@BitisTarihi", this.BitisTarihi));
                sqlParameters.Add(new SqlParameter("@OlusturanKullaniciID", this.OlusturanKullaniciID));
                sqlParameters.Add(new SqlParameter("@GrupGuid", this.GrupGuid));
                sqlParameters.Add(new SqlParameter("@YapilanIs", this.YapilanIs));
                sqlParameters.Add(new SqlParameter("@TamamlayanKullaniciID", this.TamamlayanKullaniciID));

                object o = con.RetStoredProc("sp_TblGorev", sqlParameters, tra);

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