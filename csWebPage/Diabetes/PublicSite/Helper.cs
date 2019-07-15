using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperNamespace
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

        public static bool CheckIfLoggedInMVC(int id)
        {
            if (id == 0) { return false; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("exec Dohvati_Korisnika " + id, c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    string logged_in = "n";
                    if (t.Rows.Count > 0)
                    {
                        logged_in = t.Rows[0]["Ulogiran"].ToString();
                    }
                    if (logged_in == "d")
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        public static int Get_kcal(double w, double h,double g, double a,double d,int y) =>(int)(((10 * w) + (6.25 * h) - 5*y + g) * a * d);
    }
}