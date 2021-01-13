using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Ingredients
    {
        public int IngredientsId { get; set; }
        [MaxLength(200)]
        [Display(Name ="Common Name")]
        public string CommonName { get; set; }
        [MaxLength(200)]
        [Display(Name = "INCI Name")]
        public string INCIName { get; set; }

        public double Quantity { get; set; }
        public UnitMeasure Unit { get; set; }

        public bool Restricted { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public class InViewModel
    {
        public int IngredientsId { get; set; }
        public string CommonName { get; set; }
        public string INCIName { get; set; }

        public double Quantity { get; set; }
        public UnitMeasure Unit { get; set; }

        public bool Restricted { get; set; }

        public int ProductId { get; set; }
    }
    public enum UnitMeasure
    {
        Grams,
        Milligrams,
        Litre,
        Milliliters,
    }
}