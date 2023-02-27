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
    public interface ISliderServices
    {
        #region ProductSliders

        IEnumerable<Sliders> GetAllSliders(int pageId);
        int GetProductSlidersCount();
        void AddProductCategorySlider(AddSliderViewModel slider);
        List<SelectListItem> GetCategoriesForSelectingInSliders();
        EditSliderViewModel GetSliderInfoForEdit(int id);
        void EditProductCategorySlider(int id, EditSliderViewModel model, string currentImage);
        string GetProductCategoryTitle(int id);
        IEnumerable<Sliders> FilterProductCategorySliders(string sliderTitle, int pageId);
        Sliders GetSliderInfoForSpecifications(int id);

        #endregion

        #region WebsiteCategorySliders

        IEnumerable<Sliders> GetWebsiteCategorySliders(int pageId);
        int GetCountOfWebsiteCategorySliders();
        string GetWebsiteCategoryTitle(int id);
        List<SelectListItem> GetWebsiteCategoriesForSelecting();
        void AddWebsiteCategorySlider(AddSliderViewModel slider);
        EditSliderViewModel GetWebsiteCategorySliderInfoForEdit(int id);
        void EditWebsiteSlider(int id, EditSliderViewModel model, string currentImage);
        string GetWebsiteCategoryTitleForSpecifications(int id);
        IEnumerable<Sliders> FilterWebsiteCategorySliders(string sliderTitle, int pageId);


        #endregion

        #region HomePageSliders

        IEnumerable<Sliders> GetHomePageSliders(int pageId);
        int GetCountOfHomePageSliders();
        void AddHomePageSlider(AddSliderViewModel slider);
        EditSliderViewModel GetHomePageSliderInfoForEdit(int id);
        void EditHomePageSlider(int id, EditSliderViewModel edit, string currentImage);
        Sliders GetHomePageSliderSpecifications(int id);
        IEnumerable<Sliders> FilterHomePageSliders(string sliderTitle, int pageId);
        int GetCountOfFilteredHomePageSlider();

        #endregion

        int GetCountOfFilteredCategorySliders(string sliderTitle);
        Sliders GetSlider(int id);
        void Add(Sliders sliders);
        void EditSlider(Sliders model);
        void DeleteSlider(int id);
        void ActivateSlider(int id,int WhichButton);


    }
}
