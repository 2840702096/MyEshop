using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class ImageGallery
    {
        public ImageGallery()
        {

        }

        [Key]
        public int ImageId { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }

        #region Relations

        [ForeignKey("ProductId")]
        public Products Product { get; set; }

        #endregion

    }
}
