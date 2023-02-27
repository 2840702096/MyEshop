using MyEshop.DataLayer.Entities.Categories;
using MyEshop.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface ISingleProduct
    {
        Products GetProduct(int id);
        List<ImageGallery> GetImagesOfThisProduct(int productId);
        List<ProductColors> GetColorsOfThisProduct(int productId);
        List<ProductProperties> GetPropertiesOfThisProduct(int productId);
        List<ProductSpecifications> GetProductSpecifications(int productId);
        List<Products> GetRelatedProductsToThisSingleProduct(int categoryId, int productId);
        SpecificationsInCategories GetProductSpecificationForGettingValues(int csId);
        ColorList GetColorItSelf(int colorId);
        int GetColorPrice(int colorId);
        int GetPriceOfLastAddedColor (int id);
        
    }
}
