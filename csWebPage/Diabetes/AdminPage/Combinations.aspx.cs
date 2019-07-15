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
    public partial class Combinations : System.Web.UI.Page
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
                using (SqlDataAdapter sda = new SqlDataAdapter("exec Dohvati_Sve_Kombinacije", con))
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
            int BrojObroka;
            DateTime DatumKreiranja = DateTime.Today;
            DateTime VrijediDo;

            try
            {
                BrojObroka = Convert.ToInt32(txtBrojObroka.Text);
                VrijediDo = DateTime.ParseExact(txtVrijediDo.Text,"dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Session["brojobroka"] = BrojObroka;
            }
            catch (FormatException)
            {
                return;
            }
            finally
            {
                txtBrojObroka.Text = "";
                txtVrijediDo.Text = "";
            }

            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand a = new SqlCommand("Dodaj_Kombinaciju", con))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@BrojObroka", BrojObroka);
                    a.Parameters.AddWithValue("@DatumKreiranja", DatumKreiranja);
                    a.Parameters.AddWithValue("@VrijediDo", VrijediDo);
                    con.Open();
                    Session["ID"] = Convert.ToInt32(a.ExecuteScalar());
                    con.Close();
                }
            }
            ShowKombinacijaDetaljiInsert();
            BindGrid();
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int KombinacijaID;
            int brojObroka;
            DateTime VrijediDo;
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                KombinacijaID = Convert.ToInt32((row.FindControl("txtIDKombinacija") as TextBox).Text);
                brojObroka = Convert.ToInt32((row.FindControl("txtBrojObroka") as TextBox).Text);
                VrijediDo = DateTime.ParseExact(
                    (row.FindControl("txtVrijediDo") as TextBox).Text,
                    "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
            }catch (FormatException)
            {
                return;
            }
            Session["ID"] = KombinacijaID;
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                DataTable t = new DataTable();
                using (SqlDataAdapter aa = new SqlDataAdapter("Dohvati_Kombinaciju", con))
                {
                    aa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    aa.SelectCommand.Parameters.AddWithValue("@KombinacijaID", KombinacijaID);
                    aa.Fill(t);
                    int br = Convert.ToInt32(t.Rows[0]["BrojObroka"]);
                    Session["brojobroka"] = br;
                    if(br > brojObroka)
                    {
                        SqlCommand tmp = new SqlCommand($"delete TOP({br-brojObroka}) from KombinacijaDetalji where KombinacijaID = {Session["ID"]}", con);
                        con.Open();
                        tmp.ExecuteScalar();
                        con.Close();
                    }else if(br < brojObroka){
                        ShowKombinacijaDetaljiInsert();
                    }
                }
                using (SqlCommand a = new SqlCommand("Izmjeni_Kombinaciju", con))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@KombinacijaID", KombinacijaID);
                    a.Parameters.AddWithValue("@BrojObroka", brojObroka);
                    a.Parameters.AddWithValue("@DatumKreiranja", DateTime.Today);
                    a.Parameters.AddWithValue("@VrijediDo", VrijediDo);
                    con.Open();
                    a.ExecuteScalar();
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
            int KombinacijaID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("Obrisi_Kombinaciju", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KombinacijaID", KombinacijaID);
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


        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (
                        Request.Form.AllKeys.Any(x => x.Contains("txtnazivObrokaID")) &&
                        Request.Form.AllKeys.Any(x => x.Contains("txtpUgljikogidrata")) &&
                        Request.Form.AllKeys.Any(x => x.Contains("txtpMasti")) &&
                        Request.Form.AllKeys.Any(x => x.Contains("txtpProteina")) &&
                        Request.Form.AllKeys.Any(x => x.Contains("txtpUkupno"))
                    )
                {
                    for (int i = 0; i < Convert.ToInt32(Session["brojobroka"]); i++)
                    {
                        TextBox txtnazivObrokaID = new TextBox();
                        TextBox txtpUgljikogidrata = new TextBox();
                        TextBox txtpMasti = new TextBox();
                        TextBox txtpProteina = new TextBox();
                        TextBox txtpUkupno = new TextBox();
                        TableCell nazivObrokaID = new TableCell();
                        TableCell pUgljikogidrata = new TableCell();
                        TableCell pMasti = new TableCell();
                        TableCell pProteina = new TableCell();
                        TableCell pUkupno = new TableCell();
                        txtnazivObrokaID.Width = 176;
                        txtpUgljikogidrata.Width = 176;
                        txtpMasti.Width = 176;
                        txtpProteina.Width = 176;
                        txtpUkupno.Width = 176;
                        txtnazivObrokaID.Attributes.Add("placeholder", "Naziv obrokaID");
                        txtpUgljikogidrata.Attributes.Add("placeholder", "pUgljikohidrata");
                        txtpMasti.Attributes.Add("placeholder", "pMasti");
                        txtpProteina.Attributes.Add("placeholder", "pProteina");
                        txtpUkupno.Attributes.Add("placeholder", "pUkupno");
                        TableRow row = new TableRow();
                        txtnazivObrokaID.ID = "txtnazivObrokaID" + i;
                        txtpUgljikogidrata.ID = "txtpUgljikogidrata" + i;
                        txtpMasti.ID = "txtpMasti" + i;
                        txtpProteina.ID = "txtpProteina" + i;
                        txtpUkupno.ID = "txtpUkupno" + i;
                        nazivObrokaID.Controls.Add(txtnazivObrokaID);
                        pUgljikogidrata.Controls.Add(txtpUgljikogidrata);
                        pMasti.Controls.Add(txtpMasti);
                        pProteina.Controls.Add(txtpProteina);
                        pUkupno.Controls.Add(txtpUkupno);
                        row.Cells.Add(nazivObrokaID);
                        row.Cells.Add(pUgljikogidrata);
                        row.Cells.Add(pMasti);
                        row.Cells.Add(pProteina);
                        row.Cells.Add(pUkupno);
                        tblKombinacijaDetalji.Rows.Add(row);
                        System.Diagnostics.Debug.WriteLine("Added new row!");
                    }
                }
            }
            catch (NullReferenceException) { }
        }


        private void ShowKombinacijaDetaljiInsert()
        {
            btnAdd.Visible = false;
            tblKombinacija.Visible = false;
            for (int i = 0; i < Convert.ToInt32(Session["brojobroka"]); ++i)
            {
                TextBox txtnazivObrokaID = new TextBox();
                TextBox txtpUgljikogidrata = new TextBox();
                TextBox txtpMasti = new TextBox();
                TextBox txtpProteina = new TextBox();
                TextBox txtpUkupno = new TextBox();
                TableCell nazivObrokaID = new TableCell();
                TableCell pUgljikogidrata = new TableCell();
                TableCell pMasti = new TableCell();
                TableCell pProteina = new TableCell();
                TableCell pUkupno = new TableCell();
                txtnazivObrokaID.Width = 176;
                txtpUgljikogidrata.Width = 176;
                txtpMasti.Width = 176;
                txtpProteina.Width = 176;
                txtpUkupno.Width = 176;
                txtnazivObrokaID.Attributes.Add("placeholder", "Naziv obrokaID");
                txtpUgljikogidrata.Attributes.Add("placeholder", "pUgljikohidrata");
                txtpMasti.Attributes.Add("placeholder", "pMasti");
                txtpProteina.Attributes.Add("placeholder", "pProteina");
                txtpUkupno.Attributes.Add("placeholder", "pUkupno");
                TableRow row = new TableRow();
                txtnazivObrokaID.ID = "txtnazivObrokaID" + i;
                txtpUgljikogidrata.ID = "txtpUgljikogidrata"+i;
                txtpMasti.ID = "txtpMasti" + i;
                txtpProteina.ID = "txtpProteina" + i;
                txtpUkupno.ID = "txtpUkupno" + i;
                nazivObrokaID.Controls.Add(txtnazivObrokaID);
                pUgljikogidrata.Controls.Add(txtpUgljikogidrata);
                pMasti.Controls.Add(txtpMasti);
                pProteina.Controls.Add(txtpProteina);
                pUkupno.Controls.Add(txtpUkupno);
                row.Cells.Add(nazivObrokaID);
                row.Cells.Add(pUgljikogidrata);
                row.Cells.Add(pMasti);
                row.Cells.Add(pProteina);
                row.Cells.Add(pUkupno);
                tblKombinacijaDetalji.Rows.Add(row);
            }

            pnlDetalji.Visible = true;
        }


        protected void kombinacije_home_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminHome.aspx");
        }

        protected void btnAddDetalji_Click(object sender, EventArgs e)
        {
            List<int> sumCheck = new List<int>() {0};
            for(int i = 0;i< Convert.ToInt32(Session["brojobroka"]); ++i )
            {
                int pUgljikohidrata = Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[1].Controls[0] as TextBox).Text);
                int pMasti =          Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[2].Controls[0] as TextBox).Text);
                int pProteina =       Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[3].Controls[0] as TextBox).Text);
                int pUkupno =         Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[4].Controls[0] as TextBox).Text);

                sumCheck[0] += pUkupno;
                sumCheck.Add(pUgljikohidrata + pMasti + pProteina);
            }
            if (sumCheck.Any(x=>x!=100))
            {
                lblError.Visible = true;
                return;
            }
            lblError.Visible = false;
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {

                for (int i = 0; i < Convert.ToInt32(Session["brojobroka"]); ++i)
                {
                    using (SqlCommand cmd = new SqlCommand("Dodaj_KombinacijaDetalji", con))
                    {
                        int NazivObrokaID =   Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[0].Controls[0] as TextBox).Text);
                        int pUgljikohidrata = Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[1].Controls[0] as TextBox).Text);
                        int pMasti =          Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[2].Controls[0] as TextBox).Text);
                        int pProteina =       Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[3].Controls[0] as TextBox).Text);
                        int pUkupno =         Convert.ToInt32((tblKombinacijaDetalji.Rows[i].Cells[4].Controls[0] as TextBox).Text);

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KombinacijaID", Convert.ToInt32(Session["ID"]));
                        cmd.Parameters.AddWithValue("@NazivObrokaID",NazivObrokaID);
                        cmd.Parameters.AddWithValue("@Uglj",pUgljikohidrata);
                        cmd.Parameters.AddWithValue("@Mast",pMasti);
                        cmd.Parameters.AddWithValue("@Prot",pProteina);
                        cmd.Parameters.AddWithValue("@Ukup",pUkupno);
                        con.Open();
                        cmd.ExecuteScalar();
                        con.Close();
                    }
                }
            }

            BindGrid();
            pnlDetalji.Visible = false;
            btnAdd.Visible = true;
            tblKombinacija.Visible = true;
        }
    }
}
