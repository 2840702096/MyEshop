using MyEshop.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Orders
{
    public class OrderDetails
    {
        public OrderDetails()
        {

        }

        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int MainPrice { get; set; }

        #region Relations

        [ForeignKey("OrderId")]
        public Orders Orders { get; set; }

        [ForeignKey("ProductId")]
        public MyEshop.DataLayer.Entities.Products.Products Products { get; set; }

        [ForeignKey("ColorId")]
        public ProductColors ProductColors { get; set; }

        #endregion

    }
}
