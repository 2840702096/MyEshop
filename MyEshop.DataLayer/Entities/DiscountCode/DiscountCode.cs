using MyEShop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.DiscountCode
{
    public class DiscountCode
    {
        public DiscountCode()
        {

        }

        [Key]
        public int DiscountId { get; set; }
        public int? UserId { get; set; }
        public int? ProductCategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Code { get; set; }

        [Required]
        public int DiscountPercent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        #region Relations

        [ForeignKey("ProductCategoryId")]
        public ProductCategories ProductCategories { get; set; }

        [ForeignKey("UserId")]
        public Users.Users User { get; set; }

        #endregion

    }
}
