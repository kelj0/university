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
    public partial class Jedinice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Helper.CheckIfLoggedIn((string)Session["name"], Response))
            {
                Response.Redirect("Admin_Login.aspx");
            }
            if(!IsPostBack)
                BindGrid();
        }

        protected void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("exec Dohvati_Sve_Jedinice", con))
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

        protected void jedinice_home_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminHome.aspx");
        }

        
        protected void Insert(object sender, EventArgs e)
        {
            string ImeJedinice;
            try{
                ImeJedinice = txtName.Text;
            }catch (FormatException){
                return;
            }finally{
                txtName.Text = "";
            }

            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("Dodaj_Jedinicu"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ImeJedinice", ImeJedinice);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }
            }
            BindGrid();
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string NovoImeJedinice = (row.FindControl("txtName") as TextBox).Text;
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("Izmjeni_Jedinicu"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JedinicaID", id);
                    cmd.Parameters.AddWithValue("@NovoImeJedinice", NovoImeJedinice);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }
            }

            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int JedinicaID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("Obrisi_Jedinicu"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JedinicaID", JedinicaID);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }
            }
            BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
            {
                (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

    }
}