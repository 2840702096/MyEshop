using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.DTOs
{
    #region Search

    public class ProductsInSearchViewModel
    {
        public int ProductId { get; set; }
        public int ProductCategoryMainId { get; set; }
        public int ProductCategoryParentId { get; set; }
        public int ProductCategoryId { get; set; }
        public int WebsiteCategoryId { get; set; }
        public int BrandId { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
        public int Discount { get; set; }
        public int Price { get; set; }
        public int DiscountedPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool ReturAble { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsAmazing { get; set; }
        public int CountOfVisit { get; set; }

    }

    #endregion
}
