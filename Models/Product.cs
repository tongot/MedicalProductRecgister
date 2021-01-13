using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name ="Code")]
        [MaxLength(20)]
        public string ProductCode { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "Product/Brand Name")]
        [Required]
        public string BrandName { get; set; }

        [MaxLength(200)]
        [Display(Name = "Indication")]
        [Required]
        public string Indication { get; set; }

        [MaxLength(100)]
        [Display(Name = "Pack Size")]
        public string PackSize { get; set; }

        [Display(Name = "Expiry Date")]
        [Required]
        public DateTime Expiry { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Storage Condition")]
        public string StorageCondition { get; set; }

        [Display(Name = "Ingredients")]
        public string OtherIngredients { get; set; }

        [Display(Name = "Restricted Ingredient")]
        public RestrictedIngr HasRestrictedIngredient { get; set; }

        [MaxLength(5000)]
        [Display(Name= "Restricted Ingredients")]
        public string RestrictedIngridients { get; set; }

        [Display(Name="Category")]
        public int ProductCategoryId { get; set; }
        [Display(Name = "Place of Entry")]
        public int RegionId { get; set; }
        [Display(Name = "Product Classification")]
        public int ProductClassificationId { get; set; }

        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }

        [Display(Name = "Distributor")]
        public int DistributorId { get; set; }

        public string addedBy { get; set; }
        public DateTime? createdOn { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Region Region { get; set; }
        public virtual ProductClassification ProductClassification { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Distributor Distributor { get; set; }
        public ICollection<Ingredients> ingredients { get; set; }


        public string[] listIngredient {
            get { return RestrictedIngridients.Split(','); }
                }

        public string[] OtherIngredientList
        {
            get { return OtherIngredients.Split(','); }
        }
        [NotMapped]
        public string distributorName { get; set; }

        [NotMapped]
        public string manufactureName { get; set; }

    }
    public enum RestrictedIngr
    {   NO,
        YES
     
    }
}