using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class StrengthsOrWeaknesses
    {
        public StrengthsOrWeaknesses()
        {

        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public bool IsPositiveOrNegative { get; set; }
        public int SWAndCommentsRelation { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsRefused { get; set; }

        #region Relations

        [ForeignKey("ProductId")]
        public Products Products { get; set; }

        [ForeignKey("UserId")]
        public Users.Users Users { get; set; }

        #endregion
    }
}
