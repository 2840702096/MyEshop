using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEshop.Core.Services;
using MyEshop.Core.Services.Interfaces;

namespace MyEShop.Areas.Admin.Controllers
{
    public class RulesAndOthersController : Controller
    {
        private IRulesAndOthersServices _rulesAndOthersServices;

        public RulesAndOthersController(IRulesAndOthersServices rulesAndOthersServices)
        {
            _rulesAndOthersServices = rulesAndOthersServices;
        }

        #region SocialApps

        [Area("Admin")]
        [Route("/Admin/SocialApps")]
        public IActionResult Index()
        {
            return View(_rulesAndOthersServices.GetSocialApps());
        }

        #region PutALink

        [Area("Admin")]
        [Route("/Admin/PutALink/{id}")]
        public IActionResult PutALink(int id)
        {
            ViewBag.Link = _rulesAndOthersServices.GetSocialAppInfo(id);
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/PutALink/{id}")]
        [HttpPost]
        public IActionResult PutALink(int id, string link)
        {
            _rulesAndOthersServices.PutALink(link, id);

            return Redirect("/Admin/SocialApps");
        }

        #endregion

        #endregion

        #region Logoes

        [Area("Admin")]
        [Route("/Admin/Logoes")]
        public IActionResult Logoes()
        {
            return View(_rulesAndOthersServices.GetLogoes());
        }

        #region PutALogo

        [Area("Admin")]
        [Route("/Admin/PutALogo/{id}")]
        public IActionResult PutALogo(int id)
        {
            var Logo = _rulesAndOthersServices.GetLogoInfo(id);

            ViewBag.Image = Logo.ImageName;

            return View(Logo);
        }

        [Area("Admin")]
        [Route("/Admin/PutALogo/{id}")]
        [HttpPost]
        public IActionResult PutALogo(int id, string currentImage, IFormFile imageName)
        {
            _rulesAndOthersServices.PutALogo(id, currentImage, imageName);

            return Redirect("/Admin/Logoes");
        }

        #endregion

        #endregion

        #region PrivacyRules

        [Area("Admin")]
        [Route("/Admin/PrivacyRules/{id}")]
        public IActionResult PrivacyRules(int id)
        {
            return View(_rulesAndOthersServices.GetPrivacyRules(id));
        }

        [Area("Admin")]
        [Route("/Admin/PrivacyRules/{id}")]
        [HttpPost]
        public IActionResult PrivacyRules(int id, string title, string text)
        {
            if (title == null)
            {
                ViewBag.TitleError = true;
                return View(_rulesAndOthersServices.GetPrivacyRules(id));
            }
            if (text == null)
            {
                ViewBag.TextError = true;
                return View(_rulesAndOthersServices.GetPrivacyRules(id));
            }

            _rulesAndOthersServices.PutPrivacyRule(id, title, text);

            return Redirect($"/Admin/PrivacyRules/{id}");
        }

        #endregion

        #region PublishContentRules

        [Area("Admin")]
        [Route("/Admin/PublishContentRules/{id}")]
        public IActionResult PublishContentRules(int id)
        {
            return View(_rulesAndOthersServices.GetPublishContentRules(id));
        }

        [Area("Admin")]
        [Route("/Admin/PublishContentRules/{id}")]
        [HttpPost]
        public IActionResult PublishContentRules(int id, string title, string text)
        {
            if (title == null)
            {
                ViewBag.title = title;
                ViewBag.TitleError = true;
                return View(_rulesAndOthersServices.GetPublishContentRules(id));
            }
            if (text == null)
            {
                ViewBag.text = text;
                ViewBag.TextError = true;
                return View(_rulesAndOthersServices.GetPublishContentRules(id));
            }

            _rulesAndOthersServices.PutPublishContentRules(id, title, text);

            return Redirect($"/Admin/PublishContentRules/{id}");
        }

        #endregion

        #region AboutUs

        [Area("Admin")]
        [Route("/Admin/AboutUs/{id}")]
        public IActionResult AboutUs(int id)
        {
            return View(_rulesAndOthersServices.GetAboutUs(id));
        }

        [Area("Admin")]
        [Route("/Admin/AboutUs/{id}")]
        [HttpPost]
        public IActionResult AboutUs(int id, string title, string text)
        {
            if (title == null)
            {
                ViewBag.TitleError = true;
                return View(_rulesAndOthersServices.GetAboutUs(id));
            }
            if (text == null)
            {
                ViewBag.TextError = true;
                return View(_rulesAndOthersServices.GetAboutUs(id));
            }

            _rulesAndOthersServices.PutAboutUsDucuments(id,title,text);

            return View(_rulesAndOthersServices.GetAboutUs(id));
        }



        #endregion

    }
}