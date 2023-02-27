using Microsoft.AspNetCore.Mvc;
using MyEshop.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace MyEShop.Components
{
    public class WebsiteCategoryComponent : ViewComponent
    {
        private IClientServices _clientCategoryServices;

        public WebsiteCategoryComponent(IClientServices clientCategoryServices)
        {
            _clientCategoryServices = clientCategoryServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("WebsiteCategory", _clientCategoryServices.GetWebsiteCategories()));
        }
    }
}
