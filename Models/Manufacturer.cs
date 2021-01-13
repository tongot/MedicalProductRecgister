using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        [MaxLength(200)]
        [Display(Name ="Manufacturer Name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Contact { get; set; }
        [MaxLength(500)]
        public string Addresss { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}