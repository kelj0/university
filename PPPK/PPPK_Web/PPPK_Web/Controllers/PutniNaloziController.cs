using PPPK_Web.HELPERS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPPK_Web.Controllers
{
    public class PutniNaloziController : Controller
    {
        // GET: PutniNalozi
        public ActionResult Index()
        {
            return View((object)DatabaseHandler.getAllPutniNalozi());
        }

        public ActionResult PutniNalog(int? id)
        {
            if (Validators.validID(id))
            {
                return View((object)DatabaseHandler.getPutniNalog((int)id));
            }
            else
            {
                return View((object)null);
            }
        }

        [HttpGet]
        public ActionResult DodajNalog()
        {
            ViewBag.vozila = Other.getVozilaList();
            ViewBag.vozaci = Other.getVozaciList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajNalog(DateTime datum_pocetka, DateTime datum_zavrsetka, string vozaci, string vozila)
        {
            DatabaseHandler.insertPutniNalog(datum_pocetka,datum_zavrsetka,Convert.ToInt16(vozaci), Convert.ToInt16(vozila));
            return RedirectToAction("Index");
        }
    }
}