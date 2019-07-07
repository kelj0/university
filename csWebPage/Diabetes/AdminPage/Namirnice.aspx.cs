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
    public partial class Namirnice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Helper.CheckIfLoggedIn((string)Session["name"], Response))
            {
                Response.Redirect("Admin_Login.aspx");
            }
            if (!IsPostBack)
                BindGrid();
        }

        protected void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("exec Dohvati_Sve_Namirnice", con))
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

        protected void Insert(object sender, EventArgs e)
        {
            int e_kcal;
            int e_kJ;
            int kolicina;
            string NazivJedinice;
            string nazivNamirnice;
            string TipNamirnice;
            try {
                e_kcal = Convert.ToInt32(txtEnergija_kcal.Text);
                e_kJ = Convert.ToInt32(txtEnergija_kJ.Text);
                kolicina = Convert.ToInt32(txtKolicina.Text);
                NazivJedinice = txtJedinica.Text;
                nazivNamirnice = txtNaziv.Text;
                TipNamirnice = txtTip.Text;
            } catch (FormatException) {
                return;
             }
            finally {
                txtEnergija_kcal.Text = "";
                txtEnergija_kJ.Text = "";
                txtKolicina.Text = "";
                txtJedinica.Text = "";
                txtNaziv.Text = "";
                txtTip.Text = "";
            }

            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                DataTable t = new DataTable();
                int IDJedinica = -1;
                using (SqlDataAdapter a = new SqlDataAdapter("exec Dohvati_Jedinicu '" + NazivJedinice + "'", con))
                {
                    a.Fill(t);
                    IDJedinica = Convert.ToInt32(t.Rows[0]["IDJedinica"]);
                }
                t = new DataTable();
                int IDTipNamirnice = -1;
                using (SqlDataAdapter a = new SqlDataAdapter("exec Dohvati_TipNamirnice " + TipNamirnice, con))
                {
                    a.Fill(t);
                    IDTipNamirnice = Convert.ToInt32(t.Rows[0]["IDTipNamirnice"]);
                }
                using (SqlCommand cmd = new SqlCommand("Dodaj_Namirnicu"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Naziv", nazivNamirnice);
                    cmd.Parameters.AddWithValue("@TipNamirniceID", IDTipNamirnice);
                    cmd.Parameters.AddWithValue("@Energija_kcal", e_kcal);
                    cmd.Parameters.AddWithValue("@Energija_kJ", e_kJ);
                    cmd.Parameters.AddWithValue("@JedinicaID", IDJedinica);
                    cmd.Parameters.AddWithValue("@Kolicina", kolicina);
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
            int e_kcal = Convert.ToInt32((row.FindControl("txtEnergija_kcal") as TextBox).Text);
            int e_kJ = Convert.ToInt32((row.FindControl("txtEnergija_kJ") as TextBox).Text);
            int kolicina = Convert.ToInt32((row.FindControl("txtKolicina") as TextBox).Text);
            string NazivJedinice = (row.FindControl("txtJedinica") as TextBox).Text;
            string nazivNamirnice = (row.FindControl("txtName") as TextBox).Text;
            string TipNamirnice = (row.FindControl("txtTip") as TextBox).Text;
            int NamirnicaID = Convert.ToInt32((row.FindControl("txtIDNamirnica") as TextBox).Text);
            
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                DataTable t = new DataTable();
                int IDJedinica = -1;
                using (SqlDataAdapter a = new SqlDataAdapter("exec Dohvati_Jedinicu " + NazivJedinice, con))
                {
                    a.Fill(t);
                    IDJedinica = Convert.ToInt32(t.Rows[0]["IDJedinica"]);
                }
                t = new DataTable();
                int IDTipNamirnice = -1;
                using (SqlDataAdapter a = new SqlDataAdapter("exec Dohvati_TipNamirnice " + TipNamirnice, con))
                {
                    a.Fill(t);
                    IDTipNamirnice = Convert.ToInt32(t.Rows[0]["IDTipNamirnice"]);
                }

                using (SqlCommand cmd = new SqlCommand("Izmjeni_Namirnicu"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDNamirnica", NamirnicaID);
                    cmd.Parameters.AddWithValue("@NovoNaziv", nazivNamirnice);
                    cmd.Parameters.AddWithValue("@NovoTipNamirniceID", IDTipNamirnice);
                    cmd.Parameters.AddWithValue("@NovoEnergija_kcal", e_kcal);
                    cmd.Parameters.AddWithValue("@NovoEnergija_kJ", e_kJ);
                    cmd.Parameters.AddWithValue("@NovoJedinicaID", IDJedinica);
                    cmd.Parameters.AddWithValue("@NovoKolicina", kolicina);
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
            int NamirnicaID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("Obrisi_Namirnicu"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDNamirnica", NamirnicaID);
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

        protected void namirnice_home_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminHome.aspx");
        }
    }
}