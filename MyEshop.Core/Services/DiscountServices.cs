using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.Core.Generators;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.DiscountCode;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class DiscountServices : IDiscountServices
    {
        private ShopContext _context;

        public DiscountServices(ShopContext context)
        {
            _context = context;
        }

        public void AddDiscountCode(DiscountCodesViewModel discountCode, string StDate = "", string EdDate = "")
        {
            DiscountCode NewDiscountCode = new DiscountCode();

            if (StDate != null)
            {
                string[] std = StDate.Split('/');
                NewDiscountCode.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );
            }

            if (EdDate != null)
            {
                string[] edd = EdDate.Split('/');
                NewDiscountCode.EndDate = new DateTime(int.Parse(edd[0]),
                    int.Parse(edd[1]),
                    int.Parse(edd[2]),
                    new PersianCalendar()
                    );
            }

            NewDiscountCode.Code = discountCode.Code;
            NewDiscountCode.UsableCount = discountCode.UsableCount;
            NewDiscountCode.DiscountPercent = int.Parse(discountCode.Percent);
            if(discountCode.ProductCategoryId != null)
            {
                NewDiscountCode.ProductCategoryId = int.Parse(discountCode.ProductCategoryId);
            }

            _context.DiscountCodes.Add(NewDiscountCode);
            _context.SaveChanges();
        }

        public void DeleteDiscountCode(int id)
        {
            _context.Entry(_context.DiscountCodes.Find(id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public void EditDiscountCode(int id, DiscountCodesViewModel discount, string stDate, string edDate)
        {
            var Discount = _context.DiscountCodes.Find(id);

            if (stDate != null)
            {
                string[] std = stDate.Split('/');
                Discount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );
            }

            if (edDate != null)
            {
                string[] edd = edDate.Split('/');
                Discount.EndDate = new DateTime(int.Parse(edd[0]),
                    int.Parse(edd[1]),
                    int.Parse(edd[2]),
                    new PersianCalendar()
                    );
            }

            Discount.Code = discount.Code;
            Discount.UsableCount = discount.UsableCount;
            Discount.DiscountPercent = int.Parse(discount.Percent);
            if(discount.ProductCategoryId != null)
            {
                Discount.ProductCategoryId = int.Parse(discount.ProductCategoryId);
            }

            _context.DiscountCodes.Update(Discount);
            _context.SaveChanges();
        }

        public IEnumerable<DiscountCode> GetAllDiscountCodes(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.DiscountCodes.Where(d => d.ProductCategoryId == null).OrderByDescending(d => d.DiscountId).Skip(Skip).Take(Take).ToList();
        }

        public IEnumerable<DiscountCode> GetAllDiscountCodesForProductCategories(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.DiscountCodes.Where(d => d.ProductCategoryId != null).OrderByDescending(d => d.DiscountId).Skip(Skip).Take(Take).ToList();
        }

        public int GetCountOfDiscountCodes()
        {
            return _context.DiscountCodes.Where(d => d.ProductCategoryId == null).Count();
        }

        public int GetCountOfDiscountCodesForProductCategories()
        {
            return _context.DiscountCodes.Where(d => d.ProductCategoryId != null).Count();
        }

        public DiscountCodesViewModel GetDiscountIfoForEdit(int id)
        {
            var Discount = _context.DiscountCodes.Find(id);

            return _context.DiscountCodes.Where(d => d.DiscountId == id).Select(d => new DiscountCodesViewModel
            {
                ProductCategoryId = d.ProductCategoryId.ToString(),
                Percent = d.DiscountPercent.ToString(),
                UsableCount = d.UsableCount,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                Code = d.Code
            }).Single();
        }

        public List<SelectListItem> GetProductCategoriesForDiscount()
        {
            return _context.ProductCategories.Where(p => p.ParentId == null && p.SubParentId == null).Select(p => new SelectListItem
            {
                Text = p.CategoryTitle,
                Value = p.CategoryId.ToString()
            }).ToList();
        }

        public string GetProductCategoryName(int id)
        {
            return _context.ProductCategories.SingleOrDefault(p => p.CategoryId == id).CategoryTitle;
        }
    }
}
