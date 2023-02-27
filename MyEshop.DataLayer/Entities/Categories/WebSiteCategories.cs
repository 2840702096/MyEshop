using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEShop.DataLayer.Entities.Categories
{
    public class WebSiteCategories
    {
        public WebSiteCategories()
        {

        }

        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }

        [Display(Name = "رنگ")]
        public string Color { get; set; }
        public bool IsDelete { get; set; }
    }
}
