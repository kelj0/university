using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PPPK_Web.HELPERS
{
    public static class Validators
    {
        /// <summary>
        /// Provjerava valjanost ID-a 
        /// </summary>
        /// <param name="ID">ID</param>
        /// /// <returns>
        /// true ako je valjani id, inace false
        /// </returns>
        public static bool validID(int? ID)
            => (ID == null || !Regex.IsMatch(ID.ToString(), @"^[1-9]([0-9]?)+$")) ? false : true;

        /// <summary>
        /// Provjerava valjanost broja vozacke(vozacke od 7 do 16 brojeva)
        /// </summary>
        /// <param name="broj_vozacke">Broj vozacke</param>
        /// /// <returns>
        /// true ako je valjani broj vozacke, inace false
        /// </returns>
        public static bool validBrojVozacke(string broj_vozacke)
            => (!Regex.IsMatch(broj_vozacke, @"^\d{7,16}$")) ? false : true;

        /// <summary>
        /// Provjerava valjanost broja mobitela(samo hrvatski brojevi)
        /// </summary>
        /// <param name="broj_mobitela">Broj mobitela</param>
        /// /// <returns>
        /// true ako je valjani broj mobitela, inace false
        /// </returns>
        public static bool validBrojMobitela(string broj_mobitela) //for croatian numbers
            => (!Regex.IsMatch(broj_mobitela, @"(^(\+?385)\d{9}$)|(^(09)\d{8}$)")) ? false : true;

        /// <summary>
        /// Provjerava valjanost godine proizvodnje vozila
        /// </summary>
        /// <param name="godina_proizvodnje">Godina proizvodnje</param>
        /// /// <returns>
        /// true ako je valjana godinja proizvodnje, inace false
        /// </returns>
        public static bool validGodinaProizvodnje(int? godina_proizvodnje)
            => (godina_proizvodnje != null && (godina_proizvodnje > 1886 && (godina_proizvodnje <= DateTime.Now.Year))) ? true : false;

        /// <summary>
        /// Provjerava valjanost kilometara
        /// </summary>
        /// <param name="km">Kilometri</param>
        /// /// <returns>
        /// true ako su kilometri valjani, inace false
        /// </returns>
        public static bool validKilometar(decimal? km)
            => km != null && (km > 0 && (km <= 3000000)) ? true : false; 

    }
}