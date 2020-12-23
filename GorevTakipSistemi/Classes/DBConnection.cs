using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace GorevTakipSistemi.Classes
{
    public class DBConnection
    {
        public SqlConnection sqlBaglanti;
        private int commandTimeOut = 600;
        
        public DBConnection()
        {
            try
            {
                sqlBaglanti = new SqlConnection();
                sqlBaglanti.ConnectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
                sqlBaglanti.Open();
            }
            catch //(Exception)
            {
                
            }

            GC.Collect();
        }

        public void Close()
        {
            try
            {
                if(sqlBaglanti.State != System.Data.ConnectionState.Closed)
                {
                    sqlBaglanti.Close();
                }

                sqlBaglanti.Dispose();
                sqlBaglanti = null;

            }
            catch(Exception)
            {

            }
            GC.Collect();
        }

        public DataTable GetQuery(string prmQuery)
        {
            DataTable ret = null;

            try
            {
                ret = new DataTable();
                if(sqlBaglanti.State != ConnectionState.Open)
                {
                    sqlBaglanti.Open();
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(prmQuery, sqlBaglanti);
                sqlDataAdapter.SelectCommand.CommandTimeout = this.commandTimeOut;
                sqlDataAdapter.Fill(ret);
                sqlDataAdapter.Dispose();
                sqlDataAdapter = null;
            }
            catch(Exception)
            {
                ret = null;
            }
            finally
            {
                GC.Collect();
            }

            return ret;
        }

        public DataTable RetStoredProcDataTable(string prmSpName, List<SqlParameter> prmListPar)
        {
            DataTable ret = new DataTable();

            try
            {
                if (sqlBaglanti.State != ConnectionState.Open)
                {
                    sqlBaglanti.Open();
                }

                SqlCommand cmd = new SqlCommand(prmSpName, sqlBaglanti);
                cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < prmListPar.Count; i++)
                {
                    cmd.Parameters.Add(prmListPar[i]);
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ret);
                da.Dispose();
                cmd.Dispose();
            }
            catch
            {
                ret = null;
            }
            finally
            {
                GC.Collect();
            }

            return ret;
                
        }

        public object RetStoredProc(string prmSpName, List<SqlParameter> prmListPar)
        {
            object ret = null;

            try
            {
                if (sqlBaglanti.State != ConnectionState.Open)
                {
                    sqlBaglanti.Open();
                }

                SqlCommand cmd = new SqlCommand(prmSpName, sqlBaglanti);
                cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < prmListPar.Count; i++)
                {
                    cmd.Parameters.Add(prmListPar[i]);
                }

                ret = cmd.ExecuteScalar();
                cmd.Dispose();
                cmd = null;
            }
            catch
            {
                ret = null;
            }
            finally
            {
                GC.Collect();
            }

            return ret;
        }



    }
}