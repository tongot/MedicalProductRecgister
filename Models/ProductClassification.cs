using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class ProductClassification
    {
        public int ProductClassificationId { get; set; }
        [MaxLength(200)]
        [Display(Name ="Class")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}