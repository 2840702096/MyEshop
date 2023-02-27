using Microsoft.AspNetCore.Mvc;
using MyEshop.Core.Services.Interfaces;
using System.Collections.Generic;

namespace MyEShop.Controllers
{
    public class SearchController : Controller
    {
        private IClientServices _clientServices;
        public SearchController(IClientServices clientServices)
        {
            _clientServices= clientServices;
        }

        [Route("/Search")]
        public IActionResult Index()
        {
            return View("Search");
        }

        [Route("/TheSearchOperation")]

        public IActionResult TheSearchOperation(int pageId = 1, int take = 12, string filter = "", string getType = "all", int startPrice = 0, int endPrice = 100000000, List<int> selectedProductCategories = null, List<int> selectedWebsiteCategories = null, List<int> selectedBrands = null, List<int> selectedColors = null)
        {
            ViewBag.getType = getType;
            ViewBag.SelectedProductCategory = selectedProductCategories;
            ViewBag.SelectedWebsiteCategory = selectedWebsiteCategories;
            ViewBag.SelectedBrands = selectedBrands;
            ViewBag.SelectedColors = selectedColors;
            ViewBag.Filter = filter;
            ViewBag.StartPrice = startPrice;
            ViewBag.EndPrice = endPrice;
            return View("Search", _clientServices.SearchProducts(pageId, take, filter, getType, startPrice, endPrice, selectedProductCategories, selectedWebsiteCategories, selectedBrands, selectedColors));
        }
    }
}
