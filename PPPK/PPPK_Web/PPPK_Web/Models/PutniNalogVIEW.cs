using PPPK_Web.HELPERS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace PPPK_Web.Models
{

    public class VozacVozilo<T1,T2>: Pair<T1, T2>
    {
        public T1 Vozac { get; set; }
        public T2 Vozilo { get; set; }
    }

    public class PutniNalogVIEW
    {
        public VozacVozilo<vozac,vozilo> vozac_vozilo { get; set; }
        public string status { get; set; }
        public DateTime datum { get; set; }
    }
}