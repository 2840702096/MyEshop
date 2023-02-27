using Microsoft.AspNetCore.Mvc;
using MyEshop.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace MyEShop.Components
{
    public class ProductGroupComponent : ViewComponent
    {
        private IClientServices _clientCategoryServices;

        public ProductGroupComponent(IClientServices clientCategoryServices)
        {
            _clientCategoryServices = clientCategoryServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ProductGroup", _clientCategoryServices.GetCategories()));
        }
    }
}
