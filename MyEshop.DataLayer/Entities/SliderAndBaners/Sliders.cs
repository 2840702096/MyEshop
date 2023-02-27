using MyEShop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.SliderAndBaners
{
    public class Sliders
    {
        public Sliders()
        {

        }

        [Key]
        public int Id { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? WebsiteCategoryId { get; set; }

        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "لینک اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Link { get; set; }

        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tags { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        [Display(Name = "عکس ها")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public string ImageName { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
