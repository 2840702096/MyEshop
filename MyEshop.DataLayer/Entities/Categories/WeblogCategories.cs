using MyEshop.DataLayer.Entities.Weblogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Categories
{
    public class WeblogCategories
    {
        public WeblogCategories()
        {

        }

        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        public bool IsDelete { get; set; }
        public int? ParentId { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public List<WeblogCategories> Categories { get; set; }
        public List<Weblog> Weblogs { get; set; }

        #endregion

    }
}
