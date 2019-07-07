using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminPage
{
    public static class Helper
    {
        public static string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public static bool CheckIfLoggedIn(string username, HttpResponse Response)
        {
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                if (String.IsNullOrWhiteSpace(username)) { Response.Redirect("Admin_Login.aspx"); }

                using (SqlDataAdapter a = new SqlDataAdapter("exec Dohvati_Admina " + username, c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    string logged_in = "n";
                    if (t.Rows.Count > 0)
                    {
                        logged_in = t.Rows[0]["Ulogiran"].ToString();
                    }
                    if (logged_in == "n")
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
    }
}