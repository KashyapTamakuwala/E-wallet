using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Wallet.Models;
using Microsoft.AspNet.Identity;

namespace E_Wallet.Controllers
{
    public class UserController : Controller
    {

        private Model1 db = new Model1();

        // GET: User
        //[Authorize(Roles= "Individual, Organization")]
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Pay([Bind(Include = "Email,Transaction_Amount,TO_Email,SID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(transaction);
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
