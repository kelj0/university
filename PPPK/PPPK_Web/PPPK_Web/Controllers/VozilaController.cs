using PPPK_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPPK_Web.HELPERS;

namespace PPPK_Web.Controllers
{
    public class VozilaController : Controller
    {
        // GET: Vozila
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vozilo(int? id)
        {
            if (id != null)
            {
                VoziloVIEW vsm = new VoziloVIEW
                {
                    servisi = DatabaseHandler.getServisi(Convert.ToInt16(id)),
                    vozilo = DatabaseHandler.getVozilo(Convert.ToInt16(id)),
                    tip_vozila = DatabaseHandler.getTipVozila(Convert.ToInt16(id))
                };

                return View((object)vsm);
            }
            else
            {
                return View((object)null);
            }
        }
    }
}