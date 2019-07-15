using HelperNamespace;
using PublicSite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(String.IsNullOrWhiteSpace((string)Session["id"])) { Session["id"] = 0; }
            if (!Helper.CheckIfLoggedInMVC(Convert.ToInt32(Session["id"])))
            {
                return RedirectToAction("Login", "User");
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            using (SqlConnection c = new SqlConnection(Helper.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("Odjavi_Korisnika", c))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = Session["name"];
                    c.Open();
                    cmd.ExecuteScalar();
                    c.Close();
                    Session["name"] = "";
                    Session["id"] = -1;
                }
            }

            return RedirectToAction("Login", "User");
        }

        public ActionResult GenerateMenu(int NumberOfMeals = 0)
        {
            if (String.IsNullOrWhiteSpace((string)Session["id"])) { Session["id"] = 0; }
            if (!Helper.CheckIfLoggedInMVC(Convert.ToInt32(Session["id"]))){
                return RedirectToAction("Login", "User");
            }

            User userinfo = new User();
            int id;
            string username = Session["name"].ToString();
            int userEnergyNeeded;

            Kombinacija combination = new Kombinacija();
            List<KombinacijaDetalji> combinationDetailsList = new List<KombinacijaDetalji>();
            List<Namirnica> goodsList = new List<Namirnica>();
            List<Jedinica> MeasuringUnitsList = new List<Jedinica>();
            List<NazivObroka> MealNamesList = new List<NazivObroka>();
            List<TipNamirnice> FoodTypeList = new List<TipNamirnice>();
            // get all available number of meals per menu in database
            List<int> available_number_of_meals = new List<int>();
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new SqlCommand("select * from Kombinacija", con))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            available_number_of_meals.Add(rd.GetInt32(rd.GetOrdinal("BrojObroka")));
                            if(rd.GetInt32(rd.GetOrdinal("BrojObroka")) == NumberOfMeals)
                            {
                                combination.BrojObroka = NumberOfMeals;
                                combination.DatumKreiranja = rd.GetDateTime(rd.GetOrdinal("DatumKreiranja"));
                                combination.IDKombinacija = rd.GetInt32(rd.GetOrdinal("IDKombinacija"));
                                combination.VrijediDo = rd.GetDateTime(rd.GetOrdinal("VrijediDo"));
                            }
                        }
                    }
                }
                con.Close();
            }
           
            // return if no of meals is 0 to avoid error message showing on start
            if(NumberOfMeals == 0)
            {
                return View(new ViewModels { ErrorMessage = "" });
            }
            // return if there is no combination with that number of meals
            if (available_number_of_meals.All(x=>x!=NumberOfMeals))
            {
                return View(new ViewModels { ErrorMessage = "Cant find meal combination with that number of meals" });
            }

            // fill combinationDetails array
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new SqlCommand("select * from KombinacijaDetalji where KombinacijaID = " + combination.IDKombinacija, con))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            combinationDetailsList.Add(
                                new KombinacijaDetalji
                                {
                                    IDKombinacijaDetalji = rd.GetInt32(rd.GetOrdinal("IDKombinacijaDetalji")),
                                    NazivObrokaID = rd.GetInt32(rd.GetOrdinal("NazivObrokaID")),
                                    KombinacijaID = combination.IDKombinacija,
                                    PostotakMasti = rd.GetInt32(rd.GetOrdinal("PostotakMasti")),
                                    PostotakProteina = rd.GetInt32(rd.GetOrdinal("PostotakProteina")),
                                    PostotakUgljikohidrata = rd.GetInt32(rd.GetOrdinal("PostotakUgljikohidrata")),
                                    PostotakUkupno = rd.GetInt32(rd.GetOrdinal("PostotakUkupno")),
                                });
                        }
                    }
                }
                con.Close();
            }

            // get user energy needed
            using (SqlConnection con2 = new SqlConnection(Helper.CONNECTION_STRING))
            {
                DataTable t = new DataTable();
                using (SqlDataAdapter aa = new SqlDataAdapter("select * from Korisnik where KorisnickoIme = '" + username + "'", con2))
                {
                    aa.SelectCommand.CommandType = CommandType.Text;
                    aa.Fill(t);
                    userinfo.Height = Convert.ToInt32(t.Rows[0]["Visina"]);
                    userinfo.Weight = Convert.ToInt32(t.Rows[0]["Tezina"]);
                    userinfo.Sex = Convert.ToInt32(t.Rows[0]["SpolID"]);
                    userinfo.Diabetes_type = Convert.ToInt32(t.Rows[0]["TipDia"]);
                    userinfo.Activity = Convert.ToInt32(t.Rows[0]["AktivnostID"]);
                }
                double Activity = 1;
                double diabtype = 1;
                double gender;
                if (userinfo.Activity == 1) { Activity = 1.2; }
                else if (userinfo.Activity == 2) { Activity = 1.375; }
                else if (userinfo.Activity == 3) { Activity = 1.5; }

                if (userinfo.Diabetes_type == 1) { diabtype = 0.99; }
                else if (userinfo.Diabetes_type == 2) { diabtype = 0.98; }
                if (userinfo.Sex == 1) { gender = 5; }
                else { gender = -161; }

                userEnergyNeeded = Helper.Get_kcal(userinfo.Weight,userinfo.Height,gender, Activity,diabtype,20);
            }

            // calculate calories for each type
            int kcalPerMeal = userEnergyNeeded / combination.BrojObroka;
            List<int> kcalProtein = new List<int>();
            List<int> kcalFat = new List<int>();
            List<int> kcalCarbs = new List<int>();
            List<int> kcalAll = new List<int>();
            foreach (KombinacijaDetalji combo in combinationDetailsList)
            {
                kcalAll.Add((int)(userEnergyNeeded * (combo.PostotakUkupno/100.0)));
                kcalProtein.Add((int)(kcalAll[kcalAll.Count-1]*(combo.PostotakProteina/100.0)));
                kcalFat.Add((int)(kcalAll[kcalAll.Count-1] * (combo.PostotakMasti / 100.0)));
                kcalCarbs.Add((int)(kcalAll[kcalAll.Count-1] * (combo.PostotakUgljikohidrata / 100.0)));
            }

            // fill goods array to work with
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new SqlCommand("select * from Namirnica", con))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            goodsList.Add(
                                new Namirnica
                                {
                                    Naziv = rd.GetString(rd.GetOrdinal("Naziv")),
                                    TipNamirniceID = rd.GetInt32(rd.GetOrdinal("TipNamirniceID")),
                                    JedinicaID = rd.GetInt32(rd.GetOrdinal("JedinicaID")),
                                    Energija_kcal = rd.GetString(rd.GetOrdinal("Energija_kcal")),
                                    IDNamirnica = rd.GetInt32(rd.GetOrdinal("IDNamirnica"))
                                    
                                });
                        }
                    }
                }
                con.Close();
            }

            // get goods to show it to user
            Random r = new Random();
            List<Namirnica> proteinGoods = new List<Namirnica>();
            List<Namirnica> carbsGoods = new List<Namirnica>();
            List<Namirnica> fatGoods = new List<Namirnica>();
            for(int i = 0; i < combination.BrojObroka;++i)
            {
                List<Namirnica> goodProteinGoods = goodsList.Where(x => Convert.ToInt32(x.Energija_kcal) < kcalProtein[i] && x.TipNamirniceID == 2).ToList();
                List<Namirnica> goodCarbsGoods = goodsList.Where(x => Convert.ToInt32(x.Energija_kcal) < kcalCarbs[i] && x.TipNamirniceID == 1).ToList();
                List<Namirnica> goodFatGoods = goodsList.Where(x => Convert.ToInt32(x.Energija_kcal) < kcalFat[i] && x.TipNamirniceID == 3).ToList();
                // if i cant find any good to add, add good with least kcal
                if (goodProteinGoods.Count == 0) { goodProteinGoods.Add(goodsList.Where(x => x.TipNamirniceID == 2).ToList().OrderBy(x => Convert.ToInt32(x.Energija_kcal)).First()); }
                if (goodCarbsGoods.Count == 0){ goodCarbsGoods.Add(goodsList.Where(x => x.TipNamirniceID == 1).ToList().OrderBy(x => Convert.ToInt32(x.Energija_kcal)).First()); }
                if (goodFatGoods.Count == 0) { goodFatGoods.Add(goodsList.Where(x => x.TipNamirniceID == 3).ToList().OrderBy(x => Convert.ToInt32(x.Energija_kcal)).First()); }
                proteinGoods.Add(goodProteinGoods[r.Next(0,goodProteinGoods.Count)]);
                carbsGoods.Add(goodCarbsGoods[r.Next(0, goodCarbsGoods.Count)]);
                fatGoods.Add(goodFatGoods[r.Next(0, goodFatGoods.Count)]);
            }

            // fill measuring units
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                con.Open();

                string query = "select j.* from Jedinica as j " +
                               "inner join Namirnica as n on n.JedinicaID = j.IDJedinica " +
                               "inner join Obrok as o on o.NamirnicaID = n.IDNamirnica " +
                               "inner join NazivObroka as [no] on[no].IDNazivObroka = o.NazivObrokaID " +
                               "inner join KombinacijaDetalji as kd on kd.NazivObrokaID = [no].IDNazivObroka " +
                               "inner join Kombinacija as k on k.IDKombinacija = kd.KombinacijaID " + 
                               "where k.IDKombinacija = " + combination.IDKombinacija;
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            MeasuringUnitsList.Add(new Jedinica{Jedinica1 = rd.GetString(rd.GetOrdinal("Jedinica"))});
                        }
                    }
                }
                con.Close();
            }
            MeasuringUnitsList.Add(new Jedinica { Jedinica1 = "g" });
            // fill meal names
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                con.Open();
                string q = "select [no].* from NazivObroka as [no] " +
                       "inner join KombinacijaDetalji as kd on kd.NazivObrokaID = [no].IDNazivObroka " +
                       "inner join Kombinacija as k on k.IDKombinacija = kd.KombinacijaID " +
                       "where k.IDKombinacija = " + combination.IDKombinacija;
                using (var cmd = new SqlCommand(q, con))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            MealNamesList.Add(new NazivObroka { Ime = rd.GetString(rd.GetOrdinal("Ime")) });
                        }
                    }
                }
                con.Close();
            }

            // fill foodtypes
            using (SqlConnection con = new SqlConnection(Helper.CONNECTION_STRING))
            {
                con.Open();
                string q = "select t.* from TipNamirnice as t " +
                       "inner join Namirnica as n on n.TipNamirniceID = t.IDTipNamirnice " +
                       "inner join Obrok as o on o.NamirnicaID = n.IDNamirnica " +
                       "inner join NazivObroka as [no] on[no].IDNazivObroka = o.NazivObrokaID " +
                       "inner join KombinacijaDetalji as kd on kd.NazivObrokaID = [no].IDNazivObroka " +
                       "inner join Kombinacija as k on k.IDKombinacija = kd.KombinacijaID " +
                       "where k.IDKombinacija = " + combination.IDKombinacija;
                using (var cmd = new SqlCommand(q, con))
                {
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            FoodTypeList.Add(new TipNamirnice { Tip = rd.GetString(rd.GetOrdinal("Tip")) });
                        }
                    }
                }
                con.Close();
            }

            System.Diagnostics.Debug.WriteLine("PROTEINS");
            proteinGoods.ForEach(x => System.Diagnostics.Debug.WriteLine(x.Naziv));
            System.Diagnostics.Debug.WriteLine("CARBS");
            carbsGoods.ForEach(x => System.Diagnostics.Debug.WriteLine(x.Naziv));
            System.Diagnostics.Debug.WriteLine("FATS");
            fatGoods.ForEach(x => System.Diagnostics.Debug.WriteLine(x.Naziv));

            var viewmodel = new ViewModels
            {
                Fats = fatGoods,
                Carbs = carbsGoods,
                Proteins = proteinGoods,
                MealNames = MealNamesList,
                MeasuringUnits = MeasuringUnitsList,
                FoodTypes = FoodTypeList,
                ErrorMessage = null,
                NumberOfMeals = NumberOfMeals
            };
            return View(viewmodel);
        }
    }
}
