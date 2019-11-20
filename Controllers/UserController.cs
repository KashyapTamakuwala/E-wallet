using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Wallet.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize(Roles= "Individual, Organization")]
        public ActionResult Index()
        {
            return View();
        }

        

        // GET: User/Pay
        public ActionResult Pay()
        {
            return View();
        }

        // POST: User/Pay
        [HttpPost]
        public ActionResult Pay(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Load
        public ActionResult Load()
        {
            return View();
        }

        // POST: User/Load
        [HttpPost]
        public ActionResult Load(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Link
        public ActionResult Link()
        {
            return View();
        }

        // POST: User/Link
        [HttpPost]
        public ActionResult Link(FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
