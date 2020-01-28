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
            VozilaVIEW vv = new VozilaVIEW { vozila_tipovi = new List<Pair<vozilo, tip_vozila>>() };
            foreach(vozilo v in DatabaseHandler.getAllVozila())
            {
                vv.vozila_tipovi.Add(new Pair<vozilo, tip_vozila>
                {
                    Vozilo = v,
                    Tip_vozila = DatabaseHandler.getTipVozila((int)v.tip_vozila_id)
                });
            }

            return View(vv);
        }

        public ActionResult Vozilo(int? id)
        {
            if (Validators.validID(id))
            {
                VoziloVIEW vsm = new VoziloVIEW
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