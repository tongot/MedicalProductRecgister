using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductClassificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductClassifications
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductClassifications.ToListAsync());
        }

        // GET: ProductClassifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClassification productClassification = await db.ProductClassifications.FindAsync(id);
            if (productClassification == null)
            {
                return HttpNotFound();
            }
            return View(productClassification);
        }

        // GET: ProductClassifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductClassifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductClassificationId,Name")] ProductClassification productClassification)
        {
            if (ModelState.IsValid)
            {
                db.ProductClassifications.Add(productClassification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productClassification);
        }

        // GET: ProductClassifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClassification productClassification = await db.ProductClassifications.FindAsync(id);
            if (productClassification == null)
            {
                return HttpNotFound();
            }
            return View(productClassification);
        }

        // POST: ProductClassifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductClassificationId,Name")] ProductClassification productClassification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productClassification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productClassification);
        }

        // GET: ProductClassifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClassification productClassification = await db.ProductClassifications.FindAsync(id);
            if (productClassification == null)
            {
                return HttpNotFound();
            }
            return View(productClassification);
        }

        // POST: ProductClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductClassification productClassification = await db.ProductClassifications.FindAsync(id);
            db.ProductClassifications.Remove(productClassification);
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
