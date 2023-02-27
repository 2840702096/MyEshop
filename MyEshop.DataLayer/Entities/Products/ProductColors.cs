using MyEshop.DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class ProductColors
    {
        public ProductColors()
        {

        }

        [Key]
        public int ProductColorId { get; set; }
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int DiscountedPrice { get; set; }
        public int? Discount { get; set; }
        public int Quantity { get; set; }
        public bool IsDelete { get; set; }

        #region Relations

        [ForeignKey("ColorId")]
        public ColorList ColorList { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        #endregion

    }
}
