using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyEshop.Core.Convertors;
using MyEshop.Core.Generators;
using MyEshop.Core.Security;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.Other;
using MyEshop.DataLayer.Entities.Rules;
using MyEShop.DataLayer.Context;

namespace MyEshop.Core.Services
{
    public class RulesAndOthersServices : IRulesAndOthersServices
    {
        private ShopContext _context;

        public RulesAndOthersServices(ShopContext context)
        {
            _context = context;
        }

        public AboutUs GetAboutUs(int id)
        {
            return _context.AboutUs.Find(id);
        }

        public IEnumerable<Logoes> GetLogoes()
        {
            return _context.Logoes.ToList();
        }

        public Logoes GetLogoInfo(int id)
        {
            return _context.Logoes.Find(id);
        }

        public PrivacyRules GetPrivacyRules(int id)
        {
            return _context.PrivacyRules.Find(id);
        }

        public PublishContentRules GetPublishContentRules(int id)
        {
            return _context.PublishContentRules.Find(id);
        }

        public string GetSocialAppInfo(int id)
        {
            if(_context.SocialApps.Where(s=>s.Id == id).Any())
            {
            return _context.SocialApps.SingleOrDefault(s=>s.Id == id).Link;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<SocialApps> GetSocialApps()
        {
            return _context.SocialApps.ToList();
        }

        public void PutAboutUsDucuments(int id, string title, string text)
        {
            var Ducument = _context.AboutUs.Find(id);

            Ducument.Title = title;
            Ducument.Text = text;
            _context.AboutUs.Update(Ducument);
            _context.SaveChanges();

        }

        public void PutALink(string link, int Id)
        {
            var App = _context.SocialApps.Find(Id);

            App.Link = link;

            _context.SocialApps.Update(App);
            _context.SaveChanges();
        }

        public void PutALogo(int id, string currentImage, IFormFile imageName)
        {
            var Logo = _context.Logoes.Find(id);

           if (imageName != null && imageName.IsImage())
            {
                string ThumPath = "";
                string ImagePath = "";
                if (currentImage != null)
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/LogoImages/", currentImage);
                    FileInfo file = new FileInfo(ImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    ThumPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/LogoThumbnails/", currentImage);
                    FileInfo ThumFile = new FileInfo(ThumPath);
                    if (ThumFile.Exists)
                    {
                        ThumFile.Delete();
                    }
                }
                Logo.ImageName = CodeGenerator.UniqCodeGenerator() + Path.GetExtension(imageName.FileName);
                ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/LogoImages/", Logo.ImageName);

                using (var Stream = new FileStream(ImagePath, FileMode.Create))
                {
                    imageName.CopyTo(Stream);
                }

                ImageConvertor convertor = new ImageConvertor();

                string ThumbPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/images/LogoThumbnails/", Logo.ImageName);
                convertor.Image_resize(ImagePath, ThumbPass, 400);

            }
            else
            {
                Logo.ImageName = currentImage;
            }

            _context.Logoes.Update(Logo);
            _context.SaveChanges();
        }

        public void PutPrivacyRule(int id, string title, string text)
        {
            var Rule = _context.PrivacyRules.Find(id);

            Rule.Text = text;
            Rule.Title = title;

            _context.PrivacyRules.Update(Rule);
            _context.SaveChanges();
        }

        public void PutPublishContentRules(int id, string title, string text)
        {
            var Rule = _context.PublishContentRules.Find(id);

            Rule.Text = text;
            Rule.Title = title;

            _context.PublishContentRules.Update(Rule);
            _context.SaveChanges();
        }
    }
}