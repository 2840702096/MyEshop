using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using MyEshop.DataLayer.Entities.AnswerToRepetitiveQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEShop.DataLayer.Context;

namespace MyEshop.Core.Services
{
    public class AnswerOfRepetitiveQuestions : IAnswerOfRepetitiveQuestions
    {
        private ShopContext _context;

        public AnswerOfRepetitiveQuestions(ShopContext context)
        {
            _context = context;
        }

        public void Add(AnswerOfRepetitiveQuestionsViewModel model)
        {
            RepetitiveQuestions NewRepetitiveQuestions = new RepetitiveQuestions();
            NewRepetitiveQuestions.AnswerBody = model.AnswerBody;
            NewRepetitiveQuestions.QuestionText = model.QuestionText;
            NewRepetitiveQuestions.CategoryId = model.CategoryId;
            NewRepetitiveQuestions.IsRepetitive = model.IsRepetitive;

            _context.RepetitiveQuestions.Add(NewRepetitiveQuestions);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Entry(_context.RepetitiveQuestions.Find(id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Edit(AnswerOfRepetitiveQuestionsViewModel model, int id)
        {
            var EditModel = _context.RepetitiveQuestions.Find(id);

            EditModel.QuestionText = model.QuestionText;
            EditModel.CategoryId = model.CategoryId;
            EditModel.IsRepetitive = model.IsRepetitive;
            EditModel.AnswerBody = model.AnswerBody;

            _context.RepetitiveQuestions.Update(EditModel);
            _context.SaveChanges();
        }

        public IEnumerable<RepetitiveQuestions> GetAll(int pageId)
        {
            int Take = 7;
            int Skip = (pageId - 1) * Take;

            return _context.RepetitiveQuestions.OrderByDescending(r => r.Id).Skip(Skip).Take(Take).ToList();
        }

        public List<SelectListItem> GetCategories()
        {
            return _context.RepetitiveQuestionCategories.Select(r => new SelectListItem
            {
                Text = r.CategoryTitle,
                Value = r.CategoryId.ToString()
            }).ToList();
        }

        public string GetCategoryTitle(int categoryId)
        {
            return _context.RepetitiveQuestionCategories.SingleOrDefault(r => r.CategoryId == categoryId).CategoryTitle;
        }

        public int GetCount()
        {
            return _context.RepetitiveQuestions.Count();
        }

        public AnswerOfRepetitiveQuestionsViewModel GetInfoForEdit(int id)
        {
            return _context.RepetitiveQuestions.Where(r => r.Id == id).Select(r => new AnswerOfRepetitiveQuestionsViewModel
            {
                QuestionText = r.QuestionText,
                AnswerBody = r.AnswerBody,
                CategoryId = r.CategoryId,
                IsRepetitive = r.IsRepetitive
            }).Single();
        }
    }
}
