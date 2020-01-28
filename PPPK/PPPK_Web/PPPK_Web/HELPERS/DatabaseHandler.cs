using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPPK_Web.HELPERS
{
    public static class DatabaseHandler
    {
        public static string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["PPPK_DATABASE"].ConnectionString;

        /// <summary>
        /// Updatea vozaca
        /// </summary>
        /// <param name="ID">Vozac ID</param>
        /// <param name="ime">Ime vozaca</param>
        /// <param name="prezime">Prezime vozaca</param>
        /// <param name="broj_mobitela">Broj mobitela</param>
        /// <param name="broj_vozacke">Broj vozacke</param>
        /// /// <returns>
        /// true ako je update uspio, false inace
        /// </returns>
        public static bool updateVozac(int id, string ime, string prezime, string broj_mobitela, string broj_vozacke)
        {
            if (
                !Validators.validID(id) || 
                !Validators.validBrojMobitela(broj_mobitela) || 
                !Validators.validBrojVozacke(broj_vozacke))
            { return false; }

            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("update vozac set " +
                    "ime=@ime, prezime=@prezime, broj_mobitela=@broj_mobitela, broj_vozacke=@broj_vozacke" +
                    " where id=@id", c))
                {
                    a.Parameters.AddWithValue("@ime", ime);
                    a.Parameters.AddWithValue("@prezime", prezime);
                    a.Parameters.AddWithValue("@broj_mobitela", broj_mobitela);
                    a.Parameters.AddWithValue("@broj_vozacke", broj_vozacke);
                    a.Parameters.AddWithValue("@id", id);
                    return (a.ExecuteNonQuery() == 0) ? false : true;
                }
            }

        }

   
        public static bool insertVozilo(string marka, int? tip_vozila_id, decimal trenutni_km, decimal pocetni_km, int godina_proizvodnje)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Brise vozaca
        /// </summary>
        /// <param name="id">Vozac id</param>
        /// /// <returns>
        /// True ako je obrisan, false inace
        /// </returns>
        public static bool deleteVozac(int id)
        {
            if (!Validators.validID(id)){ return false; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("obrisi_vozaca", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@id", id);
                    return (a.ExecuteNonQuery() == 0) ? false : true;
                }
            }
        }

        /// <summary>
        /// Inserta vozaca
        /// </summary>
        /// <param name="ime">Ime vozaca</param>
        /// <param name="prezime">Prezime vozaca</param>
        /// <param name="broj_mobitela">Broj mobitela</param>
        /// <param name="broj_vozacke">Broj vozacke</param>
        /// /// <returns>
        /// ID insertanog vozaca, 0 ako nije insertao
        /// </returns>
        public static int insertVozac(string ime, string prezime, string broj_mobitela, string broj_vozacke)
        {
            if (
                !Validators.validBrojMobitela(broj_mobitela) ||
                !Validators.validBrojVozacke(broj_vozacke))
            { return 0; }

            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("insert_vozac", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@ime", ime);
                    a.Parameters.AddWithValue("@prezime", prezime);
                    a.Parameters.AddWithValue("@broj_mobitela", broj_mobitela);
                    a.Parameters.AddWithValue("@broj_vozacke", broj_vozacke);
                    int cc = (int)a.ExecuteScalar();
                    return cc;
                }
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
            if (!Validators.validID(ID)) { return null; }
            using(SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using(SqlDataAdapter a = new SqlDataAdapter("select * from vozac where id=@ID", c))
                {
                    a.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = ID,
                        SqlDbType = SqlDbType.Int
                    });
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
            if (!Validators.validID(ID)) { return null; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter($"select * from vozilo where id=@ID", c))
                {
                    a.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = ID,
                        SqlDbType = SqlDbType.Int
                    });
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
        /// Dohvaca sva vozila
        /// /// <returns>
        /// List<vozilo> or null
        /// </returns>
        public static List<vozilo> getAllVozila()
        {
            List<vozilo> filler = new List<vozilo>();
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("select * from vozilo", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if (t.Rows.Count > 0)
                    {
                        foreach (DataRow dr in t.Rows)
                        {
                            vozilo v = new vozilo
                            {
                                id = Convert.ToInt16(dr["id"]),
                                tip_vozila_id = Convert.ToInt16(dr["tip_vozila_id"]),
                                marka = Convert.ToString(dr["marka"]),
                                pocetni_km = Convert.ToDecimal(dr["pocetni_km"]),
                                trenutni_km = Convert.ToDecimal(dr["trenutni_km"]),
                                godina_proizvodnje = Convert.ToInt16(dr["godina_proizvodnje"])
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
        /// Dohvaca sve servise napravljene na vozilu
        /// </summary>
        /// <param int="ID">Vozilo ID</param>
        /// /// <returns>
        /// List<servi> or null
        /// </returns>
        public static List<servi> getServisi(int ID)
        {
            if (!Validators.validID(ID)) { return null; }
            List<servi> filler = new List<servi>();
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("select * from servis where id=@ID", c))
                {
                    a.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = ID,
                        SqlDbType = SqlDbType.Int
                    });
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
            if (!Validators.validID(ID)) { return null; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("select * from tip_vozila where id=@ID", c))
                {
                    a.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = ID,
                        SqlDbType = SqlDbType.Int
                    });
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


        /// <summary>
        /// Dohvaca sve tipove vozila
        /// </summary>
        /// /// <returns>
        /// List<tip_vozila> ili null
        /// </returns>
        public static List<tip_vozila> getAllTipVozila()
        {
            List<tip_vozila> filler = new List<tip_vozila>();
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("select * from tip_vozila", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if (t.Rows.Count > 0)
                    {
                        foreach (DataRow dr in t.Rows)
                        {
                            tip_vozila tv = new tip_vozila
                            {
                                id = Convert.ToInt16(dr["id"]),
                                tip = Convert.ToString(dr["tip"])
                            };
                            filler.Add(tv);
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

    }
}