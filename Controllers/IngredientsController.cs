using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    [Authorize()]
    public class IngredientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ingredients
        public ActionResult Index(int? productId)
        {
            if(productId==null)
            {
                return null;
            }
            List<InViewModel> items = new List<InViewModel>();
            foreach (var item in db.Ingredients.Where(x => x.ProductId == productId).ToList())
            {
                InViewModel md = new InViewModel();
                md.ProductId = item.ProductId;
                md.IngredientsId = item.IngredientsId;
                md.Quantity = item.Quantity;
                md.Restricted = item.Restricted;
                md.CommonName = item.CommonName;
                md.INCIName = item.INCIName;

                items.Add(md);
            };
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = db.Ingredients.Find(id);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            return View(ingredients);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductCode");
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                db.Ingredients.Add(ingredients);
                db.SaveChanges();
                return Json(ingredients, JsonRequestBehavior.AllowGet);
            }
            return Json(ingredients, JsonRequestBehavior.AllowGet);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = db.Ingredients.Find(id);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductCode", ingredients.ProductId);
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit( Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredients).State = EntityState.Modified;
                db.SaveChanges();
                return Json(ingredients, JsonRequestBehavior.AllowGet);
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductCode", ingredients.ProductId);
            return Json(ingredients, JsonRequestBehavior.AllowGet);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = db.Ingredients.Find(id);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredients ingredients = db.Ingredients.Find(id);
            db.Ingredients.Remove(ingredients);
            db.SaveChanges();
            return Json("deleted",JsonRequestBehavior.AllowGet);
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
