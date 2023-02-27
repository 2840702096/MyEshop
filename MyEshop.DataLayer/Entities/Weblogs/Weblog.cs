using MyEshop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Weblogs
{
    public class Weblog
    {
        public Weblog()
        {

        }

        [Key]
        public int WeblogId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "عنوان وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string WeblogTitle { get; set; }
        public int Score { get; set; }
        public DateTime CreateDate { get; set; }
        public int Visit { get; set; }

        [Display(Name = "عکس وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }

        [Display(Name = "متن وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string WeblogBody { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        [ForeignKey("CategoryId")]
        public WeblogCategories WeblogCategory { get; set; }

        [ForeignKey("UserId")]
        public MyEshop.DataLayer.Entities.Users.Users User { get; set; }

        #endregion

    }
}
