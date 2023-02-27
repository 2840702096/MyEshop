using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.Convertors;
using MyEshop.Core.DTOs;
using MyEshop.Core.Generators;
using MyEshop.Core.Security;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Weblogs;
using MyEShop.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services
{
    public class WeblogServices : IWeblogServices
    {
        private ShopContext _context;

        public WeblogServices(ShopContext context)
        {
            _context = context;
        }

        public void Add(Weblog weblog)
        {
            _context.Add(weblog);
            _context.SaveChanges();
        }

        public void AddWeblog(AddWeblogViewModel weblog)
        {
            Weblog NewWeblog = new Weblog();

            NewWeblog.WeblogTitle = weblog.Title;
            NewWeblog.Score = weblog.Score.Value;
            NewWeblog.CategoryId = weblog.CategoryId;
            NewWeblog.WeblogBody = weblog.WeblogBody;
            NewWeblog.CreateDate = DateTime.Now;
            NewWeblog.UserId = 5;

            if (weblog.ImageName != null && weblog.ImageName.IsImage())
            {
                string ImagePath = "";
                NewWeblog.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(weblog.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WeblogImages/", NewWeblog.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    weblog.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WeblogThumbNail/", NewWeblog.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 300);
            }
            Add(NewWeblog);
        }

        public void ConfirmComment(int id)
        {
            var Comment = GetWeblogComments(id);

            Comment.IsConfirmed = true;

            _context.WeblogComments.Update(Comment);
            _context.SaveChanges();
        }

        public void DeleteWeblog(int id)
        {
            var Weblog = GetWeblog(id);

            Weblog.IsDelete = true;
            Edit(Weblog);
        }

        public void Edit(Weblog weblog)
        {
            _context.Update(weblog);
            _context.SaveChanges();
        }

        public void EditWeblog(int id, EditWeblogViewModel weblog, string currentImage)
        {
            var CurrentWeblog = GetWeblog(id);

            CurrentWeblog.WeblogTitle = weblog.Title;
            CurrentWeblog.Score = weblog.Score.Value;
            CurrentWeblog.WeblogBody = weblog.WeblogBody;
            CurrentWeblog.CategoryId = weblog.CategoryId;

            if (weblog.ImageName != null && weblog.ImageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WeblogImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WeblogThumbNail/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                CurrentWeblog.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(weblog.ImageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WeblogImages/", CurrentWeblog.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    weblog.ImageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/WeblogThumbNail/", CurrentWeblog.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 300);

            }
            else
            {
                CurrentWeblog.ImageName = currentImage;
            }
            Edit(CurrentWeblog);
        }

        public IEnumerable<Weblog> FilterWeblogs(string weblogTitle, int pageId)
        {
            int Take = 15;
            int Skip = (pageId - 1) * Take;
            return _context.Weblogs.Where(w => w.WeblogTitle.Contains(weblogTitle)).ToList();
        }

        public IEnumerable<Weblog> GetAllWeblogs(int pageId)
        {
            int take = 15;
            int skip = (pageId - 1) * take;

            return _context.Weblogs.OrderByDescending(w => w.CreateDate).Skip(skip).Take(take).ToList();
        }

        public List<SelectListItem> GetCategories()
        {
            return _context.WeblogCategories.Where(w => w.ParentId != null).Select(w => new SelectListItem
            {
                Text = w.Title,
                Value = w.CategoryId.ToString()
            }).ToList();
        }

        public int GetCountOfFilteredWeblogs(string weblogTitle)
        {
            return _context.Weblogs.Where(w => w.WeblogTitle.Contains(weblogTitle)).Count();
        }

        public int GetCountOfWeblogComments()
        {
            return _context.WeblogComments.Where(w => w.IsConfirmed == false).Count();
        }

        public int GetCountOfWeblogs()
        {
            return _context.Weblogs.Count();
        }

        public Weblog GetWeblog(int id)
        {
            return _context.Weblogs.Find(id);
        }

        public WeblogComments GetWeblogComments(int id)
        {
            return _context.WeblogComments.Where(w => w.IsConfirmed == false).Single();
        }

        public string GetWeblogCategoryTitle(int categoryId)
        {
            return _context.WeblogCategories.SingleOrDefault(w => w.CategoryId == categoryId).Title;
        }

        public EditWeblogViewModel GetWeblogInfoForEdit(int id)
        {
            return _context.Weblogs.Where(w => w.WeblogId == id).Select(w => new EditWeblogViewModel
            {
                Title = w.WeblogTitle,
                WeblogBody = w.WeblogBody,
                CategoryId = w.CategoryId,
                CurrentImage = w.ImageName,
                Score = w.Score,
            }).Single();
        }

        public IEnumerable<WeblogComments> WeblogComments(int pageId)
        {
            int take = 1;
            int skip = (pageId - 1) * take;

            return _context.WeblogComments.Where(w => w.IsConfirmed == false).OrderByDescending(w => w.CommentId).Skip(skip).Take(take).ToList();
        }
    }
}
