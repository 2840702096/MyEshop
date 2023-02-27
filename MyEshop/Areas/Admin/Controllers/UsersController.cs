using Microsoft.AspNetCore.Mvc;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Users;
using System.Collections.Generic;

namespace MyEShop.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [Area("Admin")]
        [Route("/Admin/Users")]
        public IActionResult Index(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _userServices.GetCount() / 15;
            return View(_userServices.GetUsers(pageId));
        }

        [Area("Admin")]
        [Route("/Admin/SearchUsers")]
        public IActionResult SearchUsers(string email, string userName,int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _userServices.GetCountOfFilteredUsers(email, userName)/15;
            List<Users> FilteredUsers = new List<Users>();
            FilteredUsers.AddRange(_userServices.FilterUsers(email, userName, pageId));
            ViewBag.UserName = userName;
            ViewBag.Email = email;
            return View("Index",FilteredUsers);
        }

        #region CreateUser

        [Area("Admin")]
        [Route("/Admin/CreateUser")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateUser")]
        [HttpPost]
        public IActionResult CreateUser(CreateUserViewModel user)
        {
            if(!ModelState.IsValid)
            return View(user);

            if (_userServices.IsEmailExist(0, user.Email))
            {
                ViewBag.EmailExist = true;
                return View(user);
            }

            if (_userServices.IsPhoneNumberExist(0, user.PhoneNumber))
            {
                ViewBag.PhoneNumgerExist = true;
                return View(user);
            }

            _userServices.AddUser(user);

            return Redirect("/Admin/Users");
        }

        #endregion

        #region EditUser

        [Area("Admin")]
        [Route("/Admin/EditUser/{id}")]
        public IActionResult EditUser(int id)
        {
            return View(_userServices.GetUserInfo(id));
        }

        [Area("Admin")]
        [Route("/Admin/EditUser/{id}")]
        [HttpPost]
        public IActionResult EditUser(int id, EditUserViewModel model, string currentPhoto)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_userServices.IsEmailExist(id,model.Email))
            {
                ViewBag.EmailExist = true;
                return View(model);
            }

            if (_userServices.IsPhoneNumberExist(id,model.PhoneNumber))
            {
                ViewBag.PhoneNumgerExist = true;
                return View(model);
            }

            _userServices.EditUser(id, model, currentPhoto);

            return Redirect("/Admin/Users");
        }

        #endregion

        #region DeleteUser

        [Route("/Admin/DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userServices.DeleteUser(id);

            return Redirect("/Admin/Users");
        }

        #endregion

    }
}
