using MyEshop.DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class Products
    {
        public Products()
        {

        }

        [Key]
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductCategoryParentId { get; set; }
        public int ProductCategoryMainId { get; set; }
        public int WebsiteCategoryId { get; set; }
        public int BrandId { get; set; }

        [Display(Name = "امتیاز محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Score { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EnglishTitle { get; set; }

        [Display(Name = "عکس محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }

        [Display(Name = "کلمات کلیدی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; }

        [Display(Name = "نقد و برسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Discription { get; set; }
        public int Discount { get; set; }
        public int Price { get; set; }
        public int DiscountedPrice { get; set; }
        public int? CountOfVisit { get; set; }
        public bool IsActive { get; set; }
        public bool IsAmazing { get; set; }
        public bool IsDelete { get; set; }
        public bool ReturnAble { get; set; }
        public DateTime CreateDate { get; set; }

        #region Relations

        [ForeignKey("BrandId")]
        public ProductBrands Brand { get; set; }

        public List<ProductSpecifications> ProductSpecifications { get; set; }

        public List<ProductColors> ProductColors { get; set; }

        public List<ImageGallery> ImageGallery { get; set; }

        public List<ProductProperties> ProductProperties { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public List<MyEshop.DataLayer.Entities.Boxes.Boxes> Boxes { get; set; }

        public List<SlideComments> SlideComments { get; set; }

        public List<StrengthsOrWeaknesses> StrengthsOrWeaknesses { get; set; }

        #endregion

    }
}
