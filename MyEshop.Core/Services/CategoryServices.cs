using MyEShop.DataLayer.Entities.Categories;
using MyEShop.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEshop.Core.DTOs;
using MyEshop.Core.Generators;
using System.IO;
using MyEshop.Core.Convertors;
using MyEshop.Core.Security;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Categories;
using MyEshop.DataLayer.Entities.Products;

namespace MyEshop.Core.Services
{
    public class CategoryServices : ICategoryServices
    {
        private ShopContext _context;

        public CategoryServices(ShopContext context)
        {
            _context = context;
        }

        public void Add(WebSiteCategories category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public void AddCategory(AddCategoryViewModel category)
        {
            ProductCategories ProductCategories = new ProductCategories();

            ProductCategories.CategoryTitle = category.CategoryTitle;
            ProductCategories.Color = category.Color;
            ProductCategories.IsDelete = false;

            if (category.ImageName != null && category.ImageName.IsImage())
            {
                string ImagePath = "";
                ProductCategories.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(category.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryImages/", ProductCategories.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    category.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryThumbnail/", ProductCategories.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);
            }

            _context.ProductCategories.Add(ProductCategories);
            _context.SaveChanges();

        }

        public void AddCategory(AddWebsiteCategoryViewModel category)
        {
            WebSiteCategories WCategory = new WebSiteCategories();

            WCategory.CategoryTitle = category.CategoryTitle;
            WCategory.Color = category.Color;
            if (category.ImageName != null && category.ImageName.IsImage())
            {
                string ImagePath = "";
                WCategory.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(category.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryImages/", WCategory.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    category.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryThumbnail/", WCategory.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);
            }

            Add(WCategory);
        }

        public void AddSubCategory(int id, AddCategoryViewModel subCategory)
        {
            ProductCategories SubCategories = new ProductCategories();

            SubCategories.CategoryTitle = subCategory.CategoryTitle;
            SubCategories.ParentId = id;

            if (subCategory.ImageName != null && subCategory.ImageName.IsImage())
            {
                string ImagePath = "";
                SubCategories.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(subCategory.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryImages/", SubCategories.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    subCategory.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryThumbnail/", SubCategories.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);
            }

            _context.ProductCategories.Add(SubCategories);
            _context.SaveChanges();
        }

        public void AddSubSet(int id, string categoryTitle)
        {
            int Sub = _context.ProductCategories.SingleOrDefault(p=>p.CategoryId == id).ParentId.Value;

            ProductCategories SubSet = new ProductCategories();

            SubSet.CategoryTitle = categoryTitle;
            SubSet.SubParentId = id;
            SubSet.ParentId = Sub;

            _context.ProductCategories.Add(SubSet);
            _context.SaveChanges();
        }


        public void DeleteMainGroup(int id)
        {
            var Category = _context.ProductCategories.SingleOrDefault(p => p.CategoryId == id);

            Category.IsDelete = true;
            _context.ProductCategories.Update(Category);
            _context.SaveChanges();
        }

        public void DeleteWebsiteCategory(int id)
        {
            var Category = GetWCategory(id);

            Category.IsDelete = true;

            _context.WebSiteCategories.Update(Category);
            _context.SaveChanges();
        }

        public void Edit(WebSiteCategories category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }

        public void EditCategory(int id, EditWebsiteCategoryViewModel category, string currentPhoto)
        {
            var WCategory = GetWCategory(id);

            WCategory.CategoryTitle = category.CategoryTitle;
            WCategory.Color = category.Color;

            if (category.NextImage != null && category.NextImage.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentPhoto != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryImages/", currentPhoto);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryThumbnail/", currentPhoto);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                WCategory.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(category.NextImage.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryImages/", WCategory.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    category.NextImage.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryThumbnail/", WCategory.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);

            }
            else
            {
                WCategory.ImageName = currentPhoto;
            }

            Edit(WCategory);
        }

        public void EditMainGroup(int id, EditCategoryViewModel edit, string CurrentPhoto)
        {
            var Category = _context.ProductCategories.SingleOrDefault(p => p.CategoryId == id);

            Category.CategoryTitle = edit.CategoryTitle;
            Category.Color = edit.Color;

            if (edit.NextImage != null && edit.NextImage.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (CurrentPhoto != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryImages/", edit.CurrentPhoto);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryThumbnail/", edit.CurrentPhoto);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Category.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(edit.NextImage.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryImages/", Category.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    edit.NextImage.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryThumbnail/", Category.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);

            }
            else
            {
                Category.ImageName = CurrentPhoto;
            }
            _context.ProductCategories.Update(Category);
            _context.SaveChanges();
        }

        public void EditSubCategory(int id, EditCategoryViewModel edit, string currntPhoto)
        {
            var SubCategory = GetCategory(id);

            SubCategory.CategoryTitle = edit.CategoryTitle;
            if (edit.NextImage != null && edit.NextImage.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currntPhoto != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryImages/", edit.CurrentPhoto);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryThumbnail/", edit.CurrentPhoto);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                SubCategory.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(edit.NextImage.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryImages/", SubCategory.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    edit.NextImage.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryThumbnail/", SubCategory.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);
            }
            else
            {
                SubCategory.ImageName = currntPhoto;
            }

            _context.ProductCategories.Update(SubCategory);
            _context.SaveChanges();
        }

        public void EditSubSet(int id, EditCategoryViewModel edit)
        {
            var SubSet = GetCategory(id);

            SubSet.CategoryTitle = edit.CategoryTitle;

            _context.ProductCategories.Update(SubSet);
            _context.SaveChanges();
        }

        public List<ProductCategories> GetAllCategories()
        {
            return _context.ProductCategories.Include(p => p.Categories).ToList();
        }

        public ProductCategories GetCategory(int id)
        {
            return _context.ProductCategories.SingleOrDefault(p => p.CategoryId == id);
        }

        public int GetCategoryCount()
        {
            return _context.ProductCategories.Where(p => p.ParentId == null && p.SubParentId == null).Count();
        }

        public EditCategoryViewModel GetCategoryInfoForEdit(int categoryId)
        {
            return _context.ProductCategories.Where(p => p.CategoryId == categoryId).Select(p => new EditCategoryViewModel
            {
                CategoryTitle = p.CategoryTitle,
                Color = p.Color,
                CurrentPhoto = p.ImageName
            }).Single();
        }

        public int GetCountOfWebsiteCategories()
        {
            return _context.WebSiteCategories.Count();
        }

        public List<ProductCategories> GetThirSubCategories(int categoryId)
        {
            return _context.ProductCategories.Where(p => p.SubParentId != null && p.SubParentId == categoryId).ToList();
        }

        public WebSiteCategories GetWCategory(int id)
        {
            return _context.WebSiteCategories.SingleOrDefault(w => w.CategoryId == id);
        }

        public IEnumerable<WebSiteCategories> GetWebSiteCategories(int pageId)
        {
            int take = 15;
            int skip = (pageId - 1) * take;

            return _context.WebSiteCategories.OrderByDescending(w => w.CategoryId).Skip(skip).Take(take).ToList();
        }

        public EditWebsiteCategoryViewModel GetWebsiteCategoryInfoForEdit(int id)
        {
            return _context.WebSiteCategories.Where(w => w.CategoryId == id).Select(w => new EditWebsiteCategoryViewModel
            {
                CategoryTitle = w.CategoryTitle,
                Color = w.Color,
                CurrentImage = w.ImageName
            }).Single();

        }

        public bool IsSubCategoryExist(int categoryId)
        {
            if (_context.ProductCategories.Any(p => p.ParentId == categoryId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThirdSubCategoryExist(int categoryId)
        {
            if (_context.ProductCategories.Any(p => p.SubParentId == categoryId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReWriteCategory(int id)
        {
            IQueryable<ProductCategories> Categories = _context.ProductCategories.IgnoreQueryFilters();

            var Category = Categories.SingleOrDefault(p => p.CategoryId == id);

            Category.IsDelete = false;
            _context.ProductCategories.Update(Category);
            _context.SaveChanges();
        }

        public void ReWriteWebsiteCategory(int id)
        {
            IQueryable<WebSiteCategories> Result = _context.WebSiteCategories.IgnoreQueryFilters();

            var category = Result.SingleOrDefault(p => p.CategoryId == id);

            category.IsDelete = false;

            _context.WebSiteCategories.Update(category);
            _context.SaveChanges();
        }

        public ProductCategories GetSpcificationsOfCategories(int categoryId)
        {
            return _context.ProductCategories.SingleOrDefault(p => p.CategoryId == categoryId);
        }

        public WebSiteCategories GetCategoryForSpecifications(int id)
        {
            return _context.WebSiteCategories.SingleOrDefault(w => w.CategoryId == id);
        }

        public int GetCountOfFilteredCategories(string categoryTitle)
        {
            return _context.ProductCategories.Where(p => p.CategoryTitle.Contains(categoryTitle)).Count();
        }

        public bool IsThereAnySliderInThisSubset(int categoryId)
        {
            if (_context.Sliders.Where(s => s.ProductCategoryId == categoryId && s.IsDelete == false).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnyBannersInThisSubset(int categoryId)
        {
            if (_context.Banners.Where(s => s.ProductCategoryId == categoryId && s.IsDelete == false).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnySliderInThisCategory(int categoryId)
        {
            if (_context.Sliders.Where(s => s.WebsiteCategoryId == categoryId && s.IsDelete == false).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnyBannersInThisCategory(int categoryId)
        {
            if (_context.Banners.Where(s => s.WebsiteCategoryId == categoryId && s.IsDelete == false).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<WeblogCategories> GetAllWeblogCategories()
        {
            return _context.WeblogCategories.Include(w => w.Categories).ToList();
        }

        public void AddWeblogCategory(int id, string title)
        {
            WeblogCategories weblogCategories = new WeblogCategories();

            weblogCategories.Title = title;
            weblogCategories.IsDelete = false;
            if (id != 0)
            {
                weblogCategories.ParentId = id;
            }

            _context.WeblogCategories.Add(weblogCategories);
            _context.SaveChanges();
        }

        public string GetWeblogCategoryTitle(int id)
        {
            return _context.WeblogCategories.SingleOrDefault(w => w.CategoryId == id).Title;
        }

        public void EditWeblogCategory(int id, string title)
        {
            var Category = _context.WeblogCategories.Find(id);

            Category.Title = title;
            _context.WeblogCategories.Update(Category);
            _context.SaveChanges();
        }

        public void DeleteWeblogCategory(int id)
        {
            var Category = _context.WeblogCategories.Find(id);

            Category.IsDelete = true;

            _context.WeblogCategories.Update(Category);
            _context.SaveChanges();
        }

        public bool IsThereAnySubCategoriesInThisCategory(int id)
        {
            if (_context.WeblogCategories.Where(w => w.ParentId == id).Any())
                return true;

            else
                return false;
        }

        public bool IsThereAnyWeblogsInThisCategory(int id)
        {
            if (_context.Weblogs.Where(w => w.CategoryId == id).Any())
                return true;

            else
                return false;
        }

        public IEnumerable<SpecificationsInCategories> GetSpecificationsInCategories(int categoryId)
        {
            return _context.SpecificationsInCategories.Where(w => w.CategoryId == categoryId).ToList();
        }

        public void AddTheTitleOfProductSpecifications(string specificationTitle, int id)
        {
            SpecificationsInCategories specifications = new SpecificationsInCategories();

            specifications.CategoryId = id;
            specifications.SpecificationTitle = specificationTitle;
            _context.SpecificationsInCategories.Add(specifications);
            _context.SaveChanges();
        }

        public void DeleteTheTitleOfProductSpecifications(int id)
        {
            _context.Entry(_context.SpecificationsInCategories.Find(id)).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public bool IsThereAnyProductInThisCategory(int id)
        {
            if (_context.Products.Where(p => p.ProductCategoryId == id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnyProductInThisWebsiteCategory(int id)
        {
            if (_context.Products.Where(p => p.WebsiteCategoryId == id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<RepetitiveQuestionCategories> GetAllRQCategories(int pageId)
        {
            int Take = 7;
            int Skip = (pageId - 1) * Take;

            return _context.RepetitiveQuestionCategories.OrderByDescending(r => r.CategoryId).Skip(Skip).Take(Take).ToList();
        }

        public int GetCountOfRepetitiveQuestionCategories()
        {
            return _context.RepetitiveQuestionCategories.Count();
        }

        public void AddQuestionCategory(AddCategoryViewModel category)
        {
            RepetitiveQuestionCategories repetitiveQuestionCategories = new RepetitiveQuestionCategories();

            repetitiveQuestionCategories.CategoryTitle = category.CategoryTitle;

            if (category.ImageName != null && category.ImageName.IsImage())
            {
                string ImagePath = "";
                repetitiveQuestionCategories.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(category.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/RepetitiveQuestionCategoryImages/", repetitiveQuestionCategories.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    category.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/RepetitiveQuestionCategoryThumbnail/", repetitiveQuestionCategories.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);

                _context.RepetitiveQuestionCategories.Add(repetitiveQuestionCategories);
                _context.SaveChanges();
            }
        }

        public EditCategoryViewModel GetQuestionInfoForEdit(int id)
        {
            return _context.RepetitiveQuestionCategories.Where(c => c.CategoryId == id).Select(d => new EditCategoryViewModel
            {
                CategoryTitle = d.CategoryTitle,
                CurrentPhoto = d.ImageName
            }).Single();
        }

        public void EditQuestionCategories(int id, EditCategoryViewModel category, string currentImage)
        {
            var Category = GetRepetitiveQuestionCategory(id);

            Category.CategoryTitle = category.CategoryTitle;

            if (category.NextImage != null && category.NextImage.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/RepetitiveQuestionCategoryImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/RepetitiveQuestionCategoryThumbnail/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Category.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(category.NextImage.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/RepetitiveQuestionCategoryImages/", Category.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    category.NextImage.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/RepetitiveQuestionCategoryThumbnail/", Category.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);

            }
            else
            {
                Category.ImageName = currentImage;
            }
            _context.RepetitiveQuestionCategories.Update(Category);
            _context.SaveChanges();
        }

        public RepetitiveQuestionCategories GetRepetitiveQuestionCategory(int id)
        {
            return _context.RepetitiveQuestionCategories.Find(id);
        }

        public void DeleteQuestionCategory(int id)
        {
            var Category = GetRepetitiveQuestionCategory(id);

            Category.IsDelete = true;

            _context.RepetitiveQuestionCategories.Update(Category);
            _context.SaveChanges();
        }

        public bool IsThereAnyQuestionInThisCategory(int categoryId)
        {
            if (_context.RepetitiveQuestions.Where(r => r.CategoryId == categoryId).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnySpecificationValuesInThisTitle(int id)
        {
            if (_context.ProductSpecifications.Where(p => p.CSId == id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<SlideCommentTitles> GetSlideCommentTitles(int id)
        {
            return _context.SlideCommentTitles.Where(s => s.ProductMainCategoryId == id).ToList();
        }

        public void AddATitleToSlideCommentTitles(string evaluationTitle, int categoryId)
        {
            SlideCommentTitles Titles = new SlideCommentTitles();
            Titles.SlideCommentTitle = evaluationTitle;
            Titles.ProductMainCategoryId = categoryId;

            _context.Add(Titles);
            _context.SaveChanges();
        }

        //public string GetSlideCommentTitle(int id)
        //{
        //    return _context.SlideCommentTitles.Find(id).SlideCommentTitle;
        //}

        //public void EditSlideCommentTitle(int id, string title)
        //{
        //    var SlideCommentTitle = _context.SlideCommentTitles.Find(id);

        //    SlideCommentTitle.SlideCommentTitle = title;

        //    _context.SlideCommentTitles.Update(SlideCommentTitle);
        //    _context.SaveChanges();
        //}
    }
}
