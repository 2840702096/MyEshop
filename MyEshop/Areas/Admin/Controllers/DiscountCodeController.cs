using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using System;
using System.Globalization;

namespace MyEShop.Areas.Admin.Controllers
{
    public class DiscountCodeController : Controller
    {
        private IDiscountServices _discountServices;

        public DiscountCodeController(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }

        #region NormalDiscountCodes

        [Area("Admin")]
        [Route("/Admin/NormalDiscountCodes")]
        public IActionResult Index(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _discountServices.GetCountOfDiscountCodes() / 15;
            return View(_discountServices.GetAllDiscountCodes(pageId));
        }

        #endregion

        #region CreateDiscountCode

        [Area("Admin")]
        [Route("/Admin/CreateDiscountCode/{id}")]
        public IActionResult CreateDiscountCode(int id)
        {
            if(id == 1)
            {
                ViewBag.Category = false;
            }
            else
            {
                ViewBag.Category = true;
                ViewBag.Categories = new SelectList(_discountServices.GetProductCategoriesForDiscount(),"Value","Text");
            }
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateDiscountCode/{id}")]
        [HttpPost]
        public IActionResult CreateDiscountCode(int id, DiscountCodesViewModel discountCode, string stDate = "", string edDate = "")
        {

            if(id == 1)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Category = false;
                    return View(discountCode);
                }
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    ViewBag.Category = true;
                    ViewBag.Categories = new SelectList(_discountServices.GetProductCategoriesForDiscount(), "Value", "Text");
                    return View(discountCode);
                }

            }


            _discountServices.AddDiscountCode(discountCode, stDate, edDate);

            if (id == 1)
            {
                return Redirect("/Admin/NormalDiscountCodes");
            }
            else
            {
                return Redirect("/Admin/DiscountCodesForProductCategories");
            }
        }

        #endregion

        #region EditDiscountCode

        [Area("Admin")]
        [Route("/Admin/EditDiscountCode/{id}")]
        public IActionResult EditDiscountCode(int id, int pageId)
        {
            var Discount = _discountServices.GetDiscountIfoForEdit(id);

            if (pageId == 1)
            {
                ViewBag.Category = false;
            }
            else
            {
                ViewBag.Category = true;
                ViewBag.Categories = new SelectList(_discountServices.GetProductCategoriesForDiscount(), "Value", "Text", Discount.ProductCategoryId);
            }
            return View(Discount);
        }

        [Area("Admin")]
        [Route("/Admin/EditDiscountCode/{id}")]
        [HttpPost]
        public IActionResult EditDiscountCode(int id, int pageId, DiscountCodesViewModel discount, string stDate, string edDate)
        {
            var Discount = _discountServices.GetDiscountIfoForEdit(id);
            if (pageId == 1)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Category = false;
                    return View(Discount);
                }
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Category = true;
                    ViewBag.Categories = new SelectList(_discountServices.GetProductCategoriesForDiscount(), "Value", "Text", Discount.ProductCategoryId);
                    return View(Discount);
                }

            }

            _discountServices.EditDiscountCode(id, discount, stDate, edDate);

            if (pageId == 1)
            {
                return Redirect("/Admin/NormalDiscountCodes");
            }
            else
            {
                return Redirect("/Admin/DiscountCodesForProductCategories");
            }
        }

        #endregion

        #region DeleteDiscountCode

        [Area("Admin")]
        [Route("/Admin/DeleteDiscountCode/{id}")]
        public IActionResult DeleteDiscountCode(int id,int pageId)
        {
            _discountServices.DeleteDiscountCode(id);

            if (pageId == 1)
            {
                return Redirect("/Admin/NormalDiscountCodes");
            }
            else
            {
                return Redirect("/Admin/DiscountCodesForProductCategories");
            }
        }

        #endregion

        #region DiscountCodesForProductCategories

        [Area("Admin")]
        [Route("/Admin/DiscountCodesForProductCategories")]
        public IActionResult DiscountCodesForProductCategories(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _discountServices.GetCountOfDiscountCodesForProductCategories() / 15;
            return View(_discountServices.GetAllDiscountCodesForProductCategories(pageId));
        }

        #endregion
    }
}
