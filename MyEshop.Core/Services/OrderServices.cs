using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Orders;
using MyEshop.DataLayer.Entities.Products;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class OrderServices : IOrderServices
    {
        private ShopContext _context;

        public OrderServices(ShopContext context)
        {
            _context = context;
        }

        public void ActivateTheHasRead(int id)
        {
            var Order = _context.Orders.Find(id);

            Order.HasRead = true;
            _context.Orders.Update(Order);
            _context.SaveChanges();
        }

        public int CountOfOrders()
        {
            return _context.Orders.Where(c => c.IsFinaly == true && c.HasRead == false).Count();
        }

        public ColorList GetColor(int colorId)
        {
            return _context.ColorList.SingleOrDefault(c => c.ColorId == colorId);
        }

        public IEnumerable<OrderDetails> GetOrderDetails(int id)
        {
            return _context.OrderDetails.Where(o=>o.OrderDetailId == id).ToList();
        }

        public IEnumerable<Orders> GetOrders(int pageId)
        {
            int take = 15;
            int skip = (pageId - 1) * take;

            return _context.Orders.Where(c => c.IsFinaly == true && c.HasRead == false).OrderBy(c => c.OrderId).Skip(skip).Take(take).ToList();
        }

        public Products GetProduct(int productId)
        {
            return _context.Products.SingleOrDefault(p => p.ProductId == productId);
        }

        public ProductColors GetProductColor(int colorId)
        {
            return _context.ProductColors.SingleOrDefault(c => c.ColorId == colorId);
        }
    }
}
