using MyEshop.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Boxes
{
    public class Boxes
    {
        public Boxes()
        {

        }

        [Key]
        public int Id { get; set; }
        public int Idd { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int WebsiteCategoryId { get; set; }
        public int BoxTitleId { get; set; }

        #region Relations

        [ForeignKey("ProductId")]
        public MyEshop.DataLayer.Entities.Products.Products Products { get; set; }

        [ForeignKey("BoxTitleId")]
        public NamesOfBoxes NamesOfBoxes { get; set; }

        #endregion

    }
}
