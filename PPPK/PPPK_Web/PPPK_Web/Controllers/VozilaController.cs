using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                return View((object)id);
            }
            else
            {
                return View((object)0);
            }
        }
    }
}