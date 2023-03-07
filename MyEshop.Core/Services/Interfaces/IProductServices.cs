using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.DataLayer.Entities.Boxes;
using MyEshop.DataLayer.Entities.Categories;
using MyEshop.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IProductServices
    {
        #region Products

        IEnumerable<Products> GetProducts(int pageId);
        int GetCountOfProducts();
        Products GetProductById(int id);
        string GetBrandTitle(int id);
        string GetProductCategoryTitle(int id);
        string GetWebsiteCategoryTitle(int id);
        List<SelectListItem> GetBrands();
        List<SelectListItem> GetProductCategories();
        List<SelectListItem> GetWebsiteCategories();
        void AddProduct(AddProductViewModel product);
        EditProductViewModel GetProductInfoForEdit(int id);
        void EditProduct(int id, EditProductViewModel product, string currentImage);
        bool IsThisProductExistInAnyBox(int productId);
        void DeleteProduct(int id);
        IEnumerable<Products> FilterProductList(string productTitle);
        Products GetProduct(int id);
        bool IsThisProductActive(int productId);

        #region ProductSpecifications

        IEnumerable<ProductSpecifications> GetProductSpecifications(int productId);
        List<SelectListItem> GetCategorySpecifications(int categoryId);
        string GetCategorySpecificationTitle(int id);
        void AddProductSpecification(int productId, int categoryId, string value);
        void DeleteProductSpecification(int id);
        int GetProductCategoryId(int id);
        bool IsThereAnySpecificationWithThisCategory(int categoryId);
        bool IsThereAnySpecificationWithThisTitleInThisProduct(int specificationTitleId, int productId);

        #endregion

        #region PageOfBoxes

        NamesOfBoxes GetBox(int id);
        void AddToBox(int productId, int TitleId);
        void DeleteProductFromBox(int id, int TitleId);
        bool IsThisProductInThisBox(int productId, int titleId);

        #endregion

        #region ProductColors

        List<SelectListItem> GetColorsOfColorList();
        IEnumerable<ProductColors> GetProductColors(int productId);
        ColorList GetColorList(int colorId);
        void AddProductColor(int productId, ProductColorViewModel color);
        void DeleteProductColor(int id);
        bool IsThereAnyValueInThisColor(int id);
        bool IsThereAnyValueInThisColorForThisProduct(int id, int productId);

        #endregion

        #region ProductProperties

        IEnumerable<ProductProperties> GetProdoductProperties(int productId);
        void AddProductProperty(ProductPropertiesViewModel property, int productId);
        void DeleteProductProperty(int id);

        #endregion

        #region ImageGallery

        IEnumerable<ImageGallery> GetImagesForImageGallery(int id);
        void AddImageToImageGallery(IFormFile imageName, int productId);
        void DeleteImageFromImageGallery(int id);

        #endregion

        #endregion

        #region Brands

        IEnumerable<ProductBrands> GetBrands(int pageId);
        int GetCountOfProductBrands();
        void AddBrand(AddBrandViewModel brand);
        EditBrandViewModel GetBrandInfoForEdit(int id);
        void EditBrand(int id, EditBrandViewModel brand, string currentPhoto);
        void DeleteBrand(int id);
        bool IsThereAProductInThisBrand(int id);
        IEnumerable<ProductBrands> FilterBrands(string brandTitle, int pageId);
        int GetCountOfFilteredBrands(string brandTitle);

        #endregion

        #region ColorList

        IEnumerable<ColorList> GetColorsForColorList();
        int GetCountOfColorsInColorList();
        void AddColorToColorList(string colorTitle, string colorItSelf);
        void DeleteColorFromColorList(int id);

        #endregion

        #region Comments

        IEnumerable<Comments_Questions_Answer> GetAllHasntConfirmedComments(int pageId);
        int GetCountOfComments();
        List<StrengthsOrWeaknesses> GetPositivePoints(int commentId);
        List<StrengthsOrWeaknesses> GetNegativePoints(int commentId);

        #endregion

        #region Questions

        IEnumerable<Comments_Questions_Answer> GetHasntConfirmedQuestions(int pageId);
        int GetCountOfQuestions();

        #endregion

        #region Answers

        IEnumerable<Comments_Questions_Answer> GetHasntConfirmedAnswers(int pageId);
        int GetCountOfAnswers();

        #endregion

        #region Boxes

        IEnumerable<NamesOfBoxes> GetNamesOfBoxes();
        string GetBoxTitles(int id);
        void EditBoxTitle(int id, string title);

        #endregion

        Comments_Questions_Answer GetProductText(int id);
        void ConfirmOrRefuseTheProductText(int orderId, int commentId);
    }
}
