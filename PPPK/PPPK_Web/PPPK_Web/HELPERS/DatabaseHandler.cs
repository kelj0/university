using PPPK_Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace PPPK_Web.HELPERS
{
    public static class DatabaseHandler
    {
        public static string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["PPPK_DATABASE"].ConnectionString;
        public static string DATA_DIR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"../DATA");
        private static List<string> TABLE_NAMES = new List<string> {
            "tip_vozila","mjesto","status","vozac","vozilo","putni_nalog","zauzece_vozilo","zauzece_vozac","servis","kupnja_goriva","ruta" };

    public static void NukeDb()
        {
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("clean_database", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.ExecuteNonQuery();
                }
                c.Close();
            }
        }

        public static void BackupDb()
        {
            foreach (DataSet tblDs in GetFullDb())
            {
                tblDs.WriteXml(Path.Combine(DATA_DIR, tblDs.DataSetName+".xml"));
            }
        }

        public static void RestoreDb()
        {
            EnableIDInsert();
            foreach (string tblname in TABLE_NAMES)
            {
                using(SqlConnection c = new SqlConnection(CONNECTION_STRING))
                {
                    DataSet ds = new DataSet(tblname);
                    ds.ReadXml(Path.Combine(DATA_DIR, tblname + ".xml"));
                    c.Open();
                    using(SqlBulkCopy cop = new SqlBulkCopy(c, SqlBulkCopyOptions.KeepIdentity, null))
                    {
                        cop.DestinationTableName = $"[dbo].[{tblname}]";
                        cop.WriteToServer(ds.Tables[0]);
                    }
                    c.Close();
                }
            }
            DisableIDInsert();
        }

        public static List<DataSet> GetFullDb()
        {
            List<DataSet> l = new List<DataSet>();

            foreach (string tblname in TABLE_NAMES)
            {
                using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
                {
                    c.Open();
                    using (SqlDataAdapter a = new SqlDataAdapter($"select * from [dbo].[{tblname}]", c))
                    {
                        DataSet t = new DataSet(tblname);
                        a.Fill(t);
                        l.Add(t);
                    }
                    c.Close();
                }
            }
            return l;
        }

        private static void EnableIDInsert()
        {
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("enable_id_insert", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.ExecuteNonQuery();
                }
                c.Close();
            }
        }

        private static void DisableIDInsert()
        {
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("disable_id_insert", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.ExecuteNonQuery();
                }
                c.Close();
            }
        }


        /// <summary>
        /// Vraca sve putne naloge zapakirane u listu PutniNalogVM-a
        /// </summary>
        /// /// <returns>
        /// List<PutniNalogVM>
        /// </returns>
        public static List<PutniNalogVM> getAllPutniNalozi()
        {
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("exec [dbo].[dohvati_sve_putne_naloge]", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    List<PutniNalogVM> lpn = new List<PutniNalogVM>();
                    if (t.Rows.Count > 0)
                    {
                        foreach (DataRow dr in t.Rows)
                        {
                            PutniNalogVM pn = new PutniNalogVM
                            {
                                putni_nalog = new putni_nalog
                                {
                                    id = Convert.ToInt16(dr["id"]),
                                    vozilo_id = Convert.ToInt16(dr["vozilo_id"]),
                                    vozac_id = Convert.ToInt16(dr["vozac_id"]),
                                    status_id = Convert.ToInt16(dr["status_id"]),
                                    datum_izrade = Convert.ToDateTime(dr["datum_izrade"]),
                                    datum_pocetka = Convert.ToDateTime(dr["datum_pocetka"]),
                                    datum_zavrsetka = Convert.ToDateTime(dr["datum_zavrsetka"])
                                },
                                status = Convert.ToString(dr["status"]),
                                vozac = new vozac
                                {
                                    ime = Convert.ToString(dr["ime"]),
                                    prezime = Convert.ToString(dr["prezime"])
                                },
                                vozilo = new vozilo
                                {
                                    marka = Convert.ToString(dr["marka"]),
                                    godina_proizvodnje = Convert.ToInt16(dr["godina_proizvodnje"])

                                },
                                tip = Convert.ToString(t.Rows[0]["tip"])
                            };
                            lpn.Add(pn);
                        }
                        return lpn;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Ubacuje novi putni nalog
        /// </summary>
        /// <param name="datum_pocetka">Datum pocetka putnog naloga</param>
        /// <param name="datum_zavrsetka">Datum zavrsetka putnog naloga</param>
        /// <param name="vozac_id">ID vozaca</param>
        /// <param name="vozilo_id">ID vozila</param>
        /// /// <returns>
        /// ID insertanog pn ili 0
        /// </returns>
        public static int insertPutniNalog(DateTime datum_pocetka, DateTime datum_zavrsetka, int vozac_id, int vozilo_id)
        {
            if (!Validators.validID(vozac_id) || !Validators.validID(vozilo_id))
            { return 0; }

            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("insert_putni_nalog", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@vozac_id ", vozac_id);
                    a.Parameters.AddWithValue("vozilo_id", vozilo_id);
                    a.Parameters.AddWithValue("@status_id", 1);
                    a.Parameters.AddWithValue("@datum_pocetka", datum_pocetka);
                    a.Parameters.AddWithValue("@datum_zavrsetka", datum_zavrsetka);
                    object result = a.ExecuteScalar();
                    result = (result == DBNull.Value) ? 0 : result;
                    int cc = Convert.ToInt32(result);
                    return cc;
                }
            }

        }

        /// <summary>
        /// Brise putni nalog
        /// </summary>
        /// <param name="id">Putni nalog ID </param>
        /// /// <returns>
        /// True ako je obrisan, false inace
        /// </returns>
        public static bool deletePutniNalog(int id)
        {
            if (!Validators.validID(id)) { return false; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("obrisi_putni_nalog", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@id", id);
                    return (a.ExecuteNonQuery() == 0) ? false : true;
                }
            }
        }
    
        /// <summary>
        /// Vraca putne nalog zapakirane u PutniNalogVM
        /// </summary>
        /// /// <returns>
        /// PutniNalogVM
        /// </returns>
        public static PutniNalogVM getPutniNalog(int id)
        {
            if (!Validators.validID(id)) { return null; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("exec [dbo].[dohvati_putni_nalog] @id", c))
                {
                    a.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = id,
                        SqlDbType = SqlDbType.Int
                    });
                    DataTable t = new DataTable();
                    a.Fill(t);
                    if (t.Rows.Count > 0)
                    {
                        PutniNalogVM pn = new PutniNalogVM
                        {
                            putni_nalog = new putni_nalog
                                {
                                    id = Convert.ToInt16(t.Rows[0]["id"]),
                                    vozilo_id = Convert.ToInt16(t.Rows[0]["vozilo_id"]),
                                    vozac_id = Convert.ToInt16(t.Rows[0]["vozac_id"]),
                                    status_id = Convert.ToInt16(t.Rows[0]["status_id"]),
                                    datum_izrade = Convert.ToDateTime(t.Rows[0]["datum_izrade"]),
                                    datum_pocetka = Convert.ToDateTime(t.Rows[0]["datum_pocetka"]),
                                    datum_zavrsetka = Convert.ToDateTime(t.Rows[0]["datum_zavrsetka"])
                                },
                            status = Convert.ToString(t.Rows[0]["status"]),
                            vozac = new vozac
                            {
                                ime = Convert.ToString(t.Rows[0]["ime"]),
                                prezime = Convert.ToString(t.Rows[0]["prezime"])
                            },
                            vozilo = new vozilo
                            {
                                marka = Convert.ToString(t.Rows[0]["marka"]),
                                godina_proizvodnje = Convert.ToInt16(t.Rows[0]["godina_proizvodnje"])
                            },
                            tip = Convert.ToString(t.Rows[0]["tip"])
                        };
                        return pn;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Brise sve rute putnog naloga
        /// </summary>
        /// <param name="id">Putni nalog ID </param>
        /// /// <returns>
        /// Broj obrisanih redova
        /// </returns>
        public static int deleteRute(int id)
        {
            if (!Validators.validID(id)) { return 0; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("obrisi_rute", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@id", id);
                    return a.ExecuteNonQuery();
                }
            }
        }
        
        /// <summary>
        /// Brise rutu
        /// </summary>
        /// <param name="id">Ruta ID</param>
        /// /// <returns>
        /// True ako je obrisan, false inace
        /// </returns>
        public static bool deleteRuta(int id)
        {
            if (!Validators.validID(id)) { return false; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("obrisi_rutu", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@id", id);
                    return (a.ExecuteNonQuery() == 0) ? false : true;
                }
            }
        }


        /// <summary>
        /// Updatea vozaca
        /// </summary>
        /// <param name="ID">Vozilo ID</param>
        /// <param name="marka">Marka vozila</param>
        /// <param name="tip_vozila_id">ID tip vozila</param>
        /// <param name="pocetni_km">Pocetni km vozila</param>
        /// <param name="trenutni_km">Trenutni km vozila</param>
        /// <param name="godina_proizvodnje">Godina proizvodnje vozila</param>
        /// /// <returns>
        /// true ako je update uspio, false inace
        /// </returns>
        public static bool updateVozilo(int id, string marka, int tip_vozila_id, decimal pocetni_km, decimal trenutni_km, int godina_proizvodnje)
        {
            if (
                !Validators.validID(id) ||
                !Validators.validGodina(godina_proizvodnje) ||
                !Validators.validKilometar(pocetni_km) ||
                !Validators.validKilometar(trenutni_km))
             { return false; }

            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("update vozilo set " +
                    "marka=@marka, tip_vozila_id=@tip_vozila_id, godina_proizvodnje=@godina_proizvodnje, pocetni_km=@pocetni_km, trenutni_km=@trenutni_km" +
                    " where id=@id", c))
                {
                    a.Parameters.AddWithValue("@id", id);
                    a.Parameters.AddWithValue("@marka", marka);
                    a.Parameters.AddWithValue("@tip_vozila_id", tip_vozila_id);
                    a.Parameters.AddWithValue("@godina_proizvodnje", godina_proizvodnje);
                    a.Parameters.AddWithValue("@pocetni_km", pocetni_km);
                    a.Parameters.AddWithValue("@trenutni_km", trenutni_km);
                    return (a.ExecuteNonQuery() == 0) ? false : true;
                }
            }
        }

        /// <summary>
        /// Brise vozilo
        /// </summary>
        /// <param name="id">Vozilo id</param>
        /// /// <returns>
        /// True ako je obrisan, false inace
        /// </returns>
        public static bool deleteVozilo(int id)
        {
            if (!Validators.validID(id)) { return false; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("obrisi_vozilo", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@id", id);
                    return (a.ExecuteNonQuery() == 0) ? false : true;
                }
            }
        }

        /// <summary>
        /// Inserta vozilo
        /// </summary>
        /// <param name="marka">Marka vozila</param>
        /// <param name="tip_vozila_id">ID tip vozila</param>
        /// <param name="pocetni_km">Pocetni km vozila</param>
        /// <param name="trenutni_km">Trenutni km vozila</param>
        /// <param name="godina_proizvodnje">Godina proizvodnje vozila</param>
        /// /// <returns>
        /// ID insertanog vozila, 0 ako faila
        /// </returns>
        public static int insertVozilo(string marka, int? tip_vozila_id, decimal trenutni_km, decimal pocetni_km, int godina_proizvodnje)
        {
            if (
                !Validators.validGodina(godina_proizvodnje) ||
                !Validators.validKilometar(pocetni_km) ||
                !Validators.validKilometar(trenutni_km))
            { return 0; }

            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("insert_vozilo", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@marka", marka);
                    a.Parameters.AddWithValue("@tip_vozila_id", tip_vozila_id);
                    a.Parameters.AddWithValue("@godina_proizvodnje", godina_proizvodnje);
                    a.Parameters.AddWithValue("@pocetni_km", pocetni_km);
                    a.Parameters.AddWithValue("@trenutni_km", trenutni_km);
                    object result = a.ExecuteScalar();
                    result = (result == DBNull.Value) ? 0 : result;
                    int cc = Convert.ToInt32(result);
                    return cc;
                }
            }
        }

        /// <summary>
        /// Inserta servis
        /// </summary>
        /// <param name="naziv_servisa">Naziv servisa</param>
        /// <param name="datum">Datum obavljanja servisa</param>
        /// <param name="cijena">Cijena servisa</param>
        /// <param name="info">Dodatne informacije o serisu</param>
        /// /// <returns>
        /// ID insertanog servisa, 0 ako faila
        /// </returns>
        public static int insertServis(string naziv_servisa, DateTime datum, decimal cijena, string info, int vozilo_id)
        {
            if (!Validators.validGodina(datum.Year))
            { return 0; }

            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("insert_servis", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@naziv_servisa", naziv_servisa);
                    a.Parameters.AddWithValue("@vozilo_id", vozilo_id);
                    a.Parameters.AddWithValue("@datum", datum);
                    a.Parameters.AddWithValue("@info", info);
                    a.Parameters.AddWithValue("@cijena", cijena);
                    object result = a.ExecuteScalar();
                    result = (result == DBNull.Value) ? 0 : result;
                    int cc = Convert.ToInt32(result);
                    return cc;
                }
            }
        }


        /// <summary>
        /// Updatea servis
        /// </summary>
        /// <param name="id">Servis ID</param>
        /// <param name="naziv_servisa">Naziv servisa</param>
        /// <param name="datum">Datum obavljanja servisa</param>
        /// <param name="cijena">Cijena servisa</param>
        /// <param name="info">Dodatne informacije o serisu</param>
        /// /// <returns>
        /// true ako je update uspio, false inace
        /// </returns>
        public static bool updateServis(int id, string naziv_servisa, DateTime datum, decimal cijena, string info)
        {
            if (!Validators.validID(id) ||
                !Validators.validGodina(datum.Year))
            { return false; }

            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("update servis set " +
                    "naziv_servisa=@naziv_servisa,datum_servisa=@datum,info=@info,cijena=@cijena" +
                    " where id=@id", c))
                {
                    a.Parameters.AddWithValue("@naziv_servisa", naziv_servisa);
                    a.Parameters.AddWithValue("@id", id);
                    a.Parameters.AddWithValue("@datum", datum);
                    a.Parameters.AddWithValue("@info", info);
                    a.Parameters.AddWithValue("@cijena", cijena);
                    return (a.ExecuteNonQuery() == 0) ? false : true;
                }
            }
        }

        /// <summary>
        /// Brise servis
        /// </summary>
        /// <param name="id">Servis id</param>
        /// /// <returns>
        /// True ako je obrisan, false inace
        /// </returns>
        public static bool deleteServis(int? id)
        {
            if (!Validators.validID(id)) { return false; }
            using (SqlConnection c = new SqlConnection(CONNECTION_STRING))
            {
                c.Open();
                using (SqlCommand a = new SqlCommand("obrisi_servis", c))
                {
                    a.CommandType = CommandType.StoredProcedure;
                    a.Parameters.AddWithValue("@id", id);
                    return (a.ExecuteNonQuery() == 0) ? false : true;
                }
            }
        }

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
                    object result = a.ExecuteScalar();
                    result = (result == DBNull.Value) ? 0 : result;
                    int cc = Convert.ToInt32(result);
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
                using (SqlDataAdapter a = new SqlDataAdapter("select * from vozilo where id=@ID", c))
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
                using (SqlDataAdapter a = new SqlDataAdapter("select * from servis where vozilo_id=@ID", c))
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