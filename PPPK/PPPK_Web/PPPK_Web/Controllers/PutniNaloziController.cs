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
            return View();
        }

        public ActionResult PutniNalog(int? id)
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