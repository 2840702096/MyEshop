using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Boxes;
using MyEshop.DataLayer.Entities.Products;
using MyEshop.DataLayer.Entities.SliderAndBaners;
using MyEShop.DataLayer.Context;
using MyEShop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class ClientServices : IClientServices
    {
        private ShopContext _context;
        public ClientServices(ShopContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductCategories> GetCategories()
        {
            return _context.ProductCategories.ToList();
        }

        public List<Banners> GetCorrespondingBanners(int id)
        {
            return _context.Banners.Where(b => b.ProductCategoryId == id && b.WebsiteCategoryId == null && b.IsActive == true).ToList();
        }

        public List<Sliders> GetCorrespondingSlider(int id)
        {

            return _context.Sliders.Where(s => s.ProductCategoryId == id && s.WebsiteCategoryId == null && s.IsActive == true).ToList();
        }

        public List<ProductColors> GetColorsOfProduct(int productId)
        {
            int take = 7;
            if (_context.ProductColors.Any(p => p.ProductId == productId))
            {
                return _context.ProductColors.Where(p => p.ProductId == productId).OrderByDescending(p => p.ProductColorId).Take(take).ToList();
            }
            else
            {
                return null;
            }
        }

        public string GetFirstImageForThisProduct(int productId)
        {
            return _context.ImageGallery.FirstOrDefault(i => i.ProductId == productId).ImageName;
        }

        public List<Banners> GetMainPageBanners()
        {
            return _context.Banners.Where(b => b.ProductCategoryId == null && b.WebsiteCategoryId == null && b.IsActive == true).ToList();
        }

        public List<Products> GetMainPageProducts()
        {
            int take = 8;
            return _context.Products.Where(p => p.IsActive == true).OrderByDescending(p => p.CreateDate).Take(take).ToList();
        }

        public List<Sliders> GetMainPageSliders()
        {
            return _context.Sliders.Where(p => p.ProductCategoryId == null && p.WebsiteCategoryId == null && p.IsActive == true).ToList();
        }

        public ProductCategories GetProductCategory(int categoryId)
        {
            var Category = _context.ProductCategories.SingleOrDefault(c => c.CategoryId == categoryId);
            return _context.ProductCategories.SingleOrDefault(c => c.CategoryId == Category.SubParentId);
        }

        public bool IsThereAnyImageForThisProductInGallery(int productId)
        {
            if (_context.ImageGallery.Where(i => i.ProductId == productId).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetColor(int ColorId)
        {
            return _context.ColorList.SingleOrDefault(p => p.ColorId == ColorId).ColorItSelf;
        }

        public string GetFirstImageOfThisProductFromGallery(int productId)
        {
            return _context.ImageGallery.FirstOrDefault(p => p.ProductId == productId).ImageName;
        }

        public List<ProductBrands> GetBrands()
        {
            return _context.ProductBrands.ToList();
        }

        public ProductCategories GetLastCategory()
        {
            return _context.ProductCategories.Where(p => p.ParentId == null && p.SubParentId == null).First();
        }

        public List<Products> GetProductForCorrespondingCategory(int categoryId)
        {
            int take = 5;
            return _context.Products.Where(p => p.ProductCategoryMainId == categoryId).OrderByDescending(p => p.CreateDate).Take(take).ToList();
        }

        public string GetBoxTitle(int id)
        {
            return _context.NamesOfBoxes.SingleOrDefault(n => n.Id == id).BoxTitle;
        }

        public List<Boxes> GetProductsOfThisBox(int id)
        {
            int take = 8;
            return _context.Boxes.Where(b => b.BoxTitleId == id).OrderByDescending(b => b.Id).Take(take).ToList();
        }

        public Products GetProducts(int id)
        {
            return _context.Products.Find(id);
        }

        public List<Products> GetLatestProductsAccordingToProductCategory(int productCategoryMainId)
        {
            int take = 8;
            return _context.Products.Where(p => p.ProductCategoryMainId == productCategoryMainId && p.IsActive == true).OrderByDescending(p => p.CreateDate).Take(take).ToList();
        }

        public List<Boxes> GetProductsOfThisBoxInThisCategory(int id, int categoryId)
        {
            int take = 8;
            return _context.Boxes.Where(b => b.BoxTitleId == id && b.CategoryId == categoryId).OrderByDescending(b => b.Id).Take(take).ToList();
        }

        public List<Products> GetLatestProductsOfThisCategory(int id)
        {
            int take = 8;
            return _context.Products.Where(p => p.ProductCategoryMainId == id).OrderByDescending(p => p.CreateDate).Take(take).ToList();
        }

        public IEnumerable<WebSiteCategories> GetWebsiteCategories()
        {
            return _context.WebSiteCategories.ToList();
        }

        public int GetCountOfSubProductCategories(int categoryId)
        {
            int Count = _context.ProductCategories.Where(p => p.ParentId == categoryId && p.SubParentId == null).Count();

            if (Count > 4)
            {
                return Count / 4;
            }
            else
            {
                return 1;
            }
        }

        public IEnumerable<ProductCategories> GetParentCategories(int categoryId, int countId)
        {
            int take = 4;
            int skip = (countId - 1) * take;
            return _context.ProductCategories.Where(p => p.ParentId == categoryId && p.SubParentId == null).OrderBy(p => p.CategoryId).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<ProductBrands> GetProductBrands()
        {
            return _context.ProductBrands.ToList();
        }

        public IEnumerable<ColorList> GetColors()
        {
            return _context.ColorList.ToList();
        }

        public string GetSubCategory(int categoryId)
        {
            return _context.ProductCategories.SingleOrDefault(c => c.CategoryId == categoryId).CategoryTitle;
        }

        public IEnumerable<Products> GetImazingProductsForMainPage(int imazingBoxId)
        {
            int take = 7;
            int skip = (imazingBoxId - 1) * 7;
            return _context.Products.Where(p => p.IsAmazing == true && p.IsActive == true).OrderByDescending(p => p.CreateDate).Skip(skip).Take(take).ToList();
        }

        public int GetBoxCount()
        {
            return _context.Boxes.Where(b => b.BoxTitleId == 3).Count();
        }

        public IEnumerable<ProductCategories> GetMainCategories()
        {
            return _context.ProductCategories.Where(p => p.ParentId == null && p.SubParentId == null).ToList(); ;
        }

        public int GetCountOfProductsInThisMainCategory(int categoryId)
        {
            return _context.Products.Where(p => p.ProductCategoryMainId == categoryId).Count();
        }

        public IEnumerable<Products> GetImazingProductsForProductCategory(int categoriaId)
        {
            int take = 8;
            return _context.Products.Where(p => p.ProductCategoryMainId == categoriaId && p.IsAmazing == true && p.IsActive == true).OrderByDescending(p => p.CreateDate).Take(take).ToList();
        }

        public ProductCategories GetMainProductCategory(int categoryId)
        {
            return _context.ProductCategories.Find(categoryId);
        }

        public IEnumerable<ProductCategories> GetSubCategoriesForThisMainCategory(int categoryId)
        {
            return _context.ProductCategories.Where(p => p.ParentId == categoryId && p.SubParentId == null).ToList();
        }

        public int GetCountOfProductInThisSubCategory(int categoryId)
        {
            return _context.Products.Where(p => p.ProductCategoryParentId == categoryId).Count();
        }

        public ProductCategories GetLastSubCategory(int categoryId)
        {
            return _context.ProductCategories.Where(p => p.ParentId == categoryId && p.SubParentId == null).OrderByDescending(p => p.CategoryId).SingleOrDefault();
        }

        public IEnumerable<Products> GetProductForCorrespondingSubCategory(int categoryId)
        {
            return _context.Products.Where(p => p.ProductCategoryParentId == categoryId && p.IsActive == true).OrderByDescending(p => p.CreateDate).ToList();
        }

        public WebSiteCategories GetLastWebsiteCategory()
        {
            return _context.WebSiteCategories.OrderByDescending(w => w.CategoryId).First();
        }

        public IEnumerable<Products> GetProductForCorrespondingWebsiteCategory(int categoryId)
        {
            return _context.Products.Where(p => p.WebsiteCategoryId == categoryId && p.IsActive == true).ToList();
        }

        public List<Products> GetProductForCorrespondingCategoryInThisProductCategoryPage(int WesbiteCategoryId, int productCategoryId)
        {
            return _context.Products.Where(p => p.WebsiteCategoryId == WesbiteCategoryId && p.ProductCategoryMainId == productCategoryId).ToList();
        }

        public List<Sliders> GetCorrespondingSliderForThisWebsiteCategory(int categoryId)
        {
            return _context.Sliders.Where(s => s.ProductCategoryId == null && s.WebsiteCategoryId == categoryId && s.IsActive == true).ToList();
        }

        public List<Banners> GetCorrespondingBannerForThisWebsiteCategory(int categoryId)
        {
            return _context.Banners.Where(b => b.ProductCategoryId == null && b.WebsiteCategoryId == categoryId && b.IsActive == true).ToList();
        }

        public List<Boxes> GetProductsOfThisBoxInThisWebsiteCategory(int id, int categoryId)
        {
            int take = 8;
            return _context.Boxes.Where(b => b.BoxTitleId == id && b.WebsiteCategoryId == categoryId).OrderByDescending(b => b.Id).Take(take).ToList();
        }

        public List<Products> GetProductsOfAmazingBoxInThisWebsiteCategory(int categoryId)
        {
            int take = 8;
            return _context.Products.Where(p => p.WebsiteCategoryId == categoryId && p.IsAmazing == true && p.IsActive == true).OrderByDescending(p => p.CreateDate).Take(take).ToList();
        }

        public List<Products> GetLatestProductsOfThisWebsiteCategory(int categoryId)
        {
            int take = 8;
            return _context.Products.Where(p => p.WebsiteCategoryId == categoryId && p.ProductCategoryId == null).OrderByDescending(p => p.CreateDate).Take(take).ToList();
        }

        public WebSiteCategories GetThisWebsiteCategory(int categoryId)
        {
            return _context.WebSiteCategories.Find(categoryId);
        }

        public int GetCountOfProductsInThisWebsiteCategory(int categoryId)
        {
            return _context.Products.Where(w => w.WebsiteCategoryId == categoryId).Count();
        }

        public IEnumerable<ProductsInSearchViewModel> SearchProducts(int pageId, int take, string filter, string getType, int startPrice, int endPrice, List<int> selectedProductCategories, List<int> selectedWebsiteCategories, List<int> selectedBrands, List<int> selectedColors)
        {
            IQueryable<Products> result = _context.Products;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(p => p.Title.Contains(filter) || p.EnglishTitle.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    break;

                case "theMostVisited":
                    {
                        result = result.OrderByDescending(p => p.CountOfVisit);
                        break;
                    }
                case "theNewest":
                    {
                        result = result.OrderByDescending(p => p.CreateDate);
                        break;
                    }
                case "theCheapest":
                    {
                        result = result.OrderBy(p => p.Price);
                        break;
                    }
                case "theMostExpensive":
                    {
                        result = result.OrderByDescending(p => p.Price);
                        break;
                    }
            }

            if (startPrice > 0)
            {
                result = result.Where(p => p.Price > startPrice);
            }

            if (endPrice > 0)
            {
                result = result.Where(p => p.Price < endPrice);
            }

            if (selectedProductCategories != null)
            {
                foreach (int Item in selectedProductCategories)
                {
                    result = result.Where(p => p.ProductCategoryId == Item || p.ProductCategoryParentId == Item || p.ProductCategoryMainId == Item);
                }
            }

            if (selectedWebsiteCategories != null)
            {
                foreach (int Item in selectedWebsiteCategories)
                {
                    result = result.Where(p => p.WebsiteCategoryId == Item);
                }
            }

            if (selectedBrands != null)
            {
                foreach (int Item in selectedBrands)
                {
                    result = result.Where(p => p.BrandId == Item);
                }
            }

            if (selectedColors != null)
            {
                foreach (int Item in selectedColors)
                {
                    List<ProductColors> Colors = _context.ProductColors.Where(p => p.ColorId == Item).ToList();
                    foreach (var Products in Colors)
                    {
                        result = result.Where(p => p.ProductId == Products.ProductId);
                    }
                }
            }

            int skip = (pageId - 1) * take;

            return result.Select(p => new ProductsInSearchViewModel
            {
                ProductId = p.ProductId,
                ProductCategoryId = p.ProductCategoryId,
                ProductCategoryMainId = p.ProductCategoryMainId,
                ProductCategoryParentId = p.ProductCategoryParentId,
                WebsiteCategoryId = p.WebsiteCategoryId,
                BrandId = p.BrandId,
                Score = p.Score,
                Title = p.Title,
                EnglishTitle = p.EnglishTitle,
                ImageName = p.ImageName,
                Tags = p.Tags,
                Discount = p.Discount,
                Price = p.Price,
                DiscountedPrice = p.DiscountedPrice,
                IsActive = p.IsActive,
                IsDelete = p.IsDelete,
                CreateDate = p.CreateDate,
                IsAmazing = p.IsAmazing,
                CountOfVisit = p.CountOfVisit.Value,
                ReturAble = p.ReturnAble
            }).Skip(skip).Take(take).ToList();
        }
    }
}
