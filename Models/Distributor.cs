using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Distributor
    {
        public int DistributorId { get; set; }
        [MaxLength(200)]
        [Display(Name="Distributor")]
        [Index(IsUnique =true)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Contact { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}