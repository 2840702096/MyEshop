using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.SliderAndBaners;
using System.Collections.Generic;

namespace MyEShop.Areas.Admin.Controllers
{
    public class BannerController : Controller
    {
        private IBannerervices _bannerServices;

        public BannerController(IBannerervices bannerServices)
        {
            _bannerServices = bannerServices;
        }

        #region HomePageBanners

        [Area("Admin")]
        [Route("/Admin/HomePageBanners/{id?}")]
        public IActionResult HomePageBanners(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _bannerServices.GetHomePageBannersCount() / 15;
            return View(_bannerServices.GetHomePageBanners(pageId));
        }

        #region CreateHomePageBanners

        [Area("Admin")]
        [Route("/Admin/CreateHomePageBanners")]
        public IActionResult CreateHomePageBanners()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateHomePageBanners")]
        [HttpPost]
        public IActionResult CreateHomePageBanners(AddBannerViewModel banner)
        {
            if (!ModelState.IsValid)
                return View(banner);

            if (_bannerServices.IsThereAnyHomePageBannerInThisNumber(0, banner.BannerNumber) == true)
            {
                ViewBag.BannerNumberError = true;
                return View(banner);
            }

            _bannerServices.AddHomePageBanner(banner);
            return Redirect("/Admin/HomePageBanners");
        }

        #endregion

        #region EditHomePageBanners

        [Area("Admin")]
        [Route("/Admin/EditHomePageBanners/{id}")]
        public IActionResult EditHomePageBanners(int id)
        {
            return View(_bannerServices.GetBannerInfoForEdit(id));
        }

        [Area("Admin")]
        [Route("/Admin/EditHomePageBanners/{id}")]
        [HttpPost]
        public IActionResult EditHomePageBanners(int id, EditBannerViewModel banner, string currentImage)
        {
            if (!ModelState.IsValid)
                return View(banner);

            if (_bannerServices.IsThereAnyHomePageBannerInThisNumber(id, banner.BannerNumber) == true)
            {
                ViewBag.BannerNumberError = true;
                return View(banner);
            }

            _bannerServices.EditHomePageBanner(id, banner, currentImage);

            return Redirect("/Admin/HomePageBanners");
        }

        #endregion

        #region DeleteHomePageBanner

        [Route("/Admin/DeleteHomePageBanner")]
        public IActionResult DeleteHomePageBanner(int id)
        {
            _bannerServices.DeleteBanner(id);
            return Redirect("/Admin/HomePageBanners");
        }

        #endregion

        #region Specifications

        [Area("Admin")]
        [Route("/Admin/HomePageBannerSpecifications/{id}")]
        public IActionResult HomePageBannerSpecifications(int id)
        {
            return View(_bannerServices.GetBanner(id));
        }

        #endregion

        #region SearchHomePageBanners

        [Area("Admin")]
        [Route("/Admin/SearchHomePageBanners")]
        public IActionResult SearchHomePageBanners(string bannerTitle, int pageId = 1)
        {
            List<Banners> FilteredBanners = new List<Banners>();
            FilteredBanners.AddRange(_bannerServices.FilterHomePageBanners(bannerTitle, pageId));
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _bannerServices.GetCountFilteredHomePageBanners(bannerTitle) / 15;
            ViewBag.SearchText = bannerTitle;
            return View("HomePageBanners", FilteredBanners);
        }

        #endregion

        #endregion

        #region ProductCategoryBanners

        [Area("Admin")]
        [Route("/Admin/ProductCategoryBanners/{id?}")]
        public IActionResult ProductCategoryBanners(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _bannerServices.GetCountOfProductCategoryBanners() / 15;
            return View(_bannerServices.GetProductCategoryBanners(pageId));
        }

        #region CreateProductCategoryBanners

