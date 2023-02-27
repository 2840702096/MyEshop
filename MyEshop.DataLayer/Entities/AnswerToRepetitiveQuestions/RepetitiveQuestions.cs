using MyEshop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.AnswerToRepetitiveQuestions
{
    public class RepetitiveQuestions
    {
        public RepetitiveQuestions()
        {

        }

        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "متن سوال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string QuestionText { get; set; }

        [Display(Name = "متن جواب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AnswerBody { get; set; }
        public bool IsRepetitive { get; set; }

        #region Relations

        [ForeignKey("CategoryId")]
        public RepetitiveQuestionCategories RepetitiveQuestionCategory{ get; set; }

        #endregion
    }
}
