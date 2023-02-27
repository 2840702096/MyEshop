using Microsoft.AspNetCore.Mvc;
using MyEshop.Core.Services.Interfaces;

namespace MyEShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [Area("Admin")]
        [Route("/Admin/OrderList")]
        public IActionResult Index(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _orderServices.CountOfOrders() / 15;
            return View(_orderServices.GetOrders(pageId));
        }

        #region OrderDetails

        [Area("Admin")]
        [Route("/Admin/OrderDetails/{id}")]
        public IActionResult OrderDetails(int id)
        {
            return View(_orderServices.GetOrderDetails(id));
        }

        #endregion

        #region HasRead

        [Route("/Admin/HasRead/{id}")]
        public IActionResult HasRead(int id)
        {
            _orderServices.ActivateTheHasRead(id);

            return Redirect("/Admin/OrderList");
        }

        #endregion
    }
}
