using PPPK_Web.HELPERS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK_Web.Models
{
    public class VozilaPair<T1, T2> : Pair<T1, T1>
    {
        public T1 Vozilo { get; set; }
        public T2 Tip_vozila { get; set; }
    }

    public class VozilaVIEW
    {
        public List<VozilaPair<vozilo,tip_vozila>> vozila_tipovi { get; set; }
    }
}