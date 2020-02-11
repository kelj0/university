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
            List<VozilaVM> vvl = new List<VozilaVM>();
            foreach(vozilo v in DatabaseHandler.getAllVozila())
            {
                vvl.Add(new VozilaVM
                {
                    tip_vozila = DatabaseHandler.getTipVozila((int)v.tip_vozila_id),
                    vozilo = v
                });
            }

            return View(vvl);
        }

        public ActionResult Vozilo(int? id)
        {
            if (Validators.validID(id))
            {
                VoziloVM vsm = new VoziloVM
                {
                    servisi = DatabaseHandler.getServisi(Convert.ToInt16(id)),
                    vozilo = DatabaseHandler.getVozilo(Convert.ToInt16(id)),
                    tip_vozila = DatabaseHandler.getTipVozila(Convert.ToInt16(id))
                };
                ViewBag.tipovi_vozila = Other.getTipoviVozilaList();
                return View((object)vsm);
            }
            else
            {
                return View((object)null);
            }
        }

        [HttpGet]
        public ActionResult DodajVozilo()
        {
            ViewBag.tipovi_vozila = Other.getTipoviVozilaList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajVozilo(vozilo v, string tipovi_vozila)
        {
            if (ModelState.IsValid)
            {
                DatabaseHandler.insertVozilo(v.marka, Convert.ToInt16(tipovi_vozila), v.pocetni_km, v.pocetni_km, v.godina_proizvodnje);
                return RedirectToAction("Index");
            }

            ViewBag.tipovi_vozila = Other.getTipoviVozilaList();
            return View();

        }

    }
}