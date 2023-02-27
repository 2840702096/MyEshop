using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class ProductBrands
    {
        public ProductBrands()
        {

        }

        [Key]
        public int BrandId { get; set; }

        [Display(Name = "عنوان برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        public string ImageName { get; set; }
        public bool IsDelete { get; set; }

        #region Relations

        public List<Products> Products { get; set; }

        #endregion

    }
}
