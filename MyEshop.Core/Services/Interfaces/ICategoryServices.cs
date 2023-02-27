using MyEshop.Core.DTOs;
using MyEshop.DataLayer.Entities.Categories;
using MyEshop.DataLayer.Entities.Products;
using MyEShop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface ICategoryServices
    {
        #region ProductCategories

        List<ProductCategories> GetAllCategories();
        List<ProductCategories> GetThirSubCategories(int categoryId);
        void AddCategory(AddCategoryViewModel category);
        EditCategoryViewModel GetCategoryInfoForEdit(int categoryId);
        void EditMainGroup(int id, EditCategoryViewModel edit, string CurrentPhoto);
        void DeleteMainGroup(int id);
        int GetCategoryCount();
        bool IsSubCategoryExist(int categoryId);
        void ReWriteCategory(int id);
        ProductCategories GetCategory(int id);
        void AddSubCategory(int id, AddCategoryViewModel subCategory);
        void EditSubCategory(int id, EditCategoryViewModel edit, string currntPhoto);
        bool IsThirdSubCategoryExist(int categoryId);
        void AddSubSet(int id, string categoryTitle);
        void EditSubSet(int id, EditCategoryViewModel edit);
        ProductCategories GetSpcificationsOfCategories(int categoryId);
        int GetCountOfFilteredCategories(string categoryTitle);
        bool IsThereAnySliderInThisSubset(int categoryId);
        bool IsThereAnyBannersInThisSubset(int categoryId);
        IEnumerable<SpecificationsInCategories> GetSpecificationsInCategories(int categoryId);
        void AddTheTitleOfProductSpecifications(string specificationTitle, int id);
        void DeleteTheTitleOfProductSpecifications(int id);
        bool IsThereAnyProductInThisCategory(int id);
        bool IsThereAnySpecificationValuesInThisTitle(int id);
        IEnumerable<SlideCommentTitles> GetSlideCommentTitles(int id);
        void AddATitleToSlideCommentTitles(string evaluationTitle, int categoryId);
        //string GetSlideCommentTitle(int id);
        //void EditSlideCommentTitle(int id, string title);

        #endregion

        #region WebsiteCategories

        IEnumerable<WebSiteCategories> GetWebSiteCategories(int pageId);
        int GetCountOfWebsiteCategories();
        void AddCategory(AddWebsiteCategoryViewModel category);
        void Add(WebSiteCategories category);
        EditWebsiteCategoryViewModel GetWebsiteCategoryInfoForEdit(int id);
        void EditCategory(int id, EditWebsiteCategoryViewModel category, string currentPhoto);
        void Edit(WebSiteCategories category);
        WebSiteCategories GetWCategory(int id);
        void DeleteWebsiteCategory(int id);
        void ReWriteWebsiteCategory(int id);
        WebSiteCategories GetCategoryForSpecifications(int id);
        bool IsThereAnySliderInThisCategory(int categoryId);
        bool IsThereAnyBannersInThisCategory(int categoryId);
        bool IsThereAnyProductInThisWebsiteCategory(int id);

        #endregion

        #region WeblogCategories

        IEnumerable<WeblogCategories> GetAllWeblogCategories();
        void AddWeblogCategory(int id, string title);
        string GetWeblogCategoryTitle(int id);
        void EditWeblogCategory(int id, string title);
        void DeleteWeblogCategory(int id);
        bool IsThereAnySubCategoriesInThisCategory(int id);
        bool IsThereAnyWeblogsInThisCategory(int id);

        #endregion

        #region RepetitiveQuestionCategories

        IEnumerable<RepetitiveQuestionCategories> GetAllRQCategories(int pageId);
        int GetCountOfRepetitiveQuestionCategories();
        void AddQuestionCategory(AddCategoryViewModel category);
        EditCategoryViewModel GetQuestionInfoForEdit(int id);
        void EditQuestionCategories(int id, EditCategoryViewModel category, string currentImage);
        RepetitiveQuestionCategories GetRepetitiveQuestionCategory(int id);
        void DeleteQuestionCategory(int id);
        bool IsThereAnyQuestionInThisCategory(int categoryId);

        #endregion
    }
} 
