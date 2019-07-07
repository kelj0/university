using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminPage
{
    public partial class AdminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Helper.CheckIfLoggedIn((string)Session["name"], Response)){
                Response.Redirect("Admin_Login.aspx");
            }
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("exec Dohvati_Sve_Korisnike", con))
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

        protected void Logout_Click(object sender, EventArgs e)
        {
            using (SqlConnection c = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("Odjavi_Admina", c))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = Session["name"];
                    c.Open();
                    cmd.ExecuteScalar();
                    Session["name"] = "";
                    Response.Redirect("Admin_Login.aspx");
                }
            }
        }

        protected void btn_namirnice_Click(object sender, EventArgs e)
        {
            Response.Redirect("Namirnice.aspx");
        }

        protected void btn_obroci_Click(object sender, EventArgs e)
        {
            Response.Redirect("Obroci.aspx");
        }

        protected void btn_jedinice_Click(object sender, EventArgs e)
        {
            Response.Redirect("Jedinice.aspx");
        }

        protected void btn_CSV_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(Helper.CONNECTION_STRING))
            using (var cmd = new SqlCommand("Dohvati_Sve_Korisnike", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
            string s = sb.ToString();
            Response.Clear();
            Response.AddHeader("content-disposition",
             "attachment;filename=Korisnici.csv");
            Response.AddHeader("content-type", "text/plain");

            using (StreamWriter writer = new StreamWriter(Response.OutputStream))
            {
                writer.WriteLine(s);
            }
            Response.Flush();
            Response.End();
        }

        protected void btn_kombinacije_Click(object sender, EventArgs e)
        {
            Response.Redirect("Combinations.aspx");
        }
    }
}