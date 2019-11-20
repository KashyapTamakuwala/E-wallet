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

namespace E_Wallet.Controllers
{
    public class SchemesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Schemes
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Schemes.ToListAsync());
        }

        // GET: Schemes/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheme scheme = await db.Schemes.FindAsync(id);
            if (scheme == null)
            {
                return HttpNotFound();
            }
            return View(scheme);
        }

        // GET: Schemes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schemes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Minimum_Transaction,Refund")] Scheme scheme)
        {
            if (ModelState.IsValid)
            {
                db.Schemes.Add(scheme);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(scheme);
        }

        // GET: Schemes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheme scheme = await db.Schemes.FindAsync(id);
            if (scheme == null)
            {
                return HttpNotFound();
            }
            return View(scheme);
        }

        // POST: Schemes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Minimum_Transaction,Refund")] Scheme scheme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheme).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(scheme);
        }

        // GET: Schemes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheme scheme = await db.Schemes.FindAsync(id);
            if (scheme == null)
            {
                return HttpNotFound();
            }
            return View(scheme);
        }

        // POST: Schemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Scheme scheme = await db.Schemes.FindAsync(id);
            db.Schemes.Remove(scheme);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
