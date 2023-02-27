using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Products
{
    public class ProductProperties
    {
        public ProductProperties()
        {

        }

        [Key]
        public int PropertyId { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string Value { get; set; }

        #region Relations

        [ForeignKey("ProductId")]
        public Products Product { get; set; }

        #endregion

    }
}
