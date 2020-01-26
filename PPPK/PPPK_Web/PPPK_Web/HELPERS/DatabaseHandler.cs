using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PPPK_Web.HELPERS
{
    public static class DatabaseHandler
    {
        public static string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["PPPK_DATABASE"].ConnectionString;

        /// <summary>
        /// Provjerava valjanost ID-a 
        /// </summary>
        /// <param name="ID">ID</param>
        /// /// <returns>
        /// true ako je valjani id, inace false
        /// </returns>
        public static bool validID(int? ID)
        {
            if (ID == 0 || !Regex.IsMatch(ID.ToString(), @"^\d+$")) {
                return false;
            }else{
                return true;
            }
        }

        /// <summary>
        /// Dohvaca vozac
        /// </summary>
        /// <param name="ID">Vozac ID</param>
        /// /// <returns>
        /// vozac or null
        /// </returns>
        public static vozac getVozac(int ID)
        {
            if (!validID(ID)) { return null; }
            using(SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using(SqlDataAdapter a = new SqlDataAdapter("select * from vozac where id="+ID, c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if(t.Rows.Count > 0)
                    {
                        vozac v = new vozac
                        {
                            id = Convert.ToInt16(t.Rows[0]["id"]),
                            broj_mobitela = Convert.ToString(t.Rows[0]["broj_mobitela"]),
                            broj_vozacke = Convert.ToString(t.Rows[0]["broj_vozacke"]),
                            ime = Convert.ToString(t.Rows[0]["ime"]),
                            prezime = Convert.ToString(t.Rows[0]["prezime"])
                        };
                        return v;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Dohvaca sve vozace
        /// </summary>
        /// /// <returns>
        /// List<vozac> or null
        /// </returns>
        public static List<vozac> getAllVozaci()
        {
            List<vozac> filler = new List<vozac>();
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("select * from vozac", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if (t.Rows.Count > 0)
                    {
                        foreach(DataRow dr in t.Rows)
                        { 
                            vozac v = new vozac
                            {
                                id = Convert.ToInt16(dr["id"]),
                                broj_mobitela = Convert.ToString(dr["broj_mobitela"]),
                                broj_vozacke = Convert.ToString(dr["broj_vozacke"]),
                                ime = Convert.ToString(dr["ime"]),
                                prezime = Convert.ToString(dr["prezime"])
                            };
                            filler.Add(v);
                        }
                        return filler;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Dohvaca vozilo
        /// </summary>
        /// <param name="ID">Vozilo ID</param>
        /// /// <returns>
        /// vozilo or null
        /// </returns>
        public static vozilo getVozilo(int ID)
        {
            if (!validID(ID)) { return null; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter($"select * from vozilo where id={ID}", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if (t.Rows.Count > 0)
                    {
                        vozilo v = new vozilo
                        {
                            id = Convert.ToInt16(t.Rows[0]["id"]),
                            tip_vozila_id = Convert.ToInt16(t.Rows[0]["tip_vozila_id"]),
                            marka = Convert.ToString(t.Rows[0]["marka"]),
                            pocetni_km = Convert.ToDecimal(t.Rows[0]["pocetni_km"]),
                            trenutni_km = Convert.ToDecimal(t.Rows[0]["trenutni_km"]),
                            godina_proizvodnje = Convert.ToInt16(t.Rows[0]["godina_proizvodnje"])
                        };
                        return v;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Dohvaca sve servise napravljene na vozilu
        /// </summary>
        /// <param int="ID">Vozilo ID</param>
        /// /// <returns>
        /// List<servi> or null
        /// </returns>
        public static List<servi> getServisi(int ID)
        {
            if (!validID(ID)) { return null; }
            List<servi> filler = new List<servi>();
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter($"select * from servis where id={ID}", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if (t.Rows.Count > 0)
                    {
                        foreach(DataRow row in t.Rows)
                        {
                            filler.Add(new servi
                            {
                                id = Convert.ToInt16(row["id"]),
                                cijena = Convert.ToDecimal(row["cijena"]),
                                datum_servisa = Convert.ToDateTime(row["datum_servisa"]),
                                info = Convert.ToString(row["info"]),
                                naziv_servisa = Convert.ToString(row["naziv_servisa"]),
                                vozilo_id = Convert.ToInt16(row["vozilo_id"])
                            });
                        }
                        return filler;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Dohvaca tip vozila
        /// </summary>
        /// <param int="ID">Tip vozila ID</param>
        /// /// <returns>
        /// <c>tip_vozila</c> ili null
        /// </returns>
        public static tip_vozila getTipVozila(int ID)
        {
            if (!validID(ID)) { return null; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter($"select * from tip_vozila where id={ID}", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if (t.Rows.Count > 0)
                    {
                        tip_vozila tv = new tip_vozila
                        {
                            id = Convert.ToInt16(t.Rows[0]["id"]),
                            tip = Convert.ToString(t.Rows[0]["tip"])
                        };
                        return tv;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


    }
}