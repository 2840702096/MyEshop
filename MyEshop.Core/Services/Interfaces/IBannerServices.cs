using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.DataLayer.Entities.SliderAndBaners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IBannerervices
    {
        #region HomePageBanners

        IEnumerable<Banners> GetHomePageBanners(int pageId);
        int GetHomePageBannersCount();
        void AddHomePageBanner(AddBannerViewModel banner);
        void AddBanner(Banners banner);
        EditBannerViewModel GetBannerInfoForEdit(int id);
        void EditHomePageBanner(int id, EditBannerViewModel banner, string currentImage);
        void EditBanner(Banners banner);
        IEnumerable<Banners> FilterHomePageBanners(string bannerTitle, int pageId);
        int GetCountFilteredHomePageBanners(string bannerTitle);
        bool IsThereAnyHomePageBannerInThisNumber(int id, int bannerNumber);

        #endregion

        #region ProductCategoryBanners

        IEnumerable<Banners> GetProductCategoryBanners(int pageId);
        int GetCountOfProductCategoryBanners();
        string GetProductCategoryTitle(int id);
        List<SelectListItem> GetProductCategoriesForSelecting();
        void AddProductCategoryBanner(AddBannerViewModel banner);
        EditBannerViewModel GetProductCategoryBannerInfoForEdit(int id);
        void EditProductCategoryBanner(int id, EditBannerViewModel banner, string currentImage);
        IEnumerable<Banners> FilterProductCategoryBanner(string bannerTitle, int pageId);
        int GetCountOfFilteredProductCategoryBanners(string bannerTitle);
        bool IsThereAnyBannerInThisNumber(int id,int bannerNumber);

        #endregion

        #region WebsiteCategoryBanners

        IEnumerable<Banners> GetWebsiteCategoryBanners(int pageId);
        int GetCountOfWebsiteCategoryBanners();
        string GetWebsiteCategoryTitle(int categoryId);
        List<SelectListItem> GetWebsiteCategoriesForSelecting();
        void AddWebsiteCategoryBanner(AddBannerViewModel banner);
        EditBannerViewModel GetWebsiteCategoryBannerInfoForEdit(int id);
        void EditWebsiteCategoryBanner(int id, EditBannerViewModel banner, string currentImage);
        IEnumerable<Banners> FilterWebsiteCategoryBanner(string bannerTitle, int pageId);
        int GetCountOfFilteredWebsiteCategoryBanners(string bannerTitle);
        bool IsThereAnyWebsiteCategoryBannerInThisNumber(int id, int bannerNumber);

        #endregion

        Banners GetBanner(int id);
        void DeleteBanner(int id);
        void ActivateBanner(int id, int whichButton);


    }
}
