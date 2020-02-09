using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace PPPK_Web.Models
{
    public class PutniNalogVIEW
    {
        public putni_nalog putni_nalog { get; set; }
        public vozac vozac { get; set; }
        public vozilo vozilo { get; set; }
        public string status { get; set; }
    }
}