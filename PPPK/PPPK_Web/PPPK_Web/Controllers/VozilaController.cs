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
                    Tip_vozila = DatabaseHandler.getTipVozila(v.id)
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
                DatabaseHandler.insertVozilo(v.marka, v.tip_vozila_id, v.trenutni_km, v.pocetni_km, v.godina_proizvodnje);
                RedirectToAction("Index");
            }

            ViewBag.tipovi_vozila = Other.getTipoviVozilaList();
            return View();

        }

    }
}