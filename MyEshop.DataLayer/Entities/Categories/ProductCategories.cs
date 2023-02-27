using MyEshop.DataLayer.Entities.DiscountCode;
using MyEshop.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEShop.DataLayer.Entities.Categories
{
    public class ProductCategories
    {

        public ProductCategories()
        {

        }

        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }
        public int? ParentId { get; set; }
        public int? SubParentId { get; set; }
        public bool IsDelete { get; set; }
        
        [Display(Name = "عکس")]
        public string ImageName { get; set; }

        [Display(Name = "رنگ")]
        public string Color { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public List<ProductCategories> Categories { get; set; }

        public List<DiscountCode> MyProperty { get; set; }

        public List<SlideCommentTitles> SlideCommentTitles { get; set; }

        #endregion

    }
}
