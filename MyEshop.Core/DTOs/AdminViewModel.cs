using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.DTOs
{

    #region Categories

    #region ProductCategories

    public class AddCategoryViewModel
    {
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [Display(Name = "عکس گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ImageName { get; set; }

        [Display(Name = "رنگ گروه")]
        public string Color { get; set; }
    }

    public class EditCategoryViewModel
    {
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }
        public string CurrentPhoto { get; set; }
        public IFormFile NextImage { get; set; }

        [Display(Name = "رنگ گروه")]
        public string Color { get; set; }
    }

    #endregion

    #region WebsiteCategories

    public class AddWebsiteCategoryViewModel
    {

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [Display(Name = "رنگ گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Color { get; set; }

        [Display(Name = "عکس گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ImageName { get; set; }
    }

    public class EditWebsiteCategoryViewModel
    {

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [Display(Name = "رنگ گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Color { get; set; }

        public IFormFile NextImage { get; set; }

        public string CurrentImage { get; set; }
    }

    #endregion

    #endregion

    #region Sliders

    public class AddSliderViewModel
    {
        [Display(Name = "گروه اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "آدرس اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Link { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tags { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ImageName { get; set; }
    }

    public class EditSliderViewModel
    {
        [Display(Name = "گروه اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "آدرس اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Link { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tags { get; set; }

        public string CurrentImage { get; set; }

        public IFormFile ImageName { get; set; }
    }

    #endregion

    #region Banners

    public class AddBannerViewModel
    {
        [Display(Name = "شماره ی بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int BannerNumber { get; set; }

        [Display(Name = "گروه بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "آدرس اسلایبنردر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Link { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tags { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile Pictures { get; set; }
    }

    public class EditBannerViewModel
    {
        [Display(Name = "شماره ی بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int BannerNumber { get; set; }

        [Display(Name = "گروه اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "آدرس اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Link { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tags { get; set; }

        public string CurrentImage { get; set; }

        public IFormFile ImageName { get; set; }
    }

    #endregion

    #region Weblogs

    public class AddWeblogViewModel
    {
        [Display(Name = "عنوان وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "امتیاز وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Score { get; set; }

        [Display(Name = "گروه وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "متن وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string WeblogBody { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public IFormFile ImageName { get; set; }
    }

    public class EditWeblogViewModel
    {
        [Display(Name = "عنوان وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "امتیاز وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Score { get; set; }

        [Display(Name = "گروه وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "متن وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string WeblogBody { get; set; }

        public IFormFile ImageName { get; set; }

        public string CurrentImage { get; set; }
    }

    #endregion

    #region Products

    public class AddBrandViewModel
    {

        [Display(Name = "عنوان برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BrandTitle { get; set; }

        [Display(Name = "عکس برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ImageName { get; set; }
    }

    public class EditBrandViewModel
    {

        [Display(Name = "عنوان برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BrandTitle { get; set; }
        public IFormFile ImageName { get; set; }
        public string CurrentImage { get; set; }
    }

    public class AddProductViewModel
    {
        public int ProductCategoryId { get; set; }
        public int WebsiteCategoryId { get; set; }
        public int BrandId { get; set; }

        [Display(Name = "امتیاز محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Score { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EnglishTitle { get; set; }

        [Display(Name ="نقد و بررسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "عکس محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ImageName { get; set; }

        [Display(Name = "کلمات کلیدی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; }

        [Display(Name = "تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Discount { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Price { get; set; }
        public string DiscountedPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsAmazing { get; set; }
        public bool IsDelete { get; set; }
        public bool ReturnAble { get; set; }
    }

    public class EditProductViewModel
    {
        public int ProductCategoryId { get; set; }
        public int WebsiteCategoryId { get; set; }
        public int BrandId { get; set; }

        [Display(Name = "امتیاز محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Score { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EnglishTitle { get; set; }

        [Display(Name = "نقد و بررسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        public IFormFile ImageName { get; set; }

        public string CurrentImage { get; set; }

        [Display(Name = "کلمات کلیدی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; }
        public int Discount { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        public string DiscountedPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsAmazing { get; set; }
        public bool IsDelete { get; set; }
        public bool ReturnAble { get; set; }
    }

    public class ColorListViewModel
    {
        public int ColorId { get; set; }

        [Display(Name = "قیمت رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "تخفیف رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Discount { get; set; }

        [Display(Name = "تعداد رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Quantity { get; set; }
    }

    public class ProductColorViewModel
    {
        public int ColorId { get; set; }

        [Display(Name = "قیمت رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Price { get; set; }

        [Display(Name = "تخفیف رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Discount { get; set; }

        [Display(Name = "تعداد رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Quantity { get; set; }
    }

    public class ProductPropertiesViewModel
    {
        [Display(Name = "عنوان ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Value { get; set; }
    }

    #endregion

    #region DiscountCodes

    public class DiscountCodesViewModel
    {
        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Code { get; set; }

        public string ProductCategoryId { get; set; }
        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Percent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    #endregion

    #region AnswerOfRepetitiveQuestions

    public class AnswerOfRepetitiveQuestionsViewModel
    {
        public int CategoryId { get; set; }

        [Display(Name = "سوال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string QuestionText { get; set; }

        [Display(Name = "جواب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AnswerBody { get; set; }
        public bool IsRepetitive { get; set; }
    }

    #endregion

    #region Users

    public class CreateUserViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Email { get; set; }

        [Display(Name = "کلمه ی عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }
        public IFormFile UserAvatar { get; set; }

        [Display(Name = "امتیاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Score { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class EditUserViewModel
    {
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile UserAvatar { get; set; }
        public string CurrentImage { get; set; }
        public int? Score { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    #endregion
}
