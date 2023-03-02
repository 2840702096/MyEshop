using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.Convertors;
using MyEshop.Core.DTOs;
using MyEshop.Core.Generators;
using MyEshop.Core.Security;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Boxes;
using MyEshop.DataLayer.Entities.Products;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class ProductServices : IProductServices
    {
        private ShopContext _context;

        public ProductServices(ShopContext context)
        {
            _context = context;
        }

        public void AddBrand(AddBrandViewModel brand)
        {
            ProductBrands ProductBrand = new ProductBrands();
            ProductBrand.Title = brand.BrandTitle;
            ProductBrand.IsDelete = false;

            if (brand.ImageName != null && brand.ImageName.IsImage())
            {
                string ImagePath = "";
                ProductBrand.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(brand.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/BrandImages/", ProductBrand.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    brand.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/BrandThumbnails/", ProductBrand.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);
            }
            _context.ProductBrands.Add(ProductBrand);
            _context.SaveChanges();
        }

        public void DeleteBrand(int id)
        {
            var Brand = _context.ProductBrands.Find(id);

            Brand.IsDelete = true;

            _context.ProductBrands.Update(Brand);
            _context.SaveChanges();
        }

        public void EditBrand(int id, EditBrandViewModel brand, string currentPhoto)
        {
            var ProductBrand = _context.ProductBrands.SingleOrDefault(p => p.BrandId == id);
            ProductBrand.Title = brand.BrandTitle;

            if (brand.ImageName != null && brand.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentPhoto != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/BrandImages/", currentPhoto);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/BrandThumbnails/", currentPhoto);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                ProductBrand.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(brand.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/BrandImages/", ProductBrand.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    brand.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/BrandThumbnails/", ProductBrand.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);

            }
            else
            {
                ProductBrand.ImageName = currentPhoto;
            }

            _context.ProductBrands.Update(ProductBrand);
            _context.SaveChanges();
        }

        public IEnumerable<ProductBrands> FilterBrands(string brandTitle, int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.ProductBrands.Where(p => p.Title.Contains(brandTitle)).OrderByDescending(p => p.BrandId).Skip(Skip).Take(Take).ToList();
        }

        public string GetBrandTitle(int id)
        {
            return _context.ProductBrands.SingleOrDefault(p => p.BrandId == id).Title;
        }

        public EditBrandViewModel GetBrandInfoForEdit(int id)
        {
            return _context.ProductBrands.Where(p => p.BrandId == id).Select(p => new EditBrandViewModel
            {
                BrandTitle = p.Title,
                CurrentImage = p.ImageName
            }).Single();
        }

        public IEnumerable<ProductBrands> GetBrands(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.ProductBrands.OrderByDescending(p => p.BrandId).Skip(Skip).Take(Take).ToList();
        }

        public int GetCountOfFilteredBrands(string brandTitle)
        {
            return _context.ProductBrands.Where(p => p.Title.Contains(brandTitle)).Count();
        }

        public int GetCountOfProductBrands()
        {
            return _context.ProductBrands.Count();
        }

        public int GetCountOfProducts()
        {
            return _context.Products.Count();
        }

        public Products GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public string GetProductCategoryTitle(int id)
        {
            return _context.ProductCategories.SingleOrDefault(p => p.CategoryId == id).CategoryTitle;
        }

        public IEnumerable<Products> GetProducts(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.Products.OrderByDescending(p => p.CreateDate).Skip(Skip).Take(Take).ToList();
        }

        public string GetWebsiteCategoryTitle(int id)
        {
            return _context.WebSiteCategories.SingleOrDefault(p => p.CategoryId == id).CategoryTitle;
        }

        public bool IsThereAProductInThisBrand(int id)
        {
            if (_context.Products.Where(p => p.BrandId == id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<SelectListItem> GetBrands()
        {
            return _context.ProductBrands.Select(p => new SelectListItem
            {
                Text = p.Title,
                Value = p.BrandId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetProductCategories()
        {
            return _context.ProductCategories.Where(p => p.SubParentId != null).Select(p => new SelectListItem
            {
                Text = p.CategoryTitle,
                Value = p.CategoryId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetWebsiteCategories()
        {
            return _context.WebSiteCategories.Select(p => new SelectListItem
            {
                Text = p.CategoryTitle,
                Value = p.CategoryId.ToString()
            }).ToList();
        }

        public void AddProduct(AddProductViewModel product)
        {
            var categoryId = _context.ProductCategories.Find(product.ProductCategoryId);

            Products NewProduct = new Products();
            NewProduct.Title = product.Title;
            NewProduct.Score = product.Score.Value;
            NewProduct.EnglishTitle = product.EnglishTitle;
            NewProduct.BrandId = product.BrandId;
            NewProduct.ProductCategoryId = product.ProductCategoryId;
            NewProduct.ProductCategoryParentId = categoryId.SubParentId.Value;
            NewProduct.ProductCategoryMainId = categoryId.ParentId.Value;
            NewProduct.WebsiteCategoryId = product.WebsiteCategoryId;
            NewProduct.Price = product.Price.Value;
            NewProduct.Tags = product.Tags;
            NewProduct.Discount = product.Discount.Value;
            NewProduct.IsActive = product.IsActive;
            NewProduct.ReturnAble = product.ReturnAble;
            NewProduct.IsAmazing = product.IsAmazing;
            NewProduct.IsDelete = false;
            NewProduct.CreateDate = DateTime.Now;
            NewProduct.CountOfVisit = 0;
            NewProduct.Discription = product.Description;
            if (product.Discount != 0)
            {
                int DiscountAmount = product.Price.Value * product.Discount.Value / 100;
                int TheDiscountedPrice = product.Price.Value - DiscountAmount;

                NewProduct.DiscountedPrice = TheDiscountedPrice;
            }
            else
            {
                NewProduct.DiscountedPrice = 0;
            }

            if (product.ImageName != null && product.ImageName.IsImage())
            {
                string ImagePath = "";
                NewProduct.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(product.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductImages/", NewProduct.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    product.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductThumbnails/", NewProduct.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);
            }

            _context.Products.Add(NewProduct);
            _context.SaveChanges();

        }

        public EditProductViewModel GetProductInfoForEdit(int id)
        {
            return _context.Products.Where(p => p.ProductId == id).Select(p => new EditProductViewModel
            {
                Title = p.Title,
                EnglishTitle = p.EnglishTitle,
                Score = p.Score,
                Tags = p.Tags,
                CurrentImage = p.ImageName,
                IsActive = p.IsActive,
                ReturnAble = p.ReturnAble,
                IsAmazing = p.IsAmazing,
                BrandId = p.BrandId,
                ProductCategoryId = p.ProductCategoryId,
                WebsiteCategoryId = p.WebsiteCategoryId,
                Discount = p.Discount,
                Price = p.Price,
                Description = p.Discription
            }).Single();
        }

        public void EditProduct(int id, EditProductViewModel product, string currentImage)
        {
            var categoryId = _context.ProductCategories.Find(product.ProductCategoryId);

            var CurrentProduct = _context.Products.Find(id);

            CurrentProduct.Title = product.Title;
            CurrentProduct.Score = product.Score;
            CurrentProduct.EnglishTitle = product.EnglishTitle;
            CurrentProduct.BrandId = product.BrandId;
            CurrentProduct.ProductCategoryId = product.ProductCategoryId;
            CurrentProduct.ProductCategoryParentId = categoryId.SubParentId.Value;
            CurrentProduct.ProductCategoryMainId = categoryId.ParentId.Value;
            CurrentProduct.WebsiteCategoryId = product.WebsiteCategoryId;
            CurrentProduct.Price = product.Price;
            CurrentProduct.Tags = product.Tags;
            CurrentProduct.Tags = product.Tags;
            CurrentProduct.Discount = product.Discount;
            CurrentProduct.IsActive = product.IsActive;
            CurrentProduct.ReturnAble = product.ReturnAble;
            CurrentProduct.IsAmazing = product.IsAmazing;
            CurrentProduct.CountOfVisit = 0;
            CurrentProduct.Discription = product.Description;
            if (product.Discount != 0)
            {
                int DiscountAmount = product.Price * product.Discount / 100;
                int TheDiscountedPrice = product.Price - DiscountAmount;

                CurrentProduct.DiscountedPrice = TheDiscountedPrice;
            }
            else
            {
                CurrentProduct.DiscountedPrice = 0;
            }

            if (product.ImageName != null && product.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductThumbnails/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                CurrentProduct.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(product.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductImages/", CurrentProduct.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    product.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductThumbnails/", CurrentProduct.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 300);

            }
            else
            {
                CurrentProduct.ImageName = currentImage;
            }
            _context.Products.Update(CurrentProduct);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var Product = _context.Products.Find(id);

            Product.IsDelete = true;

            _context.Products.Update(Product);
            _context.SaveChanges();
        }

        public IEnumerable<Products> FilterProductList(string productTitle)
        {
            return _context.Products.Where(p => p.Title.Contains(productTitle) || p.EnglishTitle.Contains(productTitle)).ToList();
        }

        public Products GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public List<SelectListItem> GetCategorySpecifications(int categoryId)
        {
            var Category = _context.ProductCategories.Find(categoryId);

            var SubCategory = _context.ProductCategories.Find(Category.SubParentId);

            return _context.SpecificationsInCategories.Where(s => s.CategoryId == SubCategory.ParentId).Select(s => new SelectListItem
            {
                Text = s.SpecificationTitle,
                Value = s.Id.ToString()
            }).ToList();
        }

        public IEnumerable<ProductSpecifications> GetProductSpecifications(int productId)
        {
            return _context.ProductSpecifications.Where(p => p.ProductId == productId).ToList();
        }

        public string GetCategorySpecificationTitle(int id)
        {
            return _context.SpecificationsInCategories.SingleOrDefault(s => s.Id == id).SpecificationTitle;
        }

        public void AddProductSpecification(int productId, int categoryId, string value)
        {
            ProductSpecifications NewProductSpecifications = new ProductSpecifications();

            NewProductSpecifications.ProductId = productId;
            NewProductSpecifications.CSId = categoryId;
            NewProductSpecifications.Value = value;

            _context.ProductSpecifications.Add(NewProductSpecifications);
            _context.SaveChanges();
        }

        public void DeleteProductSpecification(int id)
        {
            _context.Entry(_context.ProductSpecifications.Find(id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public int GetProductCategoryId(int id)
        {
            return _context.Products.SingleOrDefault(s => s.ProductId == id).ProductCategoryId;
        }

        public IEnumerable<ColorList> GetColorsForColorList()
        {
            return _context.ColorList.ToList();
        }

        public int GetCountOfColorsInColorList()
        {
            return _context.ColorList.Count();
        }

        public void AddColorToColorList(string colorTitle, string colorItSelf)
        {
            ColorList color = new ColorList();

            color.ColorTitle = colorTitle;
            color.ColorItSelf = colorItSelf;

            _context.ColorList.Add(color);
            _context.SaveChanges();
        }

        public void DeleteColorFromColorList(int id)
        {
            var Color = _context.ColorList.Find(id);

            Color.IsDelete = true;

            _context.ColorList.Update(Color);
            _context.SaveChanges();
        }

        public List<SelectListItem> GetColorsOfColorList()
        {
            return _context.ColorList.Select(c => new SelectListItem
            {
                Text = c.ColorTitle,
                Value = c.ColorId.ToString()
            }).ToList();
        }

        public IEnumerable<ProductColors> GetProductColors(int productId)
        {
            return _context.ProductColors.Where(p => p.ProductId == productId).ToList();
        }

        public ColorList GetColorList(int colorId)
        {
            return _context.ColorList.Find(colorId);
        }

        public void AddProductColor(int productId, ProductColorViewModel color)
        {
            ProductColors NewProductColors = new ProductColors();

            NewProductColors.ColorId = color.ColorId;
            NewProductColors.ProductId = productId;
            NewProductColors.Price = int.Parse(color.Price);
            NewProductColors.Discount = int.Parse(color.Discount);
            NewProductColors.Quantity = int.Parse(color.Quantity);
            NewProductColors.IsDelete = false;
            if (int.Parse(color.Discount) != 0)
            {
                int DiscountAmount = int.Parse(color.Price) * int.Parse(color.Discount) / 100;
                int TheDiscountedPrice = int.Parse(color.Price) - DiscountAmount;

                NewProductColors.DiscountedPrice = TheDiscountedPrice;
            }
            _context.ProductColors.Add(NewProductColors);
            _context.SaveChanges();
        }

        public void DeleteProductColor(int id)
        {
            var Color = _context.ProductColors.Find(id);

            Color.IsDelete = true;
            _context.ProductColors.Update(Color);
            _context.SaveChanges();
        }

        public IEnumerable<ProductProperties> GetProdoductProperties(int productId)
        {
            return _context.ProductProperties.Where(p => p.ProductId == productId).ToList();
        }

        public void AddProductProperty(ProductPropertiesViewModel property, int productId)
        {
            ProductProperties NewProperty = new ProductProperties();

            NewProperty.ProductTitle = property.Title;
            NewProperty.Value = property.Value;
            NewProperty.ProductId = productId;

            _context.ProductProperties.Add(NewProperty);
            _context.SaveChanges();
        }

        public void DeleteProductProperty(int id)
        {
            _context.Entry(_context.ProductProperties.Find(id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<ImageGallery> GetImagesForImageGallery(int id)
        {
            return _context.ImageGallery.Where(i => i.ProductId == id).ToList();
        }

        public void AddImageToImageGallery(IFormFile imageName, int productId)
        {
            ImageGallery NewImageGallery = new ImageGallery();

            NewImageGallery.ProductId = productId;

            if (imageName != null && imageName.IsImage())
            {
                string ImagePath = "";
                NewImageGallery.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(imageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductImages/", NewImageGallery.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    imageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/ProductThumbnails/", NewImageGallery.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 200);
            }

            _context.ImageGallery.Add(NewImageGallery);
            _context.SaveChanges();
        }

        public void DeleteImageFromImageGallery(int id)
        {
            _context.Entry(_context.ImageGallery.Find(id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<Comments_Questions_Answer> GetAllHasntConfirmedComments(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;
            return _context.Comments_Questions_Answers.Where(c => c.IsItCommentOrQuestionOrAnswer == 1 && c.IsConfirmed == false).OrderByDescending(c => c.Id).Skip(Skip).Take(Take).ToList();
        }

        public int GetCountOfComments()
        {
            return _context.Comments_Questions_Answers.Where(c => c.IsItCommentOrQuestionOrAnswer == 1 && c.IsConfirmed == false).Count();
        }

        public Comments_Questions_Answer GetProductText(int id)
        {
            return _context.Comments_Questions_Answers.Find(id);
        }

        public void ConfirmOrRefuseTheProductText(int orderId, int commentId)
        {
            var Comment = GetProductText(commentId);

            if (orderId == 1)
            {
                Comment.IsConfirmed = true;
            }
            else
            {
                Comment.IsRefused = true;
            }
            _context.Comments_Questions_Answers.Update(Comment);
            _context.SaveChanges();
        }

        public IEnumerable<Comments_Questions_Answer> GetHasntConfirmedQuestions(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;

            return _context.Comments_Questions_Answers.Where(c => c.IsItCommentOrQuestionOrAnswer == 2 && c.IsConfirmed == false).OrderByDescending(c => c.Id).Skip(Skip).Take(Take).ToList();
        }

        public int GetCountOfQuestions()
        {
            return _context.Comments_Questions_Answers.Where(c => c.IsItCommentOrQuestionOrAnswer == 2 && c.IsConfirmed == false).Count();
        }

        public IEnumerable<Comments_Questions_Answer> GetHasntConfirmedAnswers(int pageId)
        {
            int Take = 1;
            int Skip = (pageId - 1) * Take;

            return _context.Comments_Questions_Answers.Where(c => c.IsItCommentOrQuestionOrAnswer == 3 && c.IsConfirmed == false).OrderByDescending(c => c.Id).Skip(Skip).Take(Take).ToList();
        }

        public int GetCountOfAnswers()
        {
            return _context.Comments_Questions_Answers.Where(c => c.IsItCommentOrQuestionOrAnswer == 3 && c.IsConfirmed == false).Count();
        }

        public bool IsThereAnySpecificationWithThisCategory(int categoryId)
        {
            if (_context.ProductSpecifications.Where(p => p.CSId == categoryId).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnyValueInThisColor(int id)
        {
            if (_context.ProductColors.Where(p => p.ColorId == id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnyValueInThisColorForThisProduct(int id, int productId)
        {
            if (_context.ProductColors.Where(p => p.ColorId == id && p.ProductId == productId).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsThereAnySpecificationWithThisTitleInThisProduct(int specificationTitleId, int productId)
        {
            if (_context.ProductSpecifications.Where(p => p.CSId == specificationTitleId && p.ProductId == productId).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public NamesOfBoxes GetBox(int id)
        {
            return _context.NamesOfBoxes.SingleOrDefault(n => n.Id == id);
        }

        public void AddToBox(int productId, int TitleId)
        {
            int ProductCategoryMainId = GetProduct(productId).ProductCategoryMainId;
            int WebsiteCategoryId = GetProduct(productId).WebsiteCategoryId;

            Boxes NewBox = new Boxes();


            NewBox.ProductId = productId;
            NewBox.BoxTitleId = TitleId;
            NewBox.CategoryId = ProductCategoryMainId;
            NewBox.WebsiteCategoryId = WebsiteCategoryId;

            _context.Boxes.Add(NewBox);
            _context.SaveChanges();
        }

        public bool IsThisProductInThisBox(int productId, int titleId)
        {
            if (_context.Boxes.Where(b => b.ProductId == productId && b.BoxTitleId == titleId).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<NamesOfBoxes> GetNamesOfBoxes()
        {
            return _context.NamesOfBoxes.OrderByDescending(n=>n.Id).ToList();
        }

        public string GetBoxTitles(int id)
        {
            return _context.NamesOfBoxes.SingleOrDefault(n => n.Id == id).BoxTitle;
        }

        public void EditBoxTitle(int id, string title)
        {
            NamesOfBoxes BoxTitle = _context.NamesOfBoxes.Find(id);
            BoxTitle.BoxTitle = title;

            _context.NamesOfBoxes.Update(BoxTitle);
            _context.SaveChanges();
        }

        public void DeleteProductFromBox(int id, int TitleId)
        {
            Boxes Box = _context.Boxes.Where(b => b.ProductId == id && b.BoxTitleId == TitleId).Single();

            _context.Entry(Box).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public bool IsThisProductExistInAnyBox(int productId)
        {
            if(_context.Boxes.Where(b=>b.ProductId == productId).Any())
                return true;
            else
                return false;
        }

        public bool IsThisProductActive(int productId)
        {
            var Product = _context.Products.Find(productId);
            if (Product.IsActive == true)
                return true;
            else
                return false;
        }
    }
}
