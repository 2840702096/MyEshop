using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.DataLayer.Entities.AnswerToRepetitiveQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IAnswerOfRepetitiveQuestions
    {
        IEnumerable<RepetitiveQuestions> GetAll(int pageId);
        int GetCount();
        string GetCategoryTitle(int categoryId);
        List<SelectListItem> GetCategories();
        void Add(AnswerOfRepetitiveQuestionsViewModel model);
        AnswerOfRepetitiveQuestionsViewModel GetInfoForEdit(int id);
        void Edit(AnswerOfRepetitiveQuestionsViewModel model, int id);
        void Delete(int id);
    }
}
