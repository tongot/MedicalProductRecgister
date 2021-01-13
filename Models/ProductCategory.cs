using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId{ get; set; }

        [Display(Name="Category")]
        [MaxLength(200)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}