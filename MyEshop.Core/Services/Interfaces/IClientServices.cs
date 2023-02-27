using MyEshop.Core.DTOs;
using MyEshop.DataLayer.Entities.Boxes;
using MyEshop.DataLayer.Entities.Products;
using MyEshop.DataLayer.Entities.SliderAndBaners;
using MyEShop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IClientServices
    {
        #region Categories

        IEnumerable<ProductCategories> GetCategories();
        IEnumerable<ProductCategories> GetMainCategories();
        int GetCountOfProductsInThisMainCategory(int categoryId);
        IEnumerable<WebSiteCategories> GetWebsiteCategories();
        int GetCountOfSubProductCategories(int categoryId);
        IEnumerable<ProductCategories> GetParentCategories(int categoryId, int countId);

        #endregion

        #region SideBarOfSearchPage

        IEnumerable<ProductBrands> GetProductBrands();
        IEnumerable<ColorList> GetColors();

        #endregion

        #region MainPage

        List<Sliders> GetMainPageSliders();
        List<Products> GetMainPageProducts();

        #region Products

        string GetSubCategory(int categoryId);
        List<Banners> GetMainPageBanners();
        List<ProductColors> GetColorsOfProduct(int productId);
        string GetFirstImageOfThisProductFromGallery(int productId);
        ProductCategories GetProductCategory(int categoryId);
        bool IsThereAnyImageForThisProductInGallery(int productId);
        string GetColor(int ColorId);
        ProductCategories GetLastCategory();
        List<Products> GetProductForCorrespondingCategory(int categoryId);
        List<Products> GetProductForCorrespondingCategoryInThisProductCategoryPage(int WesbiteCategoryId, int productCategoryId);
        Products GetProducts(int id);


        #endregion

        #region Brands

        List<ProductBrands> GetBrands();

        #endregion

        #region Boxes

        string GetBoxTitle(int id);
        List<Boxes> GetProductsOfThisBox(int id);
        List<Products> GetLatestProductsAccordingToProductCategory(int productCategoryMainId);
        List<Boxes> GetProductsOfThisBoxInThisCategory(int id, int categoryId);
        List<Products> GetLatestProductsOfThisCategory(int id);
        IEnumerable<Products> GetImazingProductsForMainPage(int imazingBoxId);
        int GetBoxCount();

        #endregion

        #endregion

        #region ChangingViewAcordingToCategory

        List<Sliders> GetCorrespondingSlider(int id);
        List<Banners> GetCorrespondingBanners(int id);

        #region Categoryies

        ProductCategories GetMainProductCategory(int categoryId);
        IEnumerable<ProductCategories> GetSubCategoriesForThisMainCategory(int categoryId);
        int GetCountOfProductInThisSubCategory(int categoryId);
        ProductCategories GetLastSubCategory(int categoryId);
        WebSiteCategories GetLastWebsiteCategory();

        #endregion

        #region Product

        IEnumerable<MyEshop.DataLayer.Entities.Products.Products> GetProductForCorrespondingSubCategory(int categoryId);

        IEnumerable<MyEshop.DataLayer.Entities.Products.Products> GetProductForCorrespondingWebsiteCategory(int categoryId);

        #endregion

        #region Boxes

        IEnumerable<Products> GetImazingProductsForProductCategory(int categoriaId);

        #endregion

        #endregion

        #region ChangingViewAcordingToWebsiteCategory

        List<Sliders> GetCorrespondingSliderForThisWebsiteCategory(int categoryId);
        List<Banners> GetCorrespondingBannerForThisWebsiteCategory(int categoryId);
        List<Boxes> GetProductsOfThisBoxInThisWebsiteCategory(int id ,int categoryId);
        List<Products> GetProductsOfAmazingBoxInThisWebsiteCategory(int categoryId);
        List<Products> GetLatestProductsOfThisWebsiteCategory(int categoryId);
        WebSiteCategories GetThisWebsiteCategory(int categoryId);
        int GetCountOfProductsInThisWebsiteCategory(int categoryId);

        #endregion

        #region Search

        IEnumerable<ProductsInSearchViewModel> SearchProducts(int pageId, int take, string filter, string getType, int startPrice, int endPrice, List<int> selectedProductCategories, List<int> selectedWebsiteCategories, List<int> selectedBrands, List<int> selectedColors);

        #endregion
    }
}
