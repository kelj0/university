using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminPage
{
    public partial class Obrok : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Helper.CheckIfLoggedIn((string)Session["name"], Response)){
                Response.Redirect("Admin_Login.aspx");
            }

            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("exec Dohvati_Sve_Obroke", con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }

        protected void obrok_home_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminHome.aspx");
        }
    }
}