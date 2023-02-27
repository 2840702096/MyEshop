using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.SliderAndBaners;
using System.Collections.Generic;

namespace MyEShop.Areas.Admin.Controllers
{
    public class SlidersController : Controller
    {
        private ISliderServices _sliderServices;

        public SlidersController(ISliderServices sliderServices)
        {
            _sliderServices = sliderServices;
        }

        #region ProductCategorySliders

        [Area("Admin")]
        [Route("/Admin/ProductSliderList/{id?}")]
        public IActionResult Index(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _sliderServices.GetProductSlidersCount() / 15;
            return View(_sliderServices.GetAllSliders(pageId));
        }

        #region AddSlider

        [Area("Admin")]
        [Route("/Admin/CreateProductCategorySlider")]
        public IActionResult CreateProductCategorySlider()
        {
            var Categories = _sliderServices.GetCategoriesForSelectingInSliders();

            ViewBag.SliderTypes = new SelectList(Categories, "Value", "Text");
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateProductCategorySlider")]
        [HttpPost]
        public IActionResult CreateProductCategorySlider(AddSliderViewModel slider)
        {
            var Categories = _sliderServices.GetCategoriesForSelectingInSliders();

            ViewBag.SliderTypes = new SelectList(Categories, "Value", "Text");
            if (!ModelState.IsValid)
                return View(slider);

            _sliderServices.AddProductCategorySlider(slider);
            return Redirect("/Admin/ProductSliderList");
        }

        #endregion

        #region EditSlider

        [Area("Admin")]
        [Route("/Admin/EditProductCategorySlider/{id}")]
        public IActionResult EditProductCategorySlider(int id)
        {
            var Slider = _sliderServices.GetSliderInfoForEdit(id);

            ViewBag.CategoryOfSlider = new SelectList(_sliderServices.GetCategoriesForSelectingInSliders(), "Value", "Text", Slider.CategoryId);

            return View(Slider);
        }


        [Area("Admin")]
        [Route("/Admin/EditProductCategorySlider/{id}")]
        [HttpPost]
        public IActionResult EditProductCategorySlider(int id, string currentImage, EditSliderViewModel edit)
        {
            var Slider = _sliderServices.GetSliderInfoForEdit(id);

            ViewBag.CategoryOfSlider = new SelectList(_sliderServices.GetCategoriesForSelectingInSliders(), "Value", "Text", Slider.CategoryId);

            if (!ModelState.IsValid)
            return View(edit);

            _sliderServices.EditProductCategorySlider(id,edit,currentImage);
            return Redirect("/Admin/ProductSliderList");
        }
        #endregion

        #region Specifications

        [Area("Admin")]
        [Route("/Admin/SliderSpecifications/{id}")]
        public IActionResult Specifications(int id)
        {
            return View(_sliderServices.GetSliderInfoForSpecifications(id));
        }

        #endregion

        #region DeleteSlider

        [Route("/Admin/DeleteProductCategorySlider")]
        public IActionResult DeleteProductCategorySlider(int id)
        {
            _sliderServices.DeleteSlider(id);

            return Redirect("/Admin/ProductSliderList");
        }

        #endregion

        #region Search

        [Area("Admin")]
        public IActionResult SearchProductCategorySliders(string sliderTitle, int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _sliderServices.GetCountOfFilteredCategorySliders(sliderTitle) / 15;
            List<Sliders> Sliders = new List<Sliders>();
            Sliders.AddRange(_sliderServices.FilterProductCategorySliders(sliderTitle, pageId));
            ViewBag.SearchText = sliderTitle;
            return View("Index", Sliders);
        }

        #endregion

        #endregion

        #region Activation

        [Route("/Admin/ActivateProductCategoySlider")]
        public IActionResult ActivateProductCategoySlider(int sliderId, int WhichButton, int WhichPage)
        {
            _sliderServices.ActivateSlider(sliderId, WhichButton);

            if (WhichPage == 1)
            {
                return Redirect("/Admin/ProductSliderList");
            }
            else
            {
                if(WhichPage == 2)
                {
                    return Redirect("/Admin/WebsiteCategorySliders");
                }
                else
                {
                    return Redirect("/Admin/HomePageSliders");
                }
            }
        }

        #endregion

        #region WebsiteCategorySliders

        [Area("Admin")]
        [Route("/Admin/WebsiteCategorySliders/{id?}")]
        public IActionResult WebsiteCategorySliders(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _sliderServices.GetCountOfWebsiteCategorySliders() / 15;
            return View(_sliderServices.GetWebsiteCategorySliders(pageId));
        }

        #region AddSlider

        [Area("Admin")]
        [Route("/Admin/CreateWebsiteCategorySlider")]
        public IActionResult CreateWebsiteCategorySlider()
        {
            var Categories = _sliderServices.GetWebsiteCategoriesForSelecting();

            ViewBag.SliderTypes = new SelectList(Categories, "Value", "Text");
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateWebsiteCategorySlider")]
        [HttpPost]
        public IActionResult CreateWebsiteCategorySlider(AddSliderViewModel slider)
        {
            var Categories = _sliderServices.GetWebsiteCategoriesForSelecting();

            ViewBag.SliderTypes = new SelectList(Categories, "Value", "Text");
            if (!ModelState.IsValid)
                return View(slider);

            _sliderServices.AddWebsiteCategorySlider(slider);
            return Redirect("/Admin/WebsiteCategorySliders");
        }

        #endregion

        #region EditSlider

        [Area("Admin")]
        [Route("/Admin/EditWebsiteSlider/{id}")]
        public IActionResult EditWebsiteSlider(int id)
        {
            var Slider = _sliderServices.GetWebsiteCategorySliderInfoForEdit(id);

            var Categories = _sliderServices.GetWebsiteCategoriesForSelecting();

            ViewBag.CategoryOfSlider = new SelectList(Categories, "Value", "Text", Slider.CategoryId);

            return View(Slider);
        }

        [Area("Admin")]
        [Route("/Admin/EditWebsiteSlider/{id}")]
        [HttpPost]
        public IActionResult EditWebsiteSlider(int id, string currentImage, EditSliderViewModel edit)
        {
            var Slider = _sliderServices.GetWebsiteCategorySliderInfoForEdit(id);

            var Categories = _sliderServices.GetWebsiteCategoriesForSelecting();

            ViewBag.CategoryOfSlider = new SelectList(Categories, "Value", "Text", Slider.CategoryId);

            if (!ModelState.IsValid)
                return View(edit);

            _sliderServices.EditWebsiteSlider(id, edit, currentImage);
            return Redirect("/Admin/WebsiteCategorySliders");
        }

        #endregion

        #region Specifications

        [Area("Admin")]
        [Route("/Admin/WebsiteSliderSpecifications/{id}")]
        public IActionResult WebsiteSliderSpecifications(int id)
        {
            return View(_sliderServices.GetSliderInfoForSpecifications(id));
        }

        #endregion

        #region Delete

        [Route("/Admin/DeleteWebsiteCategorySlider")]
        public IActionResult DeleteWebsiteCategorySlider(int id)
        {
            _sliderServices.DeleteSlider(id);
            return Redirect("/Admin/WebsiteCategorySliders");
        }

        #endregion

        #region Search

        [Area("Admin")]
        public IActionResult SearchWebsiteCategorySliders(string sliderTitle, int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _sliderServices.GetCountOfFilteredCategorySliders(sliderTitle) / 15;
            List<Sliders> Sliders = new List<Sliders>();
            Sliders.AddRange(_sliderServices.FilterWebsiteCategorySliders(sliderTitle, pageId));
            ViewBag.SearchText = sliderTitle;
            return View("WebsiteCategorySliders", Sliders);
        }

        #endregion

        #endregion

        #region HomePageSliders

        [Area("Admin")]
        [Route("/Admin/HomePageSliders/{id?}")]
        public IActionResult HomePageSliders(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _sliderServices.GetCountOfHomePageSliders() / 15;
            return View(_sliderServices.GetHomePageSliders(pageId));
        }

        #region AddSlider

        [Area("Admin")]
        [Route("/Admin/CreateHomePageSliders")]
        public IActionResult CreateHomePageSliders()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateHomePageSliders")]
        [HttpPost]
        public IActionResult CreateHomePageSliders(AddSliderViewModel slider)
        {
            if (!ModelState.IsValid)
                return View(slider);

            _sliderServices.AddHomePageSlider(slider);
            return Redirect("/Admin/HomePageSliders");
        }

        #endregion

        #region EditSlider

        [Area("Admin")]
        [Route("/Admin/EditHomePageSliders/{id}")]
        public IActionResult EditHomePageSliders(int id)
        {
            return View(_sliderServices.GetHomePageSliderInfoForEdit(id));
        }

        [Area("Admin")]
        [Route("/Admin/EditHomePageSliders/{id}")]
        [HttpPost]
        public IActionResult EditHomePageSliders(int id, string currentImage, EditSliderViewModel edit)
        {
            if (!ModelState.IsValid)
                return View(edit);

            _sliderServices.EditHomePageSlider(id, edit, currentImage);
            return Redirect("/Admin/HomePageSliders");
        }

        #endregion

        #region DeleteSlider

        [Route("/Admin/DeleteHomePageSlider")]
        public IActionResult DeleteHomePageSlider(int id)
        {
            _sliderServices.DeleteSlider(id);
            return Redirect("/Admin/HomePageSliders");
        }

        #endregion

        #region Specifications

        [Area("Admin")]
        [Route("/Admin/HomePageSliserSpecifications/{id}")]
        public IActionResult HomePageSliserSpecifications(int id)
        {
            return View(_sliderServices.GetHomePageSliderSpecifications(id));
        }

        #endregion

        #region Search

        [Area("Admin")]
        [Route("/Admin/SearchHomePageSliders")]
        public IActionResult SearchHomePageSliders(string sliderTitle, int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _sliderServices.GetCountOfFilteredHomePageSlider() / 15;
            List<Sliders> FilteredSliders= new List<Sliders>();
            FilteredSliders.AddRange(_sliderServices.FilterHomePageSliders(sliderTitle, pageId));
            ViewBag.SearchText = sliderTitle;
            return View("HomePageSliders", FilteredSliders);
        }

        #endregion

        #endregion

    }
}
