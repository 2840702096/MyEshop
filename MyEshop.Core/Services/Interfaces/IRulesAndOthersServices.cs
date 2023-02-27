using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyEshop.DataLayer.Entities.Other;
using MyEshop.DataLayer.Entities.Rules;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IRulesAndOthersServices
    {
        #region SocialApps

        IEnumerable<SocialApps> GetSocialApps();
        string GetSocialAppInfo(int id);
        void PutALink(string link, int id);

        #endregion

        #region Logoes

        IEnumerable<Logoes> GetLogoes();
        void PutALogo(int id, string currentImage, IFormFile imageName);
        Logoes GetLogoInfo(int id);

        #endregion

        #region PrivacyRules

        PrivacyRules GetPrivacyRules(int id);
        void PutPrivacyRule(int id, string title, string text);

        #endregion

        #region PublishContentRules

        PublishContentRules GetPublishContentRules(int id);
        void PutPublishContentRules(int id, string title, string text);

        #endregion

        #region AboutUs

        AboutUs GetAboutUs(int id);
        void PutAboutUsDucuments(int id, string title, string text);

        #endregion
    }
}