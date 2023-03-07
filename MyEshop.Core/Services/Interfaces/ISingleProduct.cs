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

        #region Comments

        int GetCountOfEvaluationTitleRowsInCommentPage(int id);
        List<SlideCommentTitles> GetSlideCommentTitles(int id, int countOfRowId);
        void AddSlideComments(List<int> slideCommentTitleId, List<int> slideCommentValue, int productId);
        void AddCommentAndPNPoints(string commentTitle, string commentBody, int offeringStatus, List<string> positivePoints, List<string> negativePoints, int productId);


        #endregion

    }
}
