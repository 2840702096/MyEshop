using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.DataLayer.Entities.Weblogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IWeblogServices
    {

        #region Weblogs

        IEnumerable<Weblog> GetAllWeblogs(int pageId);
        int GetCountOfWeblogs();
        void AddWeblog(AddWeblogViewModel weblog);
        void Add(Weblog weblog);
        List<SelectListItem> GetCategories();
        string GetWeblogCategoryTitle(int categoryId);
        EditWeblogViewModel GetWeblogInfoForEdit(int id);
        void EditWeblog(int id, EditWeblogViewModel weblog, string currentImage);
        void Edit(Weblog weblog);
        Weblog GetWeblog(int id);
        void DeleteWeblog(int id);
        IEnumerable<Weblog> FilterWeblogs(string weblogTitle, int pageId);
        int GetCountOfFilteredWeblogs(string weblogTitle);

        #endregion

        #region WeblogComments

        public IEnumerable<WeblogComments> WeblogComments(int pageId);
        int GetCountOfWeblogComments();
        WeblogComments GetWeblogComments(int id);
        void ConfirmComment(int id);

        #endregion

    }
}
