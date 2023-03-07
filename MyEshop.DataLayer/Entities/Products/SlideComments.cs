using MyEshop.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class SlideComments
    {
        public SlideComments()
        {

        }

        [Key]
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int SCTitleId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int SCValue { get; set; }

        #region Relations

        [ForeignKey("SCTitleId")]
        public SlideCommentTitles SlideCommentTitles { get; set; }

        [ForeignKey("ProductId")]
        public Products Products { get; set; }

        [ForeignKey("UserId")]
        public Users.Users Users { get; set; }

        [ForeignKey("CommentId")]
        public Comments_Questions_Answer Comments{ get; set; }

        #endregion
    }
}
