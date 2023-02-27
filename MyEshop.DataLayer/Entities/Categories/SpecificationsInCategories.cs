using MyEshop.DataLayer.Entities.Products;
using MyEShop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Categories
{
    public class SpecificationsInCategories
    {
        public SpecificationsInCategories()
        {

        }

        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "عنوان مشخصه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string SpecificationTitle { get; set; }

        #region Relations

        [ForeignKey("CategoryId")]
        public ProductCategories ProductCategory { get; set; }

        public List<ProductSpecifications> ProductSpecifications { get; set; }

        #endregion

    }
}
