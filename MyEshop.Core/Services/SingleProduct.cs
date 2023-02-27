using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Categories;
using MyEshop.DataLayer.Entities.Products;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class SingleProduct : ISingleProduct
    {
        private ShopContext _context;

        public SingleProduct(ShopContext context)
        {
            _context = context;
        }

        public ColorList GetColorItSelf(int colorId)
        {
            return _context.ColorList.Find(colorId);
        }

        public int GetColorPrice(int colorId)
        {
            ProductColors Color = _context.ProductColors.SingleOrDefault(p => p.ColorId == colorId);
            if (Color.Discount == 0)
                return Color.Price;

            else
                return Color.DiscountedPrice;
        }

        public List<ProductColors> GetColorsOfThisProduct(int productId)
        {
            return _context.ProductColors.Where(p => p.ProductId == productId).ToList();
        }

        public List<ImageGallery> GetImagesOfThisProduct(int productId)
        {
            return _context.ImageGallery.Where(i => i.ProductId == productId).ToList();
        }

        public int GetPriceOfLastAddedColor(int id)
        {
            ProductColors color = _context.ProductColors.Where(p => p.ProductId == id).OrderByDescending(p => p.ProductColorId).First();

            if (color.Discount == 0)
                return color.Price;

            else
                return color.DiscountedPrice;
        }

        public Products GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public SpecificationsInCategories GetProductSpecificationForGettingValues(int csId)
        {
            return _context.SpecificationsInCategories.Find(csId);
        }

        public List<ProductSpecifications> GetProductSpecifications(int productId)
        {
            return _context.ProductSpecifications.Where(p => p.ProductId == productId).ToList();
        }

        public List<ProductProperties> GetPropertiesOfThisProduct(int productId)
        {
            return _context.ProductProperties.Where(p => p.ProductId == productId).ToList();
        }

        public List<Products> GetRelatedProductsToThisSingleProduct(int categoryId, int productId)
        {
            return _context.Products.Where(p => p.ProductCategoryMainId == categoryId && p.ProductId != productId).ToList();
        }
    }
}
