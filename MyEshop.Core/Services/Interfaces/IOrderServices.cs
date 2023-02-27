using MyEshop.DataLayer.Entities.Orders;
using MyEshop.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IOrderServices
    {
        IEnumerable<Orders> GetOrders(int pageId);
        int CountOfOrders();
        IEnumerable<OrderDetails> GetOrderDetails(int id);
        Products GetProduct(int productId);
        ColorList GetColor(int colorId);
        ProductColors GetProductColor(int colorId);
        void ActivateTheHasRead(int id);
    }
}
