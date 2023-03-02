using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEshop.Core.Services;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.SliderAndBaners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyEShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IClientServices _clientServices;

        public HomeController(ILogger<HomeController> logger, IClientServices clientServices)
        {
            _logger = logger;
            _clientServices = clientServices;
        }

        public IActionResult Index()
        {
            ViewBag.Sliders = _clientServices.GetMainPageSliders();
            ViewBag.Products = _clientServices.GetMainPageProducts();
            ViewBag.Banners = _clientServices.GetMainPageBanners();
            ViewBag.FirstBox = _clientServices.GetProductsOfThisBox(1);
            ViewBag.SecondBox = _clientServices.GetProductsOfThisBox(2);
            ViewBag.FirstImazingBox = _clientServices.GetImazingProductsForMainPage(1);
            ViewBag.SecondImazingBox = _clientServices.GetImazingProductsForMainPage(2);
            return View();
        }


        public IActionResult ChangingViewAcordingToCategory(int id)
        {
            ViewBag.Sliders = _clientServices.GetCorrespondingSlider(id);
            ViewBag.Banners = _clientServices.GetCorrespondingBanners(id);
            ViewBag.FirstBox = _clientServices.GetProductsOfThisBoxInThisCategory(1, id);
            ViewBag.SecondBox = _clientServices.GetProductsOfThisBoxInThisCategory(2, id);
            ViewBag.FirstImazingBox = _clientServices.GetImazingProductsForProductCategory(id);
            ViewBag.LatestProducts = _clientServices.GetLatestProductsOfThisCategory(id);
            ViewBag.SubCategory = _clientServices.GetSubCategoriesForThisMainCategory(id);
            ViewBag.Id = id;
            return View("ChangingViewAcordingToProductCategory");
        }

        public IActionResult ChangingViewAcordingToWebsiteCategory(int id)
        {
            ViewBag.Sliders = _clientServices.GetCorrespondingSliderForThisWebsiteCategory(id);
            ViewBag.Banners = _clientServices.GetCorrespondingBannerForThisWebsiteCategory(id);
            ViewBag.FirstBox = _clientServices.GetProductsOfThisBoxInThisWebsiteCategory(1, id);
            ViewBag.SecondBox = _clientServices.GetProductsOfThisBoxInThisWebsiteCategory(2, id);
            ViewBag.FirstImazingBox = _clientServices.GetProductsOfAmazingBoxInThisWebsiteCategory(id);
            ViewBag.LatestProducts = _clientServices.GetLatestProductsOfThisWebsiteCategory(id);
            ViewBag.Id = id;
            return View("ChangingViewAcordingToWebsiteCategory");
        }

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/MyImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }

    }
}
