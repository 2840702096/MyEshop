using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Categories;
using MyEshop.DataLayer.Entities.Products;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class SingleProduct : ISingleProduct
    {
        private ShopContext _context;

        public SingleProduct(ShopContext context)
        {
            _context = context;
        }

        public void AddCommentAndPNPoints(string commentTitle, string commentBody, int offeringStatus, List<string> positivePoints, List<string> negativePoints, int productId)
        {
            Comments_Questions_Answer Comment = new Comments_Questions_Answer();

            Comment.Title = commentTitle;
            Comment.CommentBody = commentBody;
            Comment.OfferingStatus = offeringStatus;
            Comment.IsItCommentOrQuestionOrAnswer = 1;
            Comment.UserId = 1;
            Comment.ProductId = productId;
            Comment.CreateDate = DateTime.Now;
            Comment.IsConfirmed = false;
            Comment.IsRefused = false;
            Comment.ProductId = productId;

            _context.Add(Comment);
            _context.SaveChanges();

            int CommentId = _context.Comments_Questions_Answers.OrderByDescending(c => c.Id).FirstOrDefault().Id;

            if (positivePoints != null)
            {
                foreach (var positivePoint in positivePoints)
                {
                    StrengthsOrWeaknesses SOrW = new StrengthsOrWeaknesses();

                    SOrW.UserId = 1;
                    SOrW.ProductId = productId;
                    SOrW.Text = positivePoint;
                    SOrW.IsPositiveOrNegative = true;
                    SOrW.CommentId = CommentId;
                    SOrW.IsConfirmed = false;
                    SOrW.IsRefused = false;

                    _context.Add(SOrW);
                    _context.SaveChanges();
                }
            }
            if(negativePoints != null)
            {
                foreach (var negativePoint in negativePoints)
                {
                    StrengthsOrWeaknesses SOrW = new StrengthsOrWeaknesses();

                    SOrW.UserId = 1;
                    SOrW.ProductId = productId;
                    SOrW.Text = negativePoint;
                    SOrW.IsPositiveOrNegative = false;
                    SOrW.CommentId = CommentId;
                    SOrW.IsConfirmed = false;
                    SOrW.IsRefused = false;

                    _context.Add(SOrW);
                    _context.SaveChanges();
                }
            }

        }

        public void AddSlideComments(List<int> slideCommentTitleId, List<int> slideCommentValue, int productId)
        {
            int CommentId = _context.Comments_Questions_Answers.OrderByDescending(c => c.Id).FirstOrDefault().Id;
            int Andex = 0;
            foreach (var Item in slideCommentTitleId)
            {
                SlideComments Evaluation = new SlideComments();
                Evaluation.SCTitleId = Item;
                Evaluation.ProductId = productId;
                Evaluation.UserId = 1;
                if(slideCommentValue[Andex] == 1)
                {
                    Evaluation.SCValue = 0;
                }
                if (slideCommentValue[Andex] == 2)
                {
                    Evaluation.SCValue = 25;
                }
                if (slideCommentValue[Andex] == 3)
                {
                    Evaluation.SCValue = 50;
                }
                if (slideCommentValue[Andex] == 4)
                {
                    Evaluation.SCValue = 75;
                }
                if (slideCommentValue[Andex] == 5)
                {
                    Evaluation.SCValue = 100;
                }
                Evaluation.CommentId = CommentId;
                Andex += 1;

                _context.Add(Evaluation);
                _context.SaveChanges();
            }
        }

        public ColorList GetColorItSelf(int colorId)
        {
            return _context.ColorList.Find(colorId);
        }

        public int GetColorPrice(int colorId)
        {
            ProductColors Color = _context.ProductColors.SingleOrDefault(p => p.ColorId == colorId);
            if (Color.Discount == 0)
                return Color.Price;

            else
                return Color.DiscountedPrice;
        }

        public List<ProductColors> GetColorsOfThisProduct(int productId)
        {
            return _context.ProductColors.Where(p => p.ProductId == productId).ToList();
        }

        public int GetCountOfEvaluationTitleRowsInCommentPage(int id)
        {
            int Count = _context.SlideCommentTitles.Where(s => s.ProductMainCategoryId == id).Count();

            return Count + 1 / 2;
        }

        public List<ImageGallery> GetImagesOfThisProduct(int productId)
        {
            return _context.ImageGallery.Where(i => i.ProductId == productId).ToList();
        }

        public int GetPriceOfLastAddedColor(int id)
        {
            ProductColors color = _context.ProductColors.Where(p => p.ProductId == id).OrderByDescending(p => p.ProductColorId).First();

            if (color.Discount == 0)
                return color.Price;

            else
                return color.DiscountedPrice;
        }

        public Products GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public SpecificationsInCategories GetProductSpecificationForGettingValues(int csId)
        {
            return _context.SpecificationsInCategories.Find(csId);
        }

        public List<ProductSpecifications> GetProductSpecifications(int productId)
        {
            return _context.ProductSpecifications.Where(p => p.ProductId == productId).ToList();
        }

        public List<ProductProperties> GetPropertiesOfThisProduct(int productId)
        {
            return _context.ProductProperties.Where(p => p.ProductId == productId).ToList();
        }

        public List<Products> GetRelatedProductsToThisSingleProduct(int categoryId, int productId)
        {
            return _context.Products.Where(p => p.ProductCategoryMainId == categoryId && p.ProductId != productId).ToList();
        }

        public List<SlideCommentTitles> GetSlideCommentTitles(int id, int countOfRowId)
        {
            int take = 2;
            int skip = (countOfRowId - 1) * take;
            return _context.SlideCommentTitles.Where(s => s.ProductMainCategoryId == id).OrderBy(s => s.Id).Skip(skip).Take(take).ToList();
        }
    }
}
