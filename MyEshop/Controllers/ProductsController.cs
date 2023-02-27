using Microsoft.AspNetCore.Mvc;

namespace MyEShop.Controllers
{
    public class ProductsController : Controller
    {
        [Route("/SingleProduct/{id}")]
        public IActionResult SingleProduct(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }
    }
}
