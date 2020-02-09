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
            return View();
        }


        /*
        [HttpGet]
        public ActionResult DodajVozaca()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajVozaca(vozac v)
        {
            if (ModelState.IsValid)
            {
                DatabaseHandler.insertVozac(v.ime, v.prezime, v.broj_mobitela, v.broj_vozacke);
                return RedirectToAction("Index");
            }
            return View();

        }
        */
    }
}