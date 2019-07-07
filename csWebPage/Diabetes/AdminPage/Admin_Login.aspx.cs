using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminPage
{
    public partial class Admin_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace((string)Session["name"])) { Session["name"] = "a"; }
            if(Helper.CheckIfLoggedIn((string)Session["name"], Response)){
                Response.Redirect("AdminHome.aspx");
            }
        }

        protected void submit_login_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("Provjeri_Admina", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = username.Text;
                    cmd.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = password.Text;

                    con.Open();
                    var r = cmd.ExecuteScalar();
                    if (r.ToString() == "1")
                    {
                        Error.Text = "You are logged in, you will be redirected soon..";
                        Session["name"] = username.Text;
                        using (SqlCommand a = new SqlCommand("Prijavi_Admina", con))
                        {
                            a.CommandType = CommandType.StoredProcedure;
                            a.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = Session["name"];
                            a.ExecuteScalar();
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS",
                               "setTimeout(function() { window.location.replace('AdminHome.aspx') }, 2000);", true);
                        }
                    }
                    else
                    {
                        Error.Text = r.ToString();
                    }
                }
            }
        }
    }
}