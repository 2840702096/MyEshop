using MyEShop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class SlideCommentTitles
    {
        public SlideCommentTitles()
        {

        }

        [Key]
        public int Id { get; set; }
        public int ProductMainCategoryId { get; set; }

        [Display(Name = "عنوان ارزیابی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string SlideCommentTitle { get; set; }

        #region Relations

        [ForeignKey("ProductMainCategoryId")]
        public ProductCategories ProductCategory { get; set; }

        public List<SlideComments> SlideComments { get; set; }

        #endregion
    }
}
