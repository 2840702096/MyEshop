using MyEshop.Core.Convertors;
using MyEshop.Core.DTOs;
using MyEshop.Core.Generators;
using MyEshop.Core.Security;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Users;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class UserServices : IUserServices
    {
        private ShopContext _context;

        public UserServices(ShopContext context)
        {
            _context = context;
        }

        public void AddUser(CreateUserViewModel user)
        {
            Users NewUser = new Users();
            NewUser.UserName = user.UserName;
            NewUser.Email = user.Email;
            NewUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            NewUser.IsActive = user.IsActive;
            NewUser.Score = user.Score.Value;
            NewUser.RegisterDate = DateTime.Now;
            NewUser.ActiveCode = CodeGenerator.UniqCodeGenerator();
            NewUser.IsDelete = false;
            NewUser.PhoeNumber = user.PhoneNumber;

            if (user.UserAvatar != null && user.UserAvatar.IsImage())
            {
                string ImagePath = "";
                NewUser.UserAvatar = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(user.UserAvatar.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/UserAvatarImages/", NewUser.UserAvatar);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/UserAvatarThumbNail/", NewUser.UserAvatar);
                convertor.Image_resize(ImagePath, ThumbPass, 300);
            }
            else
            {
                NewUser.UserAvatar = "Twitter-new-2017-avatar-001.png";
            }

            _context.Users.Add(NewUser);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var User = _context.Users.Find(id);

            User.IsDelete = true;
            _context.Users.Update(User);
            _context.SaveChanges();
        }

        public void EditUser(int id, EditUserViewModel user, string currentPhoto)
        {
            var User = _context.Users.Find(id);

            User.UserName = user.UserName;
            User.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Password))
            {
                User.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            }
            User.IsActive = user.IsActive
            ;
            User.Score = user.Score.Value;
            User.PhoeNumber = user.PhoneNumber;

            if (user.UserAvatar != null && user.UserAvatar.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentPhoto != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/UserAvatarImages/", currentPhoto);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/UserAvatarThumbNail/", currentPhoto);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                User.UserAvatar = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(user.UserAvatar.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/UserAvatarImages/", User.UserAvatar);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/UserAvatarThumbNail/", User.UserAvatar);
                convertor.Image_resize(ImagePath, ThumbPass, 200);

            }
            else
            {
                User.UserAvatar = currentPhoto;
            }

            _context.Users.Update(User);
            _context.SaveChanges();

        }

        public IEnumerable<Users> FilterUsers(string email, string userName, int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;
            return _context.Users.Where(u => u.Email.Contains(email) || u.UserName.Contains(userName)).OrderByDescending(u => u.UserId).Skip(Skip).Take(Take).ToList();
        }

        public int GetCount()
        {
            return _context.Users.Count();
        }

        public int GetCountOfFilteredUsers(string email, string userName)
        {
            return _context.Users.Where(u => u.Email.Contains(email) && u.UserName.Contains(userName)).Count();
        }

        public EditUserViewModel GetUserInfo(int id)
        {
            return _context.Users.Where(u => u.UserId == id).Select(u => new EditUserViewModel
            {
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive,
                CurrentImage = u.UserAvatar,
                Score = u.Score,
                PhoneNumber = u.PhoeNumber
            }).Single();
        }

        public IEnumerable<Users> GetUsers(int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;
            return _context.Users.OrderByDescending(u => u.UserId).Skip(Skip).Take(Take).ToList();
        }

        public bool IsEmailExist(int id, string email)
        {
            if (_context.Users.Where(u => u.Email == email && u.UserId != id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPhoneNumberExist(int id, string number)
        {
            if (_context.Users.Where(u => u.PhoeNumber == number && u.UserId != id).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
