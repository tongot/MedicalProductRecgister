using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class DisposalMethod
    {
        public int DisposalMethodId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
    }
}