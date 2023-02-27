using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.DataLayer.Entities.DiscountCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IDiscountServices
    {
        IEnumerable<DiscountCode> GetAllDiscountCodes(int pageId);
        int GetCountOfDiscountCodes();
        public void AddDiscountCode(DiscountCodesViewModel sliderViewModel, string StDate = "", string EdDate = "");
        DiscountCodesViewModel GetDiscountIfoForEdit(int id);
        void EditDiscountCode(int id, DiscountCodesViewModel discount, string stDate, string edDate);
        void DeleteDiscountCode(int id);
        IEnumerable<DiscountCode> GetAllDiscountCodesForProductCategories(int pageId);
        int GetCountOfDiscountCodesForProductCategories();
        List<SelectListItem> GetProductCategoriesForDiscount();
        string GetProductCategoryName(int id);
    }
}
