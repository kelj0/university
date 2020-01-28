using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PPPK_Web.HELPERS
{
    public static class Validator
    {
        /// <summary>
        /// Provjerava valjanost ID-a 
        /// </summary>
        /// <param name="ID">ID</param>
        /// /// <returns>
        /// true ako je valjani id, inace false
        /// </returns>
        public static bool validID(int? ID)
        {
            if (ID == null || !Regex.IsMatch(ID.ToString(), @"^[1-9]([0-9]?)+$")){
                return false;
            }
            else{
                return true;
            }
        }

        /// <summary>
        /// Provjerava valjanost broja vozacke(vozacke od 7 do 16 brojeva)
        /// </summary>
        /// <param name="broj_vozacke">Broj vozacke</param>
        /// /// <returns>
        /// true ako je valjani broj vozacke, inace false
        /// </returns>
        public static bool validBrojVozacke(string broj_vozacke)
        {
            if (!Regex.IsMatch(broj_vozacke,@"^\d{7,16}$")){
                return false;
            }else{
                return true;
            }
        }

        /// <summary>
        /// Provjerava valjanost broja mobitela(samo hrvatski brojevi)
        /// </summary>
        /// <param name="broj_mobitela">Broj mobitela</param>
        /// /// <returns>
        /// true ako je valjani broj mobitela, inace false
        /// </returns>
        public static bool validBrojMobitela(string broj_mobitela)
        {
            if (!Regex.IsMatch(broj_mobitela, @"^(\+385)?\d{9}$")){ //for croatian numbers
                return false;
            }else{
                return true;
            }
        }
    }
}