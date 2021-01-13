using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Report
        public ActionResult reportView()
        {
            return View();
        }
        public ActionResult getProducts(string search)
        {
            try
            {
                var products = new List<productViewModel>();

                foreach (var item in db.Products.Where(x => x.Name.Contains(search) | search == null | x.BrandName.Contains(search) | search == null | x.ProductCode.Contains(search) | search == null))
                {
                    var product = new productViewModel();
                    //product.ingerdients = new List<InViewModel>();
                    product.dateOfEntry = item.createdOn.Value.ToShortDateString();
                    product.brand = item.BrandName;
                    product.distributor = item.Distributor.Name;
                    product.place = item.Region.Name;
                    product.classification = item.ProductClassification.Name;
                    product.indication = item.Indication;
                    product.packSize = item.PackSize;
                    product.manufacturer = item.Manufacturer.Name;
                    product.ingerdients = item.OtherIngredients;
                    product.RestrictedIngerdients = item.RestrictedIngridients;
                    product.expiry = item.Expiry.ToShortDateString();
                    foreach (var item2 in db.Ingredients.Where(x => x.ProductId == item.ProductId))
                    {
                        if (!item2.Restricted)
                        {
                            product.ingerdients += $",{item2.CommonName}";
                        }
                    }
                    products.Add(product);
                }

                return Json(products, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json("error "+e, JsonRequestBehavior.AllowGet);
            }

        }
        public class productViewModel
        {
            public string dateOfEntry { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string brand { get; set; }
            public string distributor { get; set; }
            public string place { get; set; }
            public string classification { get; set; }
            public string indication { get; set; }
            public string packSize { get; set; }
            public string manufacturer { get; set; }
            public string expiry { get; set; }
            public string ingerdients { get; set; }
            public string RestrictedIngerdients { get; set; }

            // public List<InViewModel> ingerdients { get; set; }
        }
    }
}