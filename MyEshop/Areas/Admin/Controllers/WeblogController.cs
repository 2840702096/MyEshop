using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Weblogs;
using System.Collections.Generic;

namespace MyEShop.Areas.Admin.Controllers
{
    public class WeblogController : Controller
    {
        private IWeblogServices _weblogServices;

        public WeblogController(IWeblogServices weblogServices)
        {
            _weblogServices = weblogServices;
        }

        #region Weblogs

        [Area("Admin")]
        [Route("/Admin/WeblogList/{id?}")]
        public IActionResult Index(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _weblogServices.GetCountOfWeblogs() / 15;
            return View(_weblogServices.GetAllWeblogs(pageId));
        }

        #region CreateWeblog

        [Area("Admin")]
        [Route("/Admin/CreateWeblog")]
        public IActionResult CreateWeblog()
        {
            ViewBag.Categories = new SelectList(_weblogServices.GetCategories(), "Value", "Text");
            ViewBag.Number = 0;

            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateWeblog")]
        [HttpPost]
        public IActionResult CreateWeblog(AddWeblogViewModel weblog)
        {
            ViewBag.Categories = new SelectList(_weblogServices.GetCategories(), "Value", "Text");
            if (!ModelState.IsValid)
            {
                if (weblog.Score == 0)
                {
                    ModelState.AddModelError("Score", "لطفا امتیاز وبلاگ را وارد کنید");
                    ViewBag.Number = weblog.Score;
                }
                return View(weblog);
            }
            if (weblog.Score > 5)
            {
                ModelState.AddModelError("Score", "عدد وارد شده نباید بالاتر از 5 باشد");
                ViewBag.Number = weblog.Score;
                return View(weblog);
            }


            _weblogServices.AddWeblog(weblog);

            return Redirect("/Admin/WeblogList");
        }

        #endregion

        #region EditWeblog

        [Area("Admin")]
        [Route("/Admin/EditWeblog/{id}")]
        public IActionResult EditWeblog(int id)
        {
            var Weblog = _weblogServices.GetWeblogInfoForEdit(id);

            ViewBag.Categories = new SelectList(_weblogServices.GetCategories(), "Value", "Text", Weblog.CategoryId);
            ViewBag.Number = Weblog.Score;
            return View(Weblog);
        }

        [Area("Admin")]
        [Route("/Admin/EditWeblog/{id}")]
        [HttpPost]
        public IActionResult EditWeblog(int id, EditWeblogViewModel weblog, string currentImage)
        {
            var Weblog = _weblogServices.GetWeblogInfoForEdit(id);
            ViewBag.Categories = new SelectList(_weblogServices.GetCategories(), "Value", "Text", Weblog.CategoryId);
            if (weblog.Score == 0)
            {
                ModelState.AddModelError("Score", "لطفا امتیاز وبلاگ را وارد کنید");
                ViewBag.Number = weblog.Score;
                return View(weblog);
            }
            if (!ModelState.IsValid)
            {
                return View(weblog);
            }
            if (weblog.Score > 5)
            {
                ModelState.AddModelError("Score", "عدد وارد شده نباید بالاتر از 5 باشد");
                ViewBag.Number = weblog.Score;
                return View(weblog);
            }

            _weblogServices.EditWeblog(id, weblog, currentImage);

            return Redirect("/Admin/WeblogList");
        }

        #endregion

        #region DeleteWeblog

        [Area("Admin")]
        [Route("/Admin/DeleteWeblog")]
        public IActionResult DeleteWeblog(int id)
        {
            _weblogServices.DeleteWeblog(id);

            return Redirect("/Admin/WeblogList");
        }

        #endregion

        #region Specifications

        [Area("Admin")]
        [Route("/Admin/WeblogSpecifications/{id}")]
        public IActionResult WeblogSpecifications(int id)
        {
            return View(_weblogServices.GetWeblog(id));
        }

        [Area("Admin")]
        [Route("/Admin/CompeleteWeblogBody/{id}")]
        public IActionResult CompeleteWeblogBody(int id)
        {
            return View(_weblogServices.GetWeblog(id));
        }

        #endregion

        #region Search

        [Area("Admin")]
        [Route("/Admin/SearchWeblog")]
        public IActionResult SearchWeblog(string weblogTitle, int pageId = 1)
        {
            List<Weblog> FilteredWeblog = new List<Weblog>();
            FilteredWeblog.AddRange(_weblogServices.FilterWeblogs(weblogTitle, pageId));

            ViewBag.PageId = pageId;
            ViewBag.PageCount = _weblogServices.GetCountOfFilteredWeblogs(weblogTitle) / 15;
            ViewBag.SearchText = weblogTitle;

            return View("Index", FilteredWeblog);
        }

        #endregion

        #endregion

        #region WeblogComments


        [Area("Admin")]
        [Route("/Admin/WeblogComments/{id?}")]
        public IActionResult WeblogComments(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _weblogServices.GetCountOfWeblogComments() / 1;

            return View(_weblogServices.WeblogComments(pageId));
        }

        #region CompeleteComment

        [Area("Admin")]
        [Route("/Admin/CompeleteComment/{id}")]
        public IActionResult CompeleteComment(int id)
        {
            return View(_weblogServices.GetWeblogComments(id));
        }

        #endregion

        #region ConfirmComment

        [Area("Admin")]
        [Route("/Admin/ConfirmComment/{id}")]
        public IActionResult ConfirmComment(int id)
        {
            _weblogServices.ConfirmComment(id);

            return Redirect("/Admin/WeblogComments");
        }

        #endregion

        #endregion

    }
}
