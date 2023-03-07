using Microsoft.AspNetCore.Mvc;
using MyEshop.Core.Services.Interfaces;
using System.Collections.Generic;

namespace MyEShop.Controllers
{
    public class ProductsController : Controller
    {
        private ISingleProduct _singleProduct;

        public ProductsController(ISingleProduct singleProduct)
        {
            _singleProduct = singleProduct;
        }

        [Route("/SingleProduct/{id}")]
        public IActionResult SingleProduct(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        #region Comments

        [Route("/SingleProduct/CommentPage/{id}")]
        public IActionResult CommentPage(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public IActionResult AddComment(List<int> slideCommentTitleId, List<int> slideCommentValue, string commentTitle, List<string> positivePoint, List<string> negativePoint, string commentBody, int offeringStatus, int productId)
        {
            _singleProduct.AddCommentAndPNPoints(commentTitle, commentBody, offeringStatus, positivePoint, negativePoint, productId);

            _singleProduct.AddSlideComments(slideCommentTitleId, slideCommentValue, productId);

            return Redirect($"/SingleProduct/{productId}");
        }

        #endregion

    }
}
