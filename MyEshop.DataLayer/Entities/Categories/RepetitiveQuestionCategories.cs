using MyEshop.DataLayer.Entities.AnswerToRepetitiveQuestions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Categories
{
    public class RepetitiveQuestionCategories
    {
        public RepetitiveQuestionCategories()
        {

        }

        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [Display(Name = "عکس گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }
        public bool IsDelete { get; set; }

        #region Relations

        public List<RepetitiveQuestions> RepetitiveQuestions { get; set; }

        #endregion
    }
}
