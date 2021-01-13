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
using PagedList;

namespace Pharmacy.Controllers
{
    [Authorize(Roles ="Admin,Agent")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public async Task<ActionResult> Index(string search,int? page)
        {
            var products = db.Products.Include(p => p.Distributor)
                .Where(x=>x.Name.Contains(search)| search==null | x.BrandName.Contains(search)|search== null | x.ProductCode.Contains(search) | search == null)
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductClassification)
                .Include(p => p.Region).OrderByDescending(x => x.createdOn)
                .ToList().ToPagedList(page ?? 1, 10);
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.distributorName = new SelectList(db.Distributors, "Name", "Name");
            ViewBag.manufactureName = new SelectList(db.Manufacturers, "Name", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name");
            ViewBag.ProductClassificationId = new SelectList(db.ProductClassifications, "ProductClassificationId", "Name");
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Product product)
        {
            var date = DateTime.Now;

            product.ProductCode = $"P{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Millisecond}";
            product.addedBy = User.Identity.Name;
            product.createdOn = DateTime.Now;
            int Did = DistributorId(product.distributorName);
            if ( Did> 0)
            {
                product.DistributorId = Did;
            }
            else
            {
                ViewData["error"] = "U have entered an invalid Distributor";
                ViewBag.distributorName = new SelectList(db.Distributors, "Name", "Name");
                ViewBag.manufactureName = new SelectList(db.Manufacturers, "Name", "Name");
                ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
                ViewBag.ProductClassificationId = new SelectList(db.ProductClassifications, "ProductClassificationId", "Name", product.ProductClassificationId);
                ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", product.RegionId);
                return View(product);
            }
            int Mid = ManufacturerId(product.manufactureName);
            if (Mid > 0)
            {
                product.ManufacturerId = Mid;
            }
            else
            {
                ViewData["error"] = "U have entered an invalid Manufacturer";
                ViewBag.distributorName = new SelectList(db.Distributors, "Name", "Name");
                ViewBag.manufactureName = new SelectList(db.Manufacturers, "Name", "Name");
                ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
                ViewBag.ProductClassificationId = new SelectList(db.ProductClassifications, "ProductClassificationId", "Name", product.ProductClassificationId);
                ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", product.RegionId);
                return View(product);
            }

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("details",new { id = product.ProductId});
            }

            ViewBag.distributorName = new SelectList(db.Distributors, "Name", "Name");
            ViewBag.manufactureName = new SelectList(db.Manufacturers, "Name", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewBag.ProductClassificationId = new SelectList(db.ProductClassifications, "ProductClassificationId", "Name", product.ProductClassificationId);
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", product.RegionId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewBag.ProductClassificationId = new SelectList(db.ProductClassifications, "ProductClassificationId", "Name", product.ProductClassificationId);
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", product.RegionId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewBag.ProductClassificationId = new SelectList(db.ProductClassifications, "ProductClassificationId", "Name", product.ProductClassificationId);
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", product.RegionId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        int DistributorId(string name)
        {
            var distributor = db.Distributors.Where(x => x.Name == name).FirstOrDefault();
            if (distributor != null)
                return distributor.DistributorId;
            else return 0;
        }
        int ManufacturerId(string name)
        {
            var manufacturer = db.Manufacturers.Where(x => x.Name == name).FirstOrDefault();
            if (manufacturer != null)
                return manufacturer.ManufacturerId;
            else return 0;
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
