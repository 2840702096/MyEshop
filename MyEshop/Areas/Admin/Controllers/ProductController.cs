using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Products;
using System.Collections.Generic;

namespace MyEShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [Area("Admin")]
        [Route("/Admin/CompeleteProductText/{id}")]
        public IActionResult CompeleteProductText(int id)
        {
            return View(_productServices.GetProductText(id));
        }

        #region Products

        [Area("Admin")]
        [Route("/Admin/ProductList")]
        public IActionResult Index(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _productServices.GetCountOfProducts() / 15;
            return View(_productServices.GetProducts(pageId));
        }

        #region ProductOrders

        #region CreateProduct

        [Area("Admin")]
        [Route("/Admin/CreateProduct")]
        public IActionResult CreateProduct()
        {
            var Brands = _productServices.GetBrands();
            var ProductCategories = _productServices.GetProductCategories();
            var WebsiteCategories = _productServices.GetWebsiteCategories();

            ViewBag.Brands = new SelectList(Brands, "Value", "Text");
            ViewBag.ProductCategories = new SelectList(ProductCategories, "Value", "Text");
            ViewBag.WebsiteCategories = new SelectList(WebsiteCategories, "Value", "Text");
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateProduct")]
        [HttpPost]
        public IActionResult CreateProduct(AddProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                var Brands = _productServices.GetBrands();
                var ProductCategories = _productServices.GetProductCategories();
                var WebsiteCategories = _productServices.GetWebsiteCategories();
                ViewBag.Brands = new SelectList(Brands, "Value", "Text");
                ViewBag.ProductCategories = new SelectList(ProductCategories, "Value", "Text");
                ViewBag.WebsiteCategories = new SelectList(WebsiteCategories, "Value", "Text");
                return View(product);
            }

            _productServices.AddProduct(product);

            return Redirect("/Admin/ProductList");
        }

        #endregion

        #region EditProduct

        [Area("Admin")]
        [Route("/Admin/EditProduct/{id}")]
        public IActionResult EditProduct(int id)
        {
            var Product = _productServices.GetProductInfoForEdit(id);

            var Brands = _productServices.GetBrands();
            var ProductCategories = _productServices.GetProductCategories();
            var WebsiteCategories = _productServices.GetWebsiteCategories();

            ViewBag.Brands = new SelectList(Brands, "Value", "Text", Product.BrandId);
            ViewBag.ProductCategories = new SelectList(ProductCategories, "Value", "Text", Product.ProductCategoryId);
            ViewBag.WebsiteCategories = new SelectList(WebsiteCategories, "Value", "Text", Product.WebsiteCategoryId);

            return View(Product);
        }

        [Area("Admin")]
        [Route("/Admin/EditProduct/{id}")]
        [HttpPost]
        public IActionResult EditProduct(int id, EditProductViewModel edit, string currentImage)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CanNotBeNounActive = false;
                var Product = _productServices.GetProductInfoForEdit(id);
                var Brands = _productServices.GetBrands();
                var ProductCategories = _productServices.GetProductCategories();
                var WebsiteCategories = _productServices.GetWebsiteCategories();
                ViewBag.Brands = new SelectList(Brands, "Value", "Text", Product.BrandId);
                ViewBag.ProductCategories = new SelectList(ProductCategories, "Value", "Text", Product.ProductCategoryId);
                ViewBag.WebsiteCategories = new SelectList(WebsiteCategories, "Value", "Text", Product.WebsiteCategoryId);
                return View(edit);
            }

            if(edit.IsActive == false)
            {
                if (_productServices.IsThisProductExistInAnyBox(id) == true)
                {
                    ViewBag.CanNotBeNounActive = true;
                    var Product = _productServices.GetProductInfoForEdit(id);
                    var Brands = _productServices.GetBrands();
                    var ProductCategories = _productServices.GetProductCategories();
                    var WebsiteCategories = _productServices.GetWebsiteCategories();
                    ViewBag.Brands = new SelectList(Brands, "Value", "Text", Product.BrandId);
                    ViewBag.ProductCategories = new SelectList(ProductCategories, "Value", "Text", Product.ProductCategoryId);
                    ViewBag.WebsiteCategories = new SelectList(WebsiteCategories, "Value", "Text", Product.WebsiteCategoryId);
                    return View(edit);
                }
            }

            _productServices.EditProduct(id, edit, currentImage);

            return Redirect("/Admin/ProductList");
        }

        #endregion

        #region DeleteProduct

        [Area("Admin")]
        [Route("/Admin/DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id, int pageId)
        {
            if (_productServices.IsThisProductExistInAnyBox(id) == true)
            {
                ViewBag.ThisProductIsExistInBoxes = true;
                ViewBag.PageId = pageId;
                ViewBag.PageCount = _productServices.GetCountOfProducts() / 15;
                return View("Index", _productServices.GetProducts(pageId));
            }

            _productServices.DeleteProduct(id);

            return Redirect("/Admin/ProductList");
        }

        #endregion

        #region Search

        [Area("Admin")]
        [Route("/Admin/ProductSearch")]
        public IActionResult ProductSearch(string productTitle, int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _productServices.GetCountOfProducts() / 15;

            List<Products> FilteredProduct = new List<Products>();
            FilteredProduct.AddRange(_productServices.FilterProductList(productTitle));

            ViewBag.SearchText = productTitle;

            return View("Index", FilteredProduct);
        }

        #endregion

        #region ThePageForSpecificationsOfProduct

        [Area("Admin")]
        [Route("/Admin/ProductSpecifications/{id}")]
        public IActionResult ProductSpecifications(int id)
        {
            return View(_productServices.GetProduct(id));
        }

        [Area("Admin")]
        [Route("/Admin/CompeleteDescriptionPage/{id}")]
        public IActionResult CompeleteDescriptionPage(int id)
        {
            return View(_productServices.GetProduct(id));
        }

        #endregion

        #region ThePageForAddingToBoxes

        [Area("Admin")]
        [Route("/Admin/ThePageForAddingToBoxes/{id}")]
        public IActionResult ThePageForAddingToBoxes(int id)
        {
            if (_productServices.IsThisProductActive(id) == false)
            {
                if (_productServices.IsThisProductInThisBox(id, 1) == true)
                {
                    ViewBag.ThisIsExistInFirstBox = true;
                    ViewBag.ThisProductIsNounActive = true;

                    if (_productServices.IsThisProductInThisBox(id, 2) == true)
                    {
                        ViewBag.ThisIsExistInSecondBox = true;
                        ViewBag.ThisProductIsNounActive = true;
                        return View("ThePageForAddingToBoxes", _productServices.GetProduct(id));
                    }
                    return View("ThePageForAddingToBoxes", _productServices.GetProduct(id));
                }

                if (_productServices.IsThisProductInThisBox(id, 2) == true)
                {
                    ViewBag.ThisIsExistInSecondBox = true;
                    ViewBag.ThisProductIsNounActive = true;

                    if (_productServices.IsThisProductInThisBox(id, 1) == true)
                    {
                        ViewBag.ThisIsExistInFirstBox = true;
                        ViewBag.ThisProductIsNounActive = true;
                        return View("ThePageForAddingToBoxes", _productServices.GetProduct(id));
                    }
                    return View("ThePageForAddingToBoxes", _productServices.GetProduct(id));
                }
            }
            else
            {
                if (_productServices.IsThisProductInThisBox(id, 1) == true)
                {
                    ViewBag.ThisIsExistInFirstBox = true;
                    ViewBag.ThisProductIsNounActive = false;

                    if (_productServices.IsThisProductInThisBox(id, 2) == true)
                    {
                        ViewBag.ThisIsExistInSecondBox = true;
                        ViewBag.ThisProductIsNounActive = false;
                        return View("ThePageForAddingToBoxes", _productServices.GetProduct(id));
                    }
                    return View("ThePageForAddingToBoxes", _productServices.GetProduct(id));
                }

                if (_productServices.IsThisProductInThisBox(id, 2) == true)
                {
                    ViewBag.ThisIsExistInSecondBox = true;
                    ViewBag.ThisProductIsNounActive = false;

                    if (_productServices.IsThisProductInThisBox(id, 1) == true)
                    {
                        ViewBag.ThisIsExistInFirstBox = true;
                        ViewBag.ThisProductIsNounActive = false;
                        return View("ThePageForAddingToBoxes", _productServices.GetProduct(id));
                    }
                    return View("ThePageForAddingToBoxes", _productServices.GetProduct(id));
                }
            }
            if (_productServices.IsThisProductActive(id) == false)
                ViewBag.ThisProductIsNounActive = true;
            else
                ViewBag.ThisProductIsNounActive = false;

            return View(_productServices.GetProduct(id));
        }

        [Area("Admin")]
        [Route("/Admin/AddToBoxes/{id}")]
        public IActionResult AddToBoxes(int id, int boxTitleId)
        {
            if (_productServices.IsThisProductActive(id) == false)
                return Redirect($"/Admin/ThePageForAddingToBoxes/{id}");


            _productServices.AddToBox(id, boxTitleId);
            return Redirect($"/Admin/ThePageForAddingToBoxes/{id}");
        }

        [Route("/Admin/DeleteFromBox/{id}")]
        public IActionResult DeleteFromBox(int id, int boxTitleId)
        {
            _productServices.DeleteProductFromBox(id, boxTitleId);

            return Redirect($"/Admin/ThePageForAddingToBoxes/{id}");
        }

        #endregion

        #endregion

        #region ProductSpecificationValues

        [Area("Admin")]
        [Route("/Admin/ProductSpecificationValues/{id?}")]
        public IActionResult ProductSpecificationValues(int id, int categoryId)
        {
            ViewBag.SpecificationTitle = new SelectList(_productServices.GetCategorySpecifications(categoryId), "Value", "Text");
            ViewBag.Id = id;
            ViewBag.ProductCategoryId = categoryId;

            return View(_productServices.GetProductSpecifications(id));
        }

        #region AddProductSpecificationValue

        [Area("Admin")]
        [Route("/Admin/AddProductSpecificationValue")]
        public IActionResult AddProductSpecificationValue(int categoryId, string value, int id, int productCategoryId)
        {
            if (value == null)
            {
                ViewBag.Id = id;
                ViewBag.Error = true;
                ViewBag.SpecificationTitle = new SelectList(_productServices.GetCategorySpecifications(productCategoryId), "Value", "Text");
                ViewBag.ProductCategoryId = categoryId;
                return View("ProductSpecificationValues", _productServices.GetProductSpecifications(id));
            }

            if (_productServices.IsThereAnySpecificationWithThisTitleInThisProduct(categoryId, id))
            {
                ViewBag.Id = id;
                ViewBag.SpecificationTitle = new SelectList(_productServices.GetCategorySpecifications(productCategoryId), "Value", "Text");
                ViewBag.ProductCategoryId = categoryId;
                ViewBag.FrequentlyCategory = true;
                return View("ProductSpecificationValues", _productServices.GetProductSpecifications(id));
            }

            _productServices.AddProductSpecification(id, categoryId, value);

            ViewBag.Id = id;
            ViewBag.ProductCategoryId = categoryId;
            ViewBag.SpecificationTitle = new SelectList(_productServices.GetCategorySpecifications(productCategoryId), "Value", "Text");
            return Redirect($"/Admin/ProductSpecificationValues/{id}?categoryId={productCategoryId}");
        }

        #endregion

        #region DeleteProductSpecificationValue

        [Route("/Admin/DeleteProductSpecificationValue/{id?}")]
        public IActionResult DeleteProductSpecificationValue(int id, int productId, int categoryId)
        {
            _productServices.DeleteProductSpecification(id);

            return Redirect($"/Admin/ProductSpecificationValues/{productId}?categoryId={_productServices.GetProductCategoryId(productId)}");
        }

        #endregion

        #endregion

        #region ColorList

        [Area("Admin")]
        [Route("/Admin/ColorList")]
        public IActionResult ColorList()
        {
            ViewBag.Colors = _productServices.GetColorsForColorList();
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateColor")]
        [HttpPost]
        public IActionResult CreateColor(string colorItSelf, string colorTitle)
        {
            if (colorTitle == null)
            {
                ViewBag.ColorTitle = colorTitle;
                ViewBag.ColorItSelf = colorItSelf;
                ViewBag.TitleError = true;
                ViewBag.Colors = _productServices.GetColorsForColorList();
                return View("ColorList");
            }
            _productServices.AddColorToColorList(colorTitle, colorItSelf);

            return Redirect("/Admin/ColorList");
        }

        [Area("Admin")]
        [Route("/Admin/DeleteColor/{id}")]
        public IActionResult DeleteColor(int id)
        {
            if (_productServices.IsThereAnyValueInThisColor(id))
            {
                ViewBag.UnAbleToDelete = true;
                ViewBag.Colors = _productServices.GetColorsForColorList();
                return View("ColorList");
            }

            _productServices.DeleteColorFromColorList(id);

            return Redirect("/Admin/ColorList");
        }

        #endregion

        #region ProductColorsList

        [Area("Admin")]
        [Route("/Admin/ProductColors/{id}")]
        public IActionResult ProductColors(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.ColorList = new SelectList(_productServices.GetColorsOfColorList(), "Value", "Text");
            ViewBag.Colors = _productServices.GetProductColors(id);
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/AddProductColor")]
        public IActionResult AddProductColor(int id, ProductColorViewModel color)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductId = id;
                ViewBag.ColorList = new SelectList(_productServices.GetColorsOfColorList(), "Value", "Text");
                ViewBag.Colors = _productServices.GetProductColors(id);
                return View("ProductColors", color);
            }

            if (_productServices.IsThereAnyValueInThisColorForThisProduct(color.ColorId, id) == true)
            {
                ViewBag.FrequentlyColor = true;
                ViewBag.ProductId = id;
                ViewBag.ColorList = new SelectList(_productServices.GetColorsOfColorList(), "Value", "Text");
                ViewBag.Colors = _productServices.GetProductColors(id);
                return View("ProductColors", color);
            }

            _productServices.AddProductColor(id, color);

            return Redirect($"/Admin/ProductColors/{id}");
        }

        [Route("/Admin/DeleteProductColor/{id}")]
        public IActionResult DeleteProductColor(int id, int productId)
        {
            _productServices.DeleteProductColor(id);
            return Redirect($"/Admin/ProductColors/{productId}");
        }

        #endregion

        #region ProductProperites

        [Area("Admin")]
        [Route("/Admin/ProductProperties/{id}")]
        public IActionResult ProductProperties(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.Properties = _productServices.GetProdoductProperties(id);

            return View();
        }


        [Area("Admin")]
        [Route("/Admin/AddProductProperty")]
        public IActionResult AddProductProperty(ProductPropertiesViewModel property, int id)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductId = id;
                ViewBag.Properties = _productServices.GetProdoductProperties(id);
                return View("ProductProperties", property);
            }
            _productServices.AddProductProperty(property, id);
            return Redirect($"/Admin/ProductProperties/{id}");
        }

        [Route("/Admin/DeleteProductProperty/{id}")]
        public IActionResult DeleteProductProperty(int id, int productId)
        {
            _productServices.DeleteProductProperty(id);

            return Redirect($"/Admin/ProductProperties/{productId}");
        }

        #endregion

        #region ImageGallery

        [Area("Admin")]
        [Route("/Admin/ProductImageGallery/{id}")]
        public IActionResult ProductImageGallery(int id)
        {
            ViewBag.ProductId = id;
            return View(_productServices.GetImagesForImageGallery(id));
        }

        [Area("Admin")]
        [Route("/Admin/AddImageToImageGallery")]
        public IActionResult AddImageToImageGallery(IFormFile imageName, int productId)
        {
            if (imageName == null)
            {
                ViewBag.ProductId = productId;
                ViewBag.Error = true;
                return View("ProductImageGallery", _productServices.GetImagesForImageGallery(productId));
            }
            _productServices.AddImageToImageGallery(imageName, productId);
            return Redirect($"/Admin/ProductImageGallery/{productId}");
        }

        [Area("Admin")]
        [Route("/Admin/DeleteImageFromImageGallery/{id}")]
        public IActionResult DeleteImageFromImageGallery(int id, int productId)
        {
            _productServices.DeleteImageFromImageGallery(id);

            return Redirect($"/Admin/ProductImageGallery/{productId}");
        }

        #endregion

        #endregion

        #region Brands

        [Area("Admin")]
        [Route("/Admin/Brands")]
        public IActionResult Brands(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _productServices.GetCountOfProductBrands() / 15;
            return View(_productServices.GetBrands(pageId));
        }

        #region CreateBrand

        [Area("Admin")]
        [Route("/Admin/CreateBrand")]
        public IActionResult CreateBrand()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateBrand")]
        [HttpPost]
        public IActionResult CreateBrand(AddBrandViewModel brand)
        {
            if (!ModelState.IsValid)
                return View(brand);

            _productServices.AddBrand(brand);

            return Redirect("/Admin/Brands");
        }

        #endregion

        #region EditBrand

        [Area("Admin")]
        [Route("/Admin/EditBrand/{id}")]
        public IActionResult EditBrand(int id)
        {
            return View(_productServices.GetBrandInfoForEdit(id));
        }

        [Area("Admin")]
        [Route("/Admin/EditBrand/{id}")]
        [HttpPost]
        public IActionResult EditBrand(int id, EditBrandViewModel brand, string currentImage)
        {
            if (!ModelState.IsValid)
                return View(brand);

            _productServices.EditBrand(id, brand, currentImage);

            return Redirect("/Admin/Brands");
        }

        #endregion

        #region DeleteBrand

        [Area("Admin")]
        [Route("/Admin/DeleteBrand/{id}")]
        public IActionResult DeleteBrand(int id, int pageId = 1)
        {
            if (_productServices.IsThereAProductInThisBrand(id))
            {
                ViewBag.ThereIsProductInThisBrand = true;
                ViewBag.PageId = pageId;
                ViewBag.PageCount = _productServices.GetCountOfProductBrands() / 15;
                return View("/Admin/Brands", _productServices.GetBrands(pageId));
            }

            _productServices.DeleteBrand(id);

            return Redirect("/Admin/Brands");
        }

        #endregion

        #region Search

        [Area("Admin")]
        [Route("/Admin/SearchBrands")]
        public IActionResult SearchBrands(string brandTitle, int pageId = 1)
        {
            List<ProductBrands> ProductBrands = new List<ProductBrands>();
            ProductBrands.AddRange(_productServices.FilterBrands(brandTitle, pageId));
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _productServices.GetCountOfFilteredBrands(brandTitle) / 15;
            ViewBag.SearchText = brandTitle;
            return View("Brands", ProductBrands);
        }

        #endregion

        #endregion

        #region Comments

        [Area("Admin")]
        [Route("/Admin/Comments/{id?}")]
        public IActionResult Comments(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _productServices.GetCountOfComments() / 15;
            return View(_productServices.GetAllHasntConfirmedComments(pageId));
        }

        #endregion

        #region Questions

        [Area("Admin")]
        [Route("/Admin/Questions")]
        public IActionResult Questions(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _productServices.GetCountOfQuestions() / 15;
            return View(_productServices.GetHasntConfirmedQuestions(pageId));
        }

        #endregion

        #region Answers

        [Area("Admin")]
        [Route("/Admin/Answers")]
        public IActionResult Answers(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _productServices.GetCountOfAnswers() / 1;
            return View(_productServices.GetHasntConfirmedAnswers(pageId));
        }

        #endregion

        #region ConfirmOrRefuseText

        [Route("/Admin/ConfirmOrRefuseText")]
        public IActionResult ConfirmOrRefuseText(int orderId, int Id, int whichPage)
        {
            _productServices.ConfirmOrRefuseTheProductText(orderId, Id);

            if (whichPage == 1)
            {
                return Redirect("/Admin/Comments");
            }
            else
            {
                if (whichPage == 2)
                {
                    return Redirect("/Admin/Questions");
                }
                else
                {
                    return Redirect("/Admin/Answers");
                }
            }
        }

        #endregion

        #region Boxes

        [Area("Admin")]
        [Route("/Admin/Boxes")]
        public IActionResult Boxes()
        {
            return View(_productServices.GetNamesOfBoxes());
        }

        [Area("Admin")]
        [Route("/Admin/EditBoxTitle/{id}")]
        public IActionResult EditBoxTitle(int id)
        {
            ViewBag.Name = _productServices.GetBoxTitles(id);
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/EditBoxTitle/{id}")]
        [HttpPost]
        public IActionResult EditBoxTitle(int id, string title)
        {
            if (title == null)
            {
                ViewBag.EmptyTitle = true;
                return View();
            }

            _productServices.EditBoxTitle(id, title);
            return Redirect("/Admin/Boxes");
        }

        #endregion
    }
}
