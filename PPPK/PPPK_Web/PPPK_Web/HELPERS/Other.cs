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

    }
}