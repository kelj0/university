using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPPK_Web.Models
{
    public class DodajNalogVM
    {
        public DateTime datum { get; set; }
        public DateTime datum_pocetka { get; set; }
        public DateTime datum_zavrsetka { get; set; }
    }
}