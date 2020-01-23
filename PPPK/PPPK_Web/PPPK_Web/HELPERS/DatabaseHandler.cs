using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPPK_Web.HELPERS
{
    public static class DatabaseHandler
    {
        public static string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["PPPK_DATABASE"].ConnectionString;

        public static DataTable getVozac(int ID)
        {
            using(SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                if(ID == 0) { return null; }
                using(SqlDataAdapter a = new SqlDataAdapter("select * from Vozaci where Vozac.id="+ID, c)) //add procedures against sqli
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if(t.Rows.Count > 0)
                    {
                        return t;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


    }
}