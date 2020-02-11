using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPPK_Web.HELPERS
{
    public static class Other
    {
        public static List<SelectListItem> getTipoviVozilaList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<tip_vozila> tv = DatabaseHandler.getAllTipVozila();
            foreach (tip_vozila t in tv)
            {
                items.Add(new SelectListItem
                {
                    Text = t.tip,
                    Value = t.id.ToString()
                });
            }
            return items;
        }

        public static List<SelectListItem> getVozilaList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<vozilo> tv = DatabaseHandler.getAllVozila();
            foreach (vozilo v in tv)
            {
                items.Add(new SelectListItem
                {
                    Text = string.Format("{0}({1})",v.marka,v.godina_proizvodnje),
                    Value = v.id.ToString()
                });
            }
            return items;


        }

        public static List<SelectListItem> getVozaciList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<vozac> tv = DatabaseHandler.getAllVozaci();
            foreach (vozac v in tv)
            {
                items.Add(new SelectListItem
                {
                    Text = (v.ime + " " + v.prezime),
                    Value = v.id.ToString()
                });
            }
            return items;
        }
    }
}