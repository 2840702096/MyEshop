using MyEshop.DataLayer.Entities.Products;
using MyEshop.DataLayer.Entities.Weblogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Users
{
    public class Users
    {
        public Users()
        {

        }

        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Email { get; set; }

        [Display(Name = "کلمه ی عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "عکس پروفایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserAvatar { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoeNumber { get; set; }
        public string ActiveCode { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Score { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        #region Relations

        public List<MyEshop.DataLayer.Entities.Orders.Orders> Orders { get; set; }
        public List<Comments_Questions_Answer> Comments_Questions_Answers { get; set; }
        public List<Weblog> Weblogs { get; set; }
        public List<WeblogComments> WeblogComments { get; set; }
        public List<DiscountCode.DiscountCode> DiscountCodes { get; set; }
        public List<SlideComments> SlideComments { get; set; }
        public List<StrengthsOrWeaknesses> StrengthsOrWeaknesses { get; set; }


        #endregion

    }
}
