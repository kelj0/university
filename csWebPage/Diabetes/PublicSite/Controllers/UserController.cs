using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublicSite.Models;
using HelperNamespace;
using HashHelper;

namespace PublicSite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View(getModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            string message = null;
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
                {
                    string p;
                    using (SqlCommand cmd = new SqlCommand("Dodaj_Korisnika", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Korisnicko_ime", user.Username);
                        cmd.Parameters.AddWithValue("@Lozinka", HashingHelper.GetHash(user.Password, HashingHelper.GenerateNewSalt()));
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Ime", user.FName);
                        cmd.Parameters.AddWithValue("@Prezime", user.LName);
                        cmd.Parameters.AddWithValue("@DatumRodenja", user.Birth);
                        cmd.Parameters.AddWithValue("@Visina", user.Height);
                        cmd.Parameters.AddWithValue("@Tezina", user.Weight);
                        cmd.Parameters.AddWithValue("@IDAktivnost", user.Activity);
                        cmd.Parameters.AddWithValue("@TipDia", user.Diabetes_type);
                        cmd.Parameters.AddWithValue("@IDSpol", user.Sex);
                        con.Open();
                        p = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    if (p == "1") {
                        return Redirect("Login");
                    } else {
                        message = (p == "-1" ? "That username is in use" : "That email is in use");
                    }
                }
            }
            ViewBag.Message = message;
            return View(getModel());
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            ViewBag.Message = null;
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
                {
                    string p = "n";
                    int id = -1;
                    System.Diagnostics.Debug.WriteLine("Going to query");
                    using (SqlCommand c = new SqlCommand("select Lozinka from Korisnik where KorisnickoIme = '" + user.Username + "'", con))
                    {
                        c.Connection = con;
                        con.Open();
                        try
                        {
                            System.Diagnostics.Debug.WriteLine("Trying to query");
                            p = c.ExecuteScalar().ToString();
                            System.Diagnostics.Debug.WriteLine("DONE");
                        }
                        catch
                        {
                            System.Diagnostics.Debug.WriteLine("NOPE");
                            ViewBag.Message = "Wrong username or password";
                            return View(user);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                    if (p.Length <= 1)
                    {
                        ViewBag.Message = "Wrong username or password";
                        return View(user);
                    }

                    string pass = HashingHelper.GetHash(user.Password, HashingHelper.GetSaltFromPassword(p));

                    using (SqlCommand cmd = new SqlCommand("Provjeri_Korisnika", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KorisnickoIme", user.Username);
                        cmd.Parameters.AddWithValue("@Lozinka", pass);
                        con.Open();
                        var t = cmd.ExecuteScalar().ToString();
                        con.Close();
                        System.Diagnostics.Debug.WriteLine($"Checking user... {t}");
                        if (t == "1")
                        {
                            SqlCommand login = new SqlCommand("Prijavi_Korisnika", con);
                            login.CommandType = CommandType.StoredProcedure;
                            login.Parameters.AddWithValue("@KorisnickoIme", user.Username);
                            con.Open();
                            login.ExecuteScalar();
                            con.Close();

                            using (SqlCommand cm = new SqlCommand("Dohvati_Korisnik_ID", con))
                            {
                                cm.CommandType = CommandType.StoredProcedure;
                                cm.Parameters.AddWithValue("@KorisnickoIme", user.Username);
                                con.Open();
                                var s = cm.ExecuteScalar().ToString();
                                System.Diagnostics.Debug.WriteLine(s);
                                Session["id"] = s.ToString();
                                Session["name"] = user.Username;
                                con.Close();
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Message = "Wrong username or password";
                            return View(user);
                        }
                    }
                }
            }
            return View();
        }

        private UserAdditionalModel getModel()
        {
            UserAdditionalModel model = new UserAdditionalModel();
            List<SelectListItem> sex = new List<SelectListItem>();
            List<SelectListItem> activity = new List<SelectListItem>();
            model.d = new List<SelectListItem>
            {
                new SelectListItem{Value = "1", Text="1"},
                new SelectListItem{Value = "2", Text="2"}
            };
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                DataTable t = new DataTable();
                using (SqlDataAdapter com = new SqlDataAdapter("select * from Aktivnost", con))
                {
                    com.Fill(t);
                    foreach (DataRow row in t.Rows)
                    {
                        sex.Add(new SelectListItem { Value = row["IDAktivnost"].ToString(), Text = row["Razina"].ToString() });
                    }
                }
                t = new DataTable();
                using (SqlDataAdapter com = new SqlDataAdapter("select * from Spol", con))
                {
                    com.Fill(t);
                    foreach (DataRow row in t.Rows)
                    {
                        activity.Add(new SelectListItem { Value = row["IDSpol"].ToString(), Text = row["Spol"].ToString() });
                    }
                }
            }
            model.s = sex;
            model.a = activity;
            return model;
        }
    }
}
