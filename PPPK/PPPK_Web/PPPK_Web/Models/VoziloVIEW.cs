using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK_Web.Models
{
    public class VoziloVIEW
    {
        public vozilo vozilo { get; set; }
        public List<servi> servisi { get; set; }
        public tip_vozila tip_vozila { get; set; }
    }
}