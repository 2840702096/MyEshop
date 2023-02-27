using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEshop.Core.Convertors;
using MyEshop.Core.DTOs;
using MyEshop.Core.Generators;
using MyEshop.Core.Security;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.SliderAndBaners;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class SliderServices : ISliderServices
    {
        private ShopContext _context;

        public SliderServices(ShopContext context)
        {
            _context = context;
        }

        public void ActivateSlider(int id, int WhichButton)
        {
            var Slider = _context.Sliders.SingleOrDefault(r => r.Id == id);

            if (WhichButton == 1)
            {
                Slider.IsActive = true;
                EditSlider(Slider);
            }
            else
            {
                Slider.IsActive = false;
                EditSlider(Slider);
            }
        }

        public void Add(Sliders sliders)
        {
            _context.Add(sliders);
            _context.SaveChanges();
        }

        public void AddHomePageSlider(AddSliderViewModel slider)
        {
            Sliders ProductSlider = new Sliders();

            ProductSlider.Title = slider.Title;
            ProductSlider.Link = slider.Link;
            ProductSlider.Tags = slider.Tags;
            ProductSlider.IsActive = false;
            ProductSlider.IsDelete = false;
            ProductSlider.CreateDate = DateTime.Now;

            if (slider.ImageName != null && slider.ImageName.IsImage())
            {
                string ImagePath = "";
                ProductSlider.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(slider.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageSliderImages/", ProductSlider.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    slider.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageSliderThumbNail/", ProductSlider.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 500);
            }

            Add(ProductSlider);
        }

        public void AddProductCategorySlider(AddSliderViewModel slider)
        {
            Sliders ProductSlider = new Sliders();

            ProductSlider.Title = slider.Title;
            ProductSlider.Link = slider.Link;
            ProductSlider.Tags = slider.Tags;
            ProductSlider.IsActive = false;
            ProductSlider.IsDelete = false;
            ProductSlider.ProductCategoryId = slider.CategoryId;
            ProductSlider.CreateDate = DateTime.Now;

            if (slider.ImageName != null && slider.ImageName.IsImage())
            {
                string ImagePath = "";
                ProductSlider.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(slider.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategorySliderImages/", ProductSlider.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    slider.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategorySliderThumbNail/", ProductSlider.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 500);
            }

            Add(ProductSlider);
        }

        public void AddWebsiteCategorySlider(AddSliderViewModel slider)
        {
            Sliders ProductSlider = new Sliders();

            ProductSlider.Title = slider.Title;
            ProductSlider.Link = slider.Link;
            ProductSlider.Tags = slider.Tags;
            ProductSlider.IsActive = false;
            ProductSlider.IsDelete = false;
            ProductSlider.WebsiteCategoryId = slider.CategoryId;
            ProductSlider.CreateDate = DateTime.Now;

            if (slider.ImageName != null && slider.ImageName.IsImage())
            {
                string ImagePath = "";
                ProductSlider.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(slider.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategorySliderImages/", ProductSlider.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    slider.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategorySliderThumbNail/", ProductSlider.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 500);
            }

            Add(ProductSlider);
        }

        public void DeleteSlider(int id)
        {
            IQueryable<Sliders> Result = _context.Sliders.IgnoreQueryFilters();

            var Slider = Result.SingleOrDefault(r => r.Id == id);

            Slider.IsDelete = true;
            EditSlider(Slider);
        }

        public void EditHomePageSlider(int id, EditSliderViewModel edit, string currentImage)
        {
            var Slider = GetSlider(id);

            Slider.Title = edit.Title;
            Slider.Link = edit.Link;
            Slider.Tags = edit.Tags;

            if (edit.ImageName != null && edit.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageSliderImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageSliderThumbNail/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Slider.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(edit.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageSliderImages/", Slider.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    edit.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageSliderThumbNail/", Slider.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 500);

            }
            else
            {
                Slider.ImageName = currentImage;
            }

            EditSlider(Slider);
        }

        public void EditProductCategorySlider(int id, EditSliderViewModel model, string currentImage)
        {
            var Slider = GetSlider(id);

            Slider.Title = model.Title;
            Slider.Link = model.Link;
            Slider.Tags = model.Tags;
            Slider.ProductCategoryId = model.CategoryId;

            if (model.ImageName != null && model.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategorySliderImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategorySliderThumbNail/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Slider.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(model.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategorySliderImages/", Slider.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    model.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategorySliderThumbNail/", Slider.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 500);

            }
            else
            {
                Slider.ImageName = currentImage;
            }

            EditSlider(Slider);
        }

        public void EditSlider(Sliders model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }

        public void EditWebsiteSlider(int id, EditSliderViewModel model, string currentImage)
        {
            var Slider = GetSlider(id);

            Slider.Title = model.Title;
            Slider.Link = model.Link;
            Slider.Tags = model.Tags;
            Slider.WebsiteCategoryId = model.CategoryId;

            if (model.ImageName != null && model.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategorySliderImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategorySliderThumbNail/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Slider.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(model.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategorySliderImages/", Slider.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    model.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategorySliderThumbNail/", Slider.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 500);

            }
            else
            {
                Slider.ImageName = currentImage;
            }

            EditSlider(Slider);
        }

        public IEnumerable<Sliders> FilterProductCategorySliders(string sliderTitle, int pageId)
        {
            int take = 15;
            int skip = (pageId - 1) * take;

            return _context.Sliders.Where(s => s.Title.Contains(sliderTitle) && s.ProductCategoryId != null).OrderByDescending(s => s.CreateDate).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<Sliders> FilterHomePageSliders(string sliderTitle, int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;
            return _context.Sliders.Where(s => s.Title.Contains(sliderTitle) && s.ProductCategoryId == null && s.WebsiteCategoryId == null).OrderByDescending(s => s.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public IEnumerable<Sliders> FilterWebsiteCategorySliders(string sliderTitle, int pageId)
        {
            int take = 15;
            int skip = (pageId - 1) * take;

            return _context.Sliders.Where(s => s.Title.Contains(sliderTitle) && s.WebsiteCategoryId != null).OrderByDescending(s => s.CreateDate).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<Sliders> GetAllSliders(int pageId)
        {
            int take = 15;
            int skip = (pageId - 1) * take;

            return _context.Sliders.Where(s => s.ProductCategoryId != null && s.WebsiteCategoryId == null).OrderByDescending(s => s.Id).Skip(skip).Take(take).ToList();
        }

        public List<SelectListItem> GetCategoriesForSelectingInSliders()
        {
            return _context.ProductCategories.Where(p => p.CategoryId != null && p.ParentId == null && p.SubParentId == null).Select(p => new SelectListItem
            {
                Text = p.CategoryTitle,
                Value = p.CategoryId.ToString()
            }).ToList();
        }

        public int GetCountOfFilteredCategorySliders(string sliderTitle)
        {
            return _context.Sliders.Where(s => s.Title.Contains(sliderTitle)).Count();
        }

        public int GetCountOfHomePageSliders()
        {
            return _context.Sliders.Where(s => s.ProductCategoryId == null && s.WebsiteCategoryId == null).Count();
        }

        public int GetCountOfWebsiteCategorySliders()
        {
            return _context.Sliders.Where(s => s.WebsiteCategoryId != null && s.ProductCategoryId == null).Count();
        }

        public EditSliderViewModel GetHomePageSliderInfoForEdit(int id)
        {
            return _context.Sliders.Where(s => s.Id == id).Select(s => new EditSliderViewModel
            {
                Title = s.Title,
                Tags = s.Tags,
                Link = s.Link,
                CurrentImage = s.ImageName
            }).Single();
        }

        public IEnumerable<Sliders> GetHomePageSliders(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;
            return _context.Sliders.Where(s => s.ProductCategoryId == null && s.WebsiteCategoryId == null).OrderByDescending(s => s.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public Sliders GetHomePageSliderSpecifications(int id)
        {
            return _context.Sliders.SingleOrDefault(s => s.Id == id);
        }

        public string GetProductCategoryTitle(int id)
        {
            return _context.ProductCategories.SingleOrDefault(p => p.CategoryId == id).CategoryTitle;
        }

        public int GetProductSlidersCount()
        {
            return _context.Sliders.Where(s => s.ProductCategoryId != null && s.WebsiteCategoryId == null).Count();
        }

        public Sliders GetSlider(int id)
        {
            return _context.Sliders.Find(id);
        }

        public EditSliderViewModel GetSliderInfoForEdit(int id)
        {
            return _context.Sliders.Where(s => s.Id == id).Select(s => new EditSliderViewModel
            {
                CategoryId = s.ProductCategoryId.Value,
                Title = s.Title,
                Tags = s.Tags,
                Link = s.Link,
                CurrentImage = s.ImageName,
            }).Single();
        }

        public Sliders GetSliderInfoForSpecifications(int id)
        {
            return _context.Sliders.SingleOrDefault(s => s.Id == id);
        }

        public List<SelectListItem> GetWebsiteCategoriesForSelecting()
        {
            return _context.WebSiteCategories.Select(w => new SelectListItem
            {
                Text = w.CategoryTitle,
                Value = w.CategoryId.ToString(),
            }).ToList();
        }

        public EditSliderViewModel GetWebsiteCategorySliderInfoForEdit(int id)
        {
            return _context.Sliders.Where(s => s.Id == id).Select(s => new EditSliderViewModel
            {
                CategoryId = s.WebsiteCategoryId.Value,
                Title = s.Title,
                Tags = s.Tags,
                Link = s.Link,
                CurrentImage = s.ImageName,
            }).Single();
        }

        public IEnumerable<Sliders> GetWebsiteCategorySliders(int pageId)
        {
            int take = 15;
            int skip = (pageId - 1) * take;
            return _context.Sliders.Where(s => s.WebsiteCategoryId != null && s.ProductCategoryId == null).OrderByDescending(s => s.CreateDate).Skip(skip).Take(take).ToList();
        }

        public string GetWebsiteCategoryTitle(int id)
        {
            return _context.WebSiteCategories.SingleOrDefault(w => w.CategoryId == id).CategoryTitle;
        }

        public string GetWebsiteCategoryTitleForSpecifications(int id)
        {
            return _context.WebSiteCategories.SingleOrDefault(w => w.CategoryId == id).CategoryTitle;
        }

        public int GetCountOfFilteredHomePageSlider()
        {
            return _context.Sliders.Where(s => s.ProductCategoryId == null && s.WebsiteCategoryId == null).Count();
        }
    }
}
