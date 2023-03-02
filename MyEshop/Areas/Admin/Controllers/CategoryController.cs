using Microsoft.AspNetCore.Mvc;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Categories;
using MyEShop.DataLayer.Entities.Categories;
using System.Collections.Generic;

namespace MyEShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        #region ProductCategories

        [Area("Admin")]
        [Route("/Admin/ProductCategories/{id?}")]
        public IActionResult ProductCategories()
        {
            return View(_categoryServices.GetAllCategories());
        }

        #region MainGroup

        #region AddCategory

        [Area("Admin")]
        [Route("/Admin/AddCategory")]
        public IActionResult AddCategory()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/AddCategory")]
        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryServices.AddCategory(category);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #region EditCategory

        [Area("Admin")]
        [Route("/Admin/EditMainGroup/{id}")]
        public IActionResult EditMainGroup(int id)
        {
            EditCategoryViewModel edit = _categoryServices.GetCategoryInfoForEdit(id);

            return View(edit);
        }

        [Area("Admin")]
        [Route("/Admin/EditMainGroup/{id}")]
        [HttpPost]
        public IActionResult EditMainGroup(int id, EditCategoryViewModel edit, string currntPhoto)
        {
            if (!ModelState.IsValid)
            {
                return View("EditMainGroup", edit);
            }

            _categoryServices.EditMainGroup(id, edit, currntPhoto);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #region DeleteCategories

        [Area("Admin")]
        [Route("/Admin/DeleteGroup/{id}")]
        public IActionResult DeleteCategorY(int id)
        {
            if (_categoryServices.IsSubCategoryExist(id) == true)
            {
                ViewBag.ItExists = true;
                return View("ProductCategories", _categoryServices.GetAllCategories());
            }

            _categoryServices.DeleteMainGroup(id);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #endregion

        #region SubCategory

        #region AddCategory

        [Area("Admin")]
        [Route("/Admin/AddSubCategory/{id}")]
        public IActionResult AddSubCategory(int id)
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/AddSubCategory/{id}")]
        [HttpPost]
        public IActionResult AddSubCategory(AddCategoryViewModel category, int id)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryServices.AddSubCategory(id, category);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #region EditCategory

        [Area("Admin")]
        [Route("/Admin/EditSubGroup/{id}")]
        public IActionResult EditSubGroup(int id)
        {

            return View(_categoryServices.GetCategoryInfoForEdit(id));
        }

        [Area("Admin")]
        [Route("/Admin/EditSubGroup/{id}")]
        [HttpPost]
        public IActionResult EditSubGroup(EditCategoryViewModel edit, int id, string currentPhoto)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }

            _categoryServices.EditSubCategory(id, edit, currentPhoto);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #region DeleteCategory

        [Area("Admin")]
        [Route("/Admin/DeleteSubCategory/{id}")]
        public IActionResult DeleteSubCategory(int id)
        {
            if (_categoryServices.IsThirdSubCategoryExist(id) == true)
            {
                ViewBag.TheThirdSubCategoryExists = true;
                return View("ProductCategories", _categoryServices.GetAllCategories());
            }

            _categoryServices.DeleteMainGroup(id);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #endregion

        #region SubSet

        #region AddSubSet

        [Area("Admin")]
        [Route("/Admin/AddSubSet/{id}")]
        public IActionResult AddSubSet(int id)
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/AddSubSet/{id}")]
        [HttpPost]
        public IActionResult AddSubSet(string categoryTitle, int id)
        {
            if (categoryTitle == null)
            {
                ViewBag.Error = true;
                return View();
            }

            _categoryServices.AddSubSet(id, categoryTitle);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #region EditSubSet

        [Area("Admin")]
        [Route("/Admin/EditThirdSubCategory/{id}")]
        public IActionResult EditThirdSubCategory(int id)
        {
            return View(_categoryServices.GetCategoryInfoForEdit(id));
        }

        [Area("Admin")]
        [Route("/Admin/EditThirdSubCategory/{id}")]
        [HttpPost]
        public IActionResult EditThirdSubCategory(EditCategoryViewModel edit, int id)
        {
            if (!ModelState.IsValid)
                return View(edit);

            _categoryServices.EditSubSet(id, edit);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #region DeleteSubSet

        [Area("Admin")]
        [Route("/Admin/DeleteSubSet/{id}")]
        public IActionResult DeleteSubSet(int id)
        {
            if (_categoryServices.IsThereAnySliderInThisSubset(id) == true && _categoryServices.IsThereAnyBannersInThisSubset(id) == true)
            {
                ViewBag.ThereAreSlidersAndBanners = true;
                return View("ProductCategories", _categoryServices.GetAllCategories());
            }
            else
            {
                if (_categoryServices.IsThereAnySliderInThisSubset(id) == true)
                {
                    ViewBag.ThereAreSliders = true;
                    return View("ProductCategories", _categoryServices.GetAllCategories());
                }
                if (_categoryServices.IsThereAnyBannersInThisSubset(id) == true)
                {
                    ViewBag.ThereAreBanners = true;
                    return View("ProductCategories", _categoryServices.GetAllCategories());
                }
            }

            if (_categoryServices.IsThereAnyProductInThisCategory(id) == true)
            {
                ViewBag.ThereAreProductsInThisCategory = true;
                return View("ProductCategories", _categoryServices.GetAllCategories());
            }

            _categoryServices.DeleteMainGroup(id);

            return Redirect("/Admin/ProductCategories");
        }

        #endregion

        #endregion

        #region Specifications

        [Area("Admin")]
        [Route("/Admin/Specifications/{id}")]
        public IActionResult Specifications(int id)
        {
            return View(_categoryServices.GetSpcificationsOfCategories(id));
        }

        #endregion

        #region TheTitlesOfProductSpecifications

        [Area("Admin")]
        [Route("/Admin/TheTitlesOfProductSpecifications/{id}")]
        public IActionResult TheTitlesOfProductSpecifications(int id)
        {
            ViewBag.Id = id;
            return View(_categoryServices.GetSpecificationsInCategories(id));
        }

        [Area("Admin")]
        [Route("/Admin/AddTheTitlesOfProductSpecifications")]
        public IActionResult AddTheTitlesOfProductSpecifications(int id, string specificationTitle)
        {
            if (specificationTitle == null)
            {
                ViewBag.Id = id;
                ViewBag.Error = true;
                return View("TheTitlesOfProductSpecifications", _categoryServices.GetSpecificationsInCategories(id));
            }

            _categoryServices.AddTheTitleOfProductSpecifications(specificationTitle, id);

            return Redirect("/Admin/TheTitlesOfProductSpecifications/" + id);
        }

        [Area("Admin")]
        [Route("/Admin/DeleteTheTitlesOfProductSpecifications/{id}")]
        public IActionResult DeleteTheTitlesOfProductSpecifications(int id, int categoryId)
        {
            if (_categoryServices.IsThereAnySpecificationValuesInThisTitle(id) == true)
            {
                ViewBag.TherIsValueInIt = true;
                ViewBag.Id = categoryId;
                return View("TheTitlesOfProductSpecifications", _categoryServices.GetSpecificationsInCategories(categoryId));
            }

            _categoryServices.DeleteTheTitleOfProductSpecifications(id);

            return Redirect("/Admin/TheTitlesOfProductSpecifications/" + categoryId);
        }

        #endregion

        #region TitlesOfEvaluationOfProduct

        [Area("Admin")]
        [Route("/Admin/TitlesOfEvaluationOfProduct/{Id}")]
        public IActionResult TitlesOfEvaluationOfProduct(int id)
        {
            ViewBag.Id = id;
            return View(_categoryServices.GetSlideCommentTitles(id));
        }

        [Area("Admin")]
        [Route("/Admin/TitlesOfEvaluationOfProduct/{Id}")]
        [HttpPost]
        public IActionResult TitlesOfEvaluationOfProduct(int id, string evalutionTitle)
        {
            ViewBag.Id = id;
            if (evalutionTitle == null)
            {
                ViewBag.Error = true;
                return View(_categoryServices.GetSlideCommentTitles(id));
            }

            _categoryServices.AddATitleToSlideCommentTitles(evalutionTitle, id);

            return Redirect($"/Admin/TitlesOfEvaluationOfProduct/{id}");
        }

        [Area("Admin")]
        [Route("/Admin/EditTitlesOfEvaluationOfProduct/{id}")]
        public IActionResult EditTitlesOfEvaluationOfProduct(int id, int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            ViewBag.EvaluationTitle = _categoryServices.GetSlideCommentTitle(id);
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/EditTitlesOfEvaluationOfProduct/{id}")]
        [HttpPost]
        public IActionResult EditTitlesOfEvaluationOfProduct(int id,int categoryId, string title)
        {
            if(title == null)
            {
                ViewBag.CategoryId = categoryId;
                ViewBag.Error = true;
                return View();
            }

            _categoryServices.EditSlideCommentTitle(id, title);

            return Redirect($"/Admin/TitlesOfEvaluationOfProduct/{categoryId}");
        }

        #endregion

        #endregion

        #region WebSiteCategories

        [Area("Admin")]
        [Route("/Admin/WebSiteCategories/{id?}")]
        public IActionResult WebSiteCategories(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.CountOfGroups = _categoryServices.GetCountOfWebsiteCategories() / 15;

            return View(_categoryServices.GetWebSiteCategories(pageId));
        }

        #region AddCategory

        [Area("Admin")]
        [Route("/Admin/AddWebSiteCategory")]
        public IActionResult AddWebSiteCategory()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/AddWebSiteCategory")]
        [HttpPost]
        public IActionResult AddWebSiteCategory(AddWebsiteCategoryViewModel category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryServices.AddCategory(category);

            return Redirect("/Admin/WebSiteCategories");
        }

        #endregion

        #region EditCategory

        [Area("Admin")]
        [Route("/Admin/EditWebsiteCategory/{id}")]
        public IActionResult EditWebsiteCategory(int id)
        {
            var category = _categoryServices.GetWebsiteCategoryInfoForEdit(id);

            return View(category);
        }

        [Area("Admin")]
        [Route("/Admin/EditWebsiteCategory/{id}")]
        [HttpPost]
        public IActionResult EditWebsiteCategory(EditWebsiteCategoryViewModel editCategory, int id, string currentPhoto)
        {
            if (!ModelState.IsValid)
                return View("EditWebsiteCategory", editCategory);

            _categoryServices.EditCategory(id, editCategory, currentPhoto);

            return Redirect("/Admin/WebSiteCategories");
        }

        #endregion

        #region DeleteCategory

        [Area("Admin")]
        [Route("/Admin/DeleteWebseiteCategory/{id}")]
        public IActionResult DeleteWebseiteCategory(int id, int pageId = 1)
        {
            if (_categoryServices.IsThereAnySliderInThisCategory(id) == true && _categoryServices.IsThereAnyBannersInThisCategory(id) == true)
            {
                ViewBag.ThereAreSlidersAndBanners = true;
                return View("WebSiteCategories", _categoryServices.GetWebSiteCategories(pageId));
            }
            else
            {
                if (_categoryServices.IsThereAnySliderInThisCategory(id) == true)
                {
                    ViewBag.ThereAreSliders = true;
                    return View("WebSiteCategories", _categoryServices.GetWebSiteCategories(pageId));
                }
                if (_categoryServices.IsThereAnyBannersInThisCategory(id) == true)
                {
                    ViewBag.ThereAreBanners = true;
                    return View("WebSiteCategories", _categoryServices.GetWebSiteCategories(pageId));
                }
            }

            if (_categoryServices.IsThereAnyProductInThisWebsiteCategory(id) == true)
            {
                ViewBag.ThereAreProducts = true;
                return View("WebSiteCategories", _categoryServices.GetWebSiteCategories(pageId));
            }

            _categoryServices.DeleteWebsiteCategory(id);

            return Redirect("/Admin/WebSiteCategories");
        }

        #endregion

        #region Specifications

        [Area("Admin")]
        [Route("/Admin/WebCategorySpecifications/{id}")]
        public IActionResult WebCategorySpecifications(int id)
        {
            return View(_categoryServices.GetCategoryForSpecifications(id));
        }

        #endregion

        #endregion

        #region WeblogCategories

        [Area("Admin")]
        [Route("/Admin/WeblogCategories")]
        public IActionResult WeblogCategories()
        {
            return View(_categoryServices.GetAllWeblogCategories());
        }

        #region CreateWeblogCategory

        [Area("Admin")]
        [Route("/Admin/CreateWeblogCategory")]
        public IActionResult CreateWeblogCategory()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateWeblogCategory")]
        [HttpPost]
        public IActionResult CreateWeblogCategory(string title)
        {
            if (title == null)
            {
                ViewBag.EmptyTitle = true;
                return View();
            }

            _categoryServices.AddWeblogCategory(0, title);

            return Redirect("/Admin/WeblogCategories");
        }

        #endregion

        #region EditWeblogCategories

        [Area("Admin")]
        [Route("/Admin/EditWeblogCategories/{id}")]
        public IActionResult EditWeblogCategories(int id)
        {
            ViewBag.Text = _categoryServices.GetWeblogCategoryTitle(id);
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/EditWeblogCategories/{id}")]
        [HttpPost]
        public IActionResult EditWeblogCategories(int id, string title)
        {
            if (title == null)
            {
                ViewBag.EmptyTitle = true;
                return View();
            }

            _categoryServices.EditWeblogCategory(id, title);

            return Redirect("/Admin/WeblogCategories");
        }

        #endregion

        #region Delete

        [Area("Admin")]
        [Route("/Admin/DeleteWeblogCategories/{id}")]
        public IActionResult DeleteWeblogCategories(int id)
        {
            if (_categoryServices.IsThereAnySubCategoriesInThisCategory(id))
            {
                ViewBag.IsThereSubCategory = true;
                return View("WeblogCategories", _categoryServices.GetAllWeblogCategories());
            }

            if (_categoryServices.IsThereAnyWeblogsInThisCategory(id))
            {
                ViewBag.IsThereWeblogs = true;
                return View("WeblogCategories", _categoryServices.GetAllWeblogCategories());
            }

            _categoryServices.DeleteWeblogCategory(id);

            return Redirect("/Admin/WeblogCategories");
        }

        #endregion

        #region SubCategory

        #region CreateWeblogSubCategory

        [Area("Admin")]
        [Route("/Admin/CreateWeblogSubCategory/{id}")]
        public IActionResult CreateWeblogSubCategory(int id)
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateWeblogSubCategory/{id}")]
        [HttpPost]
        public IActionResult CreateWeblogSubCategory(int id, string title)
        {
            if (title == null)
            {
                ViewBag.EmptyTitle = true;
                return View();
            }

            _categoryServices.AddWeblogCategory(id, title);

            return Redirect("/Admin/WeblogCategories");
        }

        #endregion

        #endregion

        #endregion

        #region RepetitiveQuestionCategories

        [Area("Admin")]
        [Route("/Admin/RepetitiveQuestionCategories")]
        public IActionResult RepetitiveQuestionCategories(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.CountOfGroups = _categoryServices.GetCountOfRepetitiveQuestionCategories() / 7;
            return View(_categoryServices.GetAllRQCategories(pageId));
        }

        #region CreateRepetitiveQuestionCategory

        [Area("Admin")]
        [Route("/Admin/CreateRepetitiveQuestionCategory")]
        public IActionResult CreateRepetitiveQuestionCategory()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateRepetitiveQuestionCategory")]
        [HttpPost]
        public IActionResult CreateRepetitiveQuestionCategory(AddCategoryViewModel category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryServices.AddQuestionCategory(category);

            return Redirect("/Admin/RepetitiveQuestionCategories");
        }

        #endregion

        #region EditRepetitiveQuestionCategory

        [Area("Admin")]
        [Route("/Admin/EditRepetitiveQuestionCategory/{id}")]
        public IActionResult EditRepetitiveQuestionCategory(int id)
        {
            return View(_categoryServices.GetQuestionInfoForEdit(id));
        }

        [Area("Admin")]
        [Route("/Admin/EditRepetitiveQuestionCategory/{id}")]
        [HttpPost]
        public IActionResult EditRepetitiveQuestionCategory(int id, EditCategoryViewModel category, string currentImage)
        {
            if (!ModelState.IsValid)
                return View(_categoryServices.GetQuestionInfoForEdit(id));

            _categoryServices.EditQuestionCategories(id, category, currentImage);

            return Redirect("/Admin/RepetitiveQuestionCategories");
        }

        #endregion

        #region DeleteRepetitiveQuestionCategory

        [Area("Admin")]
        [Route("/Admin/DeleteRepetitiveQuestionCategory/{id}")]
        public IActionResult DeleteRepetitiveQuestionCategory(int id, int pageId = 1)
        {
            if (_categoryServices.IsThereAnyQuestionInThisCategory(id) == true)
            {
                ViewBag.PageId = pageId;
                ViewBag.CountOfGroups = _categoryServices.GetCountOfRepetitiveQuestionCategories() / 7;
                ViewBag.ThereIsError = true;
                return View("RepetitiveQuestionCategories", _categoryServices.GetAllRQCategories(pageId));
            }

            _categoryServices.DeleteQuestionCategory(id);

            return Redirect("/Admin/RepetitiveQuestionCategories");
        }

        #endregion

        #region RepetitiveQuestionCategory

        [Area("Admin")]
        [Route("/Admin/RepetitiveQuestionCategory/{id}")]
        public IActionResult RepetitiveQuestionCategory(int id)
        {
            return View(_categoryServices.GetRepetitiveQuestionCategory(id));
        }

        #endregion

        #endregion

    }
}
