using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK_Web.Models
{
    public class Pair<vozilo, tip_vozila>
    {
        public vozilo Vozilo { get; set; }
        public tip_vozila Tip_vozila { get; set; }
    }

    public class VozilaVIEW
    {
        public List<Pair<vozilo,tip_vozila>> vozila_tipovi { get; set; }
    }
}