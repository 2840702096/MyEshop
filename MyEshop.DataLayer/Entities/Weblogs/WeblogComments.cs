using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Weblogs
{
    public class WeblogComments
    {
        public WeblogComments()
        {

        }

        [Key]
        public int CommentId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "عنوان نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "متن نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CommentBody { get; set; }
        public DateTime CreateTime { get; set; }
        public int? Yes { get; set; }
        public int? No { get; set; }
        public bool IsConfirmed { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public MyEshop.DataLayer.Entities.Users.Users User { get; set; }

        #endregion

    }
}