        [Area("Admin")]
        [Route("/Admin/CreateProductCategoryBanners")]
        public IActionResult CreateProductCategoryBanners()
        {
            ViewBag.Categories = new SelectList(_bannerServices.GetProductCategoriesForSelecting(), "Value", "Text");

            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateProductCategoryBanners")]
        [HttpPost]
        public IActionResult CreateProductCategoryBanners(AddBannerViewModel banner)
        {
            ViewBag.Categories = new SelectList(_bannerServices.GetProductCategoriesForSelecting(), "Value", "Text");

            if (!ModelState.IsValid)
                return View(banner);

            if (_bannerServices.IsThereAnyBannerInThisNumber(0, banner.BannerNumber) == true)
            {
                ViewBag.BannerNumberError = true;
                ViewBag.Categories = new SelectList(_bannerServices.GetProductCategoriesForSelecting(), "Value", "Text");
                return View(banner);
            }

            _bannerServices.AddProductCategoryBanner(banner);

            return Redirect("/Admin/ProductCategoryBanners");
        }
        #endregion

        #region EditProductCategoryBanners

        [Area("Admin")]
        [Route("/Admin/EditProductCategoryBanners/{id}")]
        public IActionResult EditProductCategoryBanners(int id)
        {
            var Banner = _bannerServices.GetProductCategoryBannerInfoForEdit(id);

            ViewBag.Categories = new SelectList(_bannerServices.GetProductCategoriesForSelecting(), "Value", "Text", Banner.CategoryId);

            return View(Banner);
        }

        [Area("Admin")]
        [Route("/Admin/EditProductCategoryBanners/{id}")]
        [HttpPost]
        public IActionResult EditProductCategoryBanners(int id, EditBannerViewModel banner, string currentImage)
        {
            var Banner = _bannerServices.GetProductCategoryBannerInfoForEdit(id);
            ViewBag.Categories = new SelectList(_bannerServices.GetProductCategoriesForSelecting(), "Value", "Text", Banner.CategoryId);

            if (!ModelState.IsValid)
                return View(banner);

            if (_bannerServices.IsThereAnyBannerInThisNumber(id, banner.BannerNumber) == true)
            {
                ViewBag.BannerNumberError = true;
                return View(Banner);
            }

            _bannerServices.EditProductCategoryBanner(id, banner, currentImage);
            return Redirect("/Admin/ProductCategoryBanners");
        }

        #endregion

        #region DeleteProductCategoryBanners

        [Route("/Admin/DeleteProductCategoryBanners")]
        public IActionResult DeleteProductCategoryBanners(int id)
        {
            _bannerServices.DeleteBanner(id);
            return Redirect("/Admin/ProductCategoryBanners");
        }

        #endregion

        #region ProductCategoryBannerSpecifications

        [Area("Admin")]
        [Route("/Admin/ProductCategoryBannerSpecifications/{id}")]
        public IActionResult ProductCategoryBannerSpecifications(int id)
        {
            return View(_bannerServices.GetBanner(id));
        }

        #endregion

        #region SearchProductCategoryBanners

        [Area("Admin")]
        [Route("/Admin/SearchProductCategoryBanners")]
        public IActionResult SearchProductCategoryBanners(string bannerTitle, int pageId = 1)
        {
            List<Banners> FilteredBanners = new List<Banners>();
            FilteredBanners.AddRange(_bannerServices.FilterProductCategoryBanner(bannerTitle, pageId));
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _bannerServices.GetCountOfFilteredProductCategoryBanners(bannerTitle) / 15;
            ViewBag.SearchText = bannerTitle;
            return View("ProductCategoryBanners", FilteredBanners);
        }

        #endregion

        #endregion

        #region WebsiteCategoryBanners

        [Area("Admin")]
        [Route("/Admin/WebsiteCategoryBanners/{id?}")]
        public IActionResult WebsiteCategoryBanners(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _bannerServices.GetCountOfWebsiteCategoryBanners() / 15;
            return View(_bannerServices.GetWebsiteCategoryBanners(pageId));
        }

        #region CreateWebsiteCategoryBanner

        [Area("Admin")]
        [Route("/Admin/CreateWebsiteCategoryBanner")]
        public IActionResult CreateWebsiteCategoryBanner()
        {
            ViewBag.Categories = new SelectList(_bannerServices.GetWebsiteCategoriesForSelecting(), "Value", "Text");
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateWebsiteCategoryBanner")]
        [HttpPost]
        public IActionResult CreateWebsiteCategoryBanner(AddBannerViewModel banner)
        {
            ViewBag.Categories = new SelectList(_bannerServices.GetWebsiteCategoriesForSelecting(), "Value", "Text");

            if (!ModelState.IsValid)
                return View(banner);

            if (_bannerServices.IsThereAnyWebsiteCategoryBannerInThisNumber(0, banner.BannerNumber) == true)
            {
                ViewBag.BannerNumberError = true;
                ViewBag.Categories = new SelectList(_bannerServices.GetWebsiteCategoriesForSelecting(), "Value", "Text");
                return View(banner);
            }

            _bannerServices.AddWebsiteCategoryBanner(banner);

            return Redirect("/Admin/WebsiteCategoryBanners");
        }

        #endregion

        #region EditWebsiteCategoryBanner

        [Area("Admin")]
        [Route("/Admin/EditWebsiteCategoryBanner/{id}")]
        public IActionResult EditWebsiteCategoryBanner(int id)
        {
            var Banner = _bannerServices.GetWebsiteCategoryBannerInfoForEdit(id);

            ViewBag.Categories = new SelectList(_bannerServices.GetWebsiteCategoriesForSelecting(), "Value", "Text", Banner.CategoryId);

            return View(Banner);
        }

        [Area("Admin")]
        [Route("/Admin/EditWebsiteCategoryBanner/{id}")]
        [HttpPost]
        public IActionResult EditWebsiteCategoryBanner(int id, EditBannerViewModel banner, string currentImage)
        {
            var Banner = _bannerServices.GetWebsiteCategoryBannerInfoForEdit(id);
            ViewBag.Categories = new SelectList(_bannerServices.GetWebsiteCategoriesForSelecting(), "Value", "Text", Banner.CategoryId);

            if (!ModelState.IsValid)
                return View(banner);

            if (_bannerServices.IsThereAnyWebsiteCategoryBannerInThisNumber(id, banner.BannerNumber) == true)
            {
                ViewBag.BannerNumberError = true;
                return View(Banner);
            }

            _bannerServices.EditWebsiteCategoryBanner(id, banner, currentImage);
            return Redirect("/Admin/WebsiteCategoryBanners");
        }

        #endregion

        #region DeleteWebsiteCategoryBanner

        [Area("Admin")]
        [Route("/Admin/DeleteWebsiteCategoryBanner")]
        public IActionResult DeleteWebsiteCategoryBanner(int id)
        {
            _bannerServices.DeleteBanner(id);

            return Redirect("/Admin/WebsiteCategoryBanners");
        }

        #endregion

        #region WebsiteCategoryBannerSpecifications

        [Area("Admin")]
        [Route("/Admin/WebsiteCategoryBannerSpecifications/{id}")]
        public IActionResult WebsiteCategoryBannerSpecifications(int id)
        {
            return View(_bannerServices.GetBanner(id));
        }

        #endregion

        #region FilterWebsiteCategoryBanners

        [Area("Admin")]
        [Route("/Admin/FilterWebsiteCategoryBanners")]
        public IActionResult FilterWebsiteCategoryBanners(string bannerTitle, int pageId = 1)
        {
            List<Banners> FilteredBanners = new List<Banners>();
            FilteredBanners.AddRange(_bannerServices.FilterWebsiteCategoryBanner(bannerTitle, pageId));
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _bannerServices.GetCountOfFilteredWebsiteCategoryBanners(bannerTitle) / 15;
            ViewBag.SearchText = bannerTitle;
            return View("WebsiteCategoryBanners", FilteredBanners);
        }

        #endregion

        #endregion

        #region Activation

        [Route("/Admin/Activation")]
        public IActionResult Activation(int bannerId, int WhichButton, int WhichPage)
        {
            _bannerServices.ActivateBanner(bannerId, WhichButton);

            if (WhichPage == 1)
            {
                return Redirect("/Admin/HomePageBanners");
            }
            else
            {
                if (WhichPage == 2)
                {
                    return Redirect("/Admin/ProductCategoryBanners");
                }
                else
                {
                    return Redirect("/Admin/WebsiteCategoryBanners");
                }
            }
        }

        #endregion

    }
}
