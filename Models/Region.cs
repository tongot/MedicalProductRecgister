using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        [MaxLength(200)]
        [Display(Name="Place of entry")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}