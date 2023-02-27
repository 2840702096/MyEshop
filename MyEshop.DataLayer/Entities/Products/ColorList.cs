using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class ColorList
    {
        public ColorList()
        {

        }

        [Key]
        public int ColorId { get; set; }

        [Display(Name = "عنوان رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ColorTitle { get; set; }

        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ColorItSelf { get; set; }
        public bool IsDelete { get; set; }

        #region Relations

        public List<ProductColors> ProductColors { get; set; }

        #endregion

    }
}
