using PPPK_Web.HELPERS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPPK_Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BackupDb()
        {
            DatabaseHandler.BackupDb();
            return RedirectToAction("Index");
        }

        public ActionResult NukeDb()
        {
            DatabaseHandler.NukeDb();
            return RedirectToAction("Index");
        }

        public ActionResult RestoreDb()
        {
            DatabaseHandler.RestoreDb();
            return RedirectToAction("Index");
        }

    }
}