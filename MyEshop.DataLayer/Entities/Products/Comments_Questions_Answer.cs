using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class Comments_Questions_Answer
    {
        public Comments_Questions_Answer()
        {

        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IsItCommentOrQuestionOrAnswer { get; set; }

        [Display(Name = "عنوان نظر")]
        public string Title { get; set; }

        [Display(Name = "متن نظر")]
        public string CommentBody { get; set; }

        [Display(Name = "متن سوال")]
        public string QuestionBody { get; set; }

        [Display(Name = "متن جواب")]
        public string AnswerBody { get; set; }
        public string ProsAndCons { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsRefused { get; set; }
        public int? SWAndCommentsRelation { get; set; }


        #region Relations

        [ForeignKey("UserId")]
        public MyEshop.DataLayer.Entities.Users.Users User { get; set; }

        #endregion

    }
}
