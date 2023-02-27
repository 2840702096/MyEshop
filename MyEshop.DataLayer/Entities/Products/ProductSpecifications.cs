using MyEshop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class ProductSpecifications
    {
        public ProductSpecifications()
        {

        }

        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CSId { get; set; }

        [Display(Name = "مقدار مشخصه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Value { get; set; }

        #region Relations

        [ForeignKey("ProductId")]
        public Products Product { get; set; }

        [ForeignKey("CSId")]
        public SpecificationsInCategories TheSpecificationInCategory { get; set; }

        #endregion

    }
}
