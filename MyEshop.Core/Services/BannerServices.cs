using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class BannerServices : IBannerervices
    {
        private ShopContext _context;

        public BannerServices(ShopContext context)
        {
            _context = context;
        }

        public void ActivateBanner(int id, int whichButton)
        {
            var Banner = GetBanner(id);

            if (whichButton == 1)
            {
                Banner.IsActive = true;
                EditBanner(Banner);
            }
            else
            {
                Banner.IsActive = false;
                EditBanner(Banner);
            }
        }

        public void AddBanner(Banners banner)
        {
            _context.Add(banner);
            _context.SaveChanges();
        }

        public void AddHomePageBanner(AddBannerViewModel banner)
        {
            Banners Banner = new Banners();
            Banner.BannerNumber = banner.BannerNumber;
            Banner.Tags = banner.Tags;
            Banner.Title = banner.Title;
            Banner.Link = banner.Link;
            Banner.CreateDate = DateTime.Now;
            Banner.IsActive = false;
            Banner.IsDelete = false;

            if (banner.Pictures != null && banner.Pictures.IsImage())
            {
                string ImagePath = "";
                Banner.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(banner.Pictures.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageBannerImages/", Banner.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    banner.Pictures.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageBannerThumbNail/", Banner.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 400);
            }
            AddBanner(Banner);
        }

        public void AddProductCategoryBanner(AddBannerViewModel banner)
        {
            Banners Banner = new Banners();
            Banner.BannerNumber = banner.BannerNumber;
            Banner.Tags = banner.Tags;
            Banner.Title = banner.Title;
            Banner.Link = banner.Link;
            Banner.CreateDate = DateTime.Now;
            Banner.IsActive = false;
            Banner.IsDelete = false;
            Banner.ProductCategoryId = banner.CategoryId;

            if (banner.Pictures != null && banner.Pictures.IsImage())
            {
                string ImagePath = "";
                Banner.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(banner.Pictures.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryBannerImages/", Banner.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    banner.Pictures.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryBannerThumbNail/", Banner.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 400);
            }
            AddBanner(Banner);
        }

        public void AddWebsiteCategoryBanner(AddBannerViewModel banner)
        {
            Banners Banner = new Banners();
            Banner.BannerNumber = banner.BannerNumber;
            Banner.Tags = banner.Tags;
            Banner.Title = banner.Title;
            Banner.Link = banner.Link;
            Banner.CreateDate = DateTime.Now;
            Banner.IsActive = false;
            Banner.IsDelete = false;
            Banner.WebsiteCategoryId = banner.CategoryId;

            if (banner.Pictures != null && banner.Pictures.IsImage())
            {
                string ImagePath = "";
                Banner.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(banner.Pictures.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryBannerImages/", Banner.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    banner.Pictures.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryBannerThumbNail/", Banner.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 400);
            }
            AddBanner(Banner);
        }

        public void DeleteBanner(int id)
        {
            var Banner = GetBanner(id);
            Banner.IsDelete = true;
            EditBanner(Banner);
        }

        public void EditBanner(Banners banner)
        {
            _context.Update(banner);
            _context.SaveChanges();
        }

        public void EditHomePageBanner(int id, EditBannerViewModel banner, string currentImage)
        {
            var Banner = GetBanner(id);

            Banner.Title = banner.Title;
            Banner.Link = banner.Link;
            Banner.Tags = banner.Tags;
            Banner.BannerNumber = banner.BannerNumber;

            if (banner.ImageName != null && banner.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageBannerImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageBannerThumbNail/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Banner.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(banner.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageBannerImages/", Banner.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    banner.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/HomePageBannerThumbNail/", Banner.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 400);

            }
            else
            {
                Banner.ImageName = currentImage;
            }

            EditBanner(Banner);
        }

        public void EditProductCategoryBanner(int id, EditBannerViewModel banner, string currentImage)
        {
            var Banner = GetBanner(id);

            Banner.Title = banner.Title;
            Banner.Link = banner.Link;
            Banner.Tags = banner.Tags;
            Banner.BannerNumber = banner.BannerNumber;
            Banner.ProductCategoryId = banner.CategoryId;

            if (banner.ImageName != null && banner.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryBannerImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryBannerThumbNail/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Banner.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(banner.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryBannerImages/", Banner.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    banner.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductCategoryBannerThumbNail/", Banner.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 400);

            }
            else
            {
                Banner.ImageName = currentImage;
            }

            EditBanner(Banner);
        }

        public void EditWebsiteCategoryBanner(int id, EditBannerViewModel banner, string currentImage)
        {
            var Banner = GetBanner(id);

            Banner.Title = banner.Title;
            Banner.Link = banner.Link;
            Banner.Tags = banner.Tags;
            Banner.BannerNumber = banner.BannerNumber;
            Banner.WebsiteCategoryId = banner.CategoryId;

            if (banner.ImageName != null && banner.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryBannerImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryBannerThumbNail/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Banner.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(banner.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryBannerImages/", Banner.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    banner.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WebsiteCategoryBannerThumbNail/", Banner.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 400);

            }
            else
            {
                Banner.ImageName = currentImage;
            }

            EditBanner(Banner);
        }

        public IEnumerable<Banners> FilterHomePageBanners(string bannerTitle, int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.Banners.Where(b => b.Title.Contains(bannerTitle) && b.ProductCategoryId == null && b.WebsiteCategoryId == null).OrderByDescending(b => b.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public IEnumerable<Banners> FilterProductCategoryBanner(string bannerTitle, int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.Banners.Where(b => b.Title.Contains(bannerTitle) && b.ProductCategoryId != null && b.WebsiteCategoryId == null).OrderByDescending(b => b.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public IEnumerable<Banners> FilterWebsiteCategoryBanner(string bannerTitle, int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.Banners.Where(b => b.Title.Contains(bannerTitle) && b.ProductCategoryId == null && b.WebsiteCategoryId != null).OrderByDescending(b => b.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public Banners GetBanner(int id)
        {
            return _context.Banners.Find(id);
        }

        public EditBannerViewModel GetBannerInfoForEdit(int id)
        {
            return _context.Banners.Where(b => b.Id == id).Select(b => new EditBannerViewModel
            {
                Title = b.Title,
                Tags = b.Tags,
                Link = b.Link,
                CurrentImage = b.ImageName,
                BannerNumber = b.BannerNumber,
            }).Single();
        }

        public int GetCountFilteredHomePageBanners(string bannerTitle)
        {
            return _context.Banners.Where(b => b.Title.Contains(bannerTitle) && b.ProductCategoryId == null && b.WebsiteCategoryId == null).Count();
        }

        public int GetCountOfFilteredProductCategoryBanners(string bannerTitle)
        {
            return _context.Banners.Where(b => b.Title.Contains(bannerTitle) && b.ProductCategoryId != null && b.WebsiteCategoryId == null).Count();
        }

        public int GetCountOfFilteredWebsiteCategoryBanners(string bannerTitle)
        {
            return _context.Banners.Where(b => b.Title.Contains(bannerTitle) && b.ProductCategoryId == null && b.WebsiteCategoryId != null).Count();
        }

        public int GetCountOfProductCategoryBanners()
        {
            return _context.Banners.Where(b => b.ProductCategoryId != null && b.WebsiteCategoryId == null).Count();
        }

        public int GetCountOfWebsiteCategoryBanners()
        {
            return _context.Banners.Where(b => b.ProductCategoryId == null && b.WebsiteCategoryId != null).Count();
        }

        public IEnumerable<Banners> GetHomePageBanners(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.Banners.Where(b => b.ProductCategoryId == null && b.WebsiteCategoryId == null).OrderByDescending(b => b.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public int GetHomePageBannersCount()
        {
            return _context.Banners.Where(b => b.ProductCategoryId == null && b.WebsiteCategoryId == null).Count();
        }

        public List<SelectListItem> GetProductCategoriesForSelecting()
        {
            return _context.ProductCategories.Where(p => p.CategoryId != null && p.ParentId == null && p.SubParentId == null).Select(p => new SelectListItem
            {
                Text = p.CategoryTitle,
                Value = p.CategoryId.ToString()
            }).ToList();
        }

        public EditBannerViewModel GetProductCategoryBannerInfoForEdit(int id)
        {
            return _context.Banners.Where(b => b.Id == id).Select(b => new EditBannerViewModel
            {
                Title = b.Title,
                Tags = b.Tags,
                Link = b.Link,
                CurrentImage = b.ImageName,
                BannerNumber = b.BannerNumber,
                CategoryId = b.ProductCategoryId.Value
            }).Single();
        }

        public IEnumerable<Banners> GetProductCategoryBanners(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.Banners.Where(b => b.ProductCategoryId != null && b.WebsiteCategoryId == null).OrderByDescending(b => b.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public string GetProductCategoryTitle(int id)
        {
            return _context.ProductCategories.SingleOrDefault(p => p.CategoryId == id).CategoryTitle;
        }

        public List<SelectListItem> GetWebsiteCategoriesForSelecting()
        {
            return _context.WebSiteCategories.Select(p => new SelectListItem
            {
                Text = p.CategoryTitle,
                Value = p.CategoryId.ToString()
            }).ToList();
        }

        public EditBannerViewModel GetWebsiteCategoryBannerInfoForEdit(int id)
        {
            return _context.Banners.Where(b => b.Id == id).Select(b => new EditBannerViewModel
            {
                Title = b.Title,
                Tags = b.Tags,
                Link = b.Link,
                CurrentImage = b.ImageName,
                BannerNumber = b.BannerNumber,
                CategoryId = b.WebsiteCategoryId.Value
            }).Single();
        }

        public IEnumerable<Banners> GetWebsiteCategoryBanners(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.Banners.Where(b => b.ProductCategoryId == null && b.WebsiteCategoryId != null).OrderByDescending(b => b.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public string GetWebsiteCategoryTitle(int categoryId)
        {
            return _context.WebSiteCategories.SingleOrDefault(w => w.CategoryId == categoryId).CategoryTitle;
        }

        public bool IsThereAnyBannerInThisNumber(int id, int bannerNumber)
        {
            if (_context.Banners.Where(b => b.ProductCategoryId != null && b.BannerNumber == bannerNumber && b.Id != id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnyHomePageBannerInThisNumber(int id, int bannerNumber)
        {
            if (_context.Banners.Where(b => b.Id != id && b.ProductCategoryId == null && b.WebsiteCategoryId == null && b.BannerNumber == bannerNumber).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnyWebsiteCategoryBannerInThisNumber(int id, int bannerNumber)
        {
            if (_context.Banners.Where(b => b.WebsiteCategoryId != null && b.BannerNumber == bannerNumber && b.Id != id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
