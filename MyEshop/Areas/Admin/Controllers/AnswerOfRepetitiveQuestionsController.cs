using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Core.DTOs;
using MyEshop.Core.Services.Interfaces;
using System;
using System.IO;

namespace MyEShop.Areas.Admin.Controllers
{
    public class AnswerOfRepetitiveQuestionsController : Controller
    {
        private IAnswerOfRepetitiveQuestions _answerOfRepetitiveQuestions;

        public AnswerOfRepetitiveQuestionsController(IAnswerOfRepetitiveQuestions answerOfRepetitiveQuestions)
        {
            _answerOfRepetitiveQuestions = answerOfRepetitiveQuestions;
        }

        [Area("Admin")]
        [Route("/Admin/AnswerOfRepetitiveQuestions")]
        public IActionResult Index(int pageId = 1)
        {
            ViewBag.PageId = pageId;
            ViewBag.PageCount = _answerOfRepetitiveQuestions.GetCount() / 7;
            return View(_answerOfRepetitiveQuestions.GetAll(pageId));
        }

        #region CreateAnswerOfRepetitiveQuestions

        [Area("Admin")]
        [Route("/Admin/CreateAnswerOfRepetitiveQuestions")]
        public IActionResult CreateAnswerOfRepetitiveQuestions()
        {
            ViewBag.Categorie = new SelectList(_answerOfRepetitiveQuestions.GetCategories(),"Value","Text");
            return View();
        }

        [Area("Admin")]
        [Route("/Admin/CreateAnswerOfRepetitiveQuestions")]
        [HttpPost]
        public IActionResult CreateAnswerOfRepetitiveQuestions(AnswerOfRepetitiveQuestionsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorie = new SelectList(_answerOfRepetitiveQuestions.GetCategories(), "Value", "Text");
                return View(model);
            }

            _answerOfRepetitiveQuestions.Add(model);

            return Redirect("/Admin/AnswerOfRepetitiveQuestions");
        }

        #endregion

        #region EditAnswerOfRepetitiveQuestions

        [Area("Admin")]
        [Route("/Admin/EditAnswerOfRepetitiveQuestions/{id}")]
        public IActionResult EditAnswerOfRepetitiveQuestions(int id)
        {
            var Model = _answerOfRepetitiveQuestions.GetInfoForEdit(id);

            ViewBag.Categorie = new SelectList(_answerOfRepetitiveQuestions.GetCategories(), "Value", "Text",Model.CategoryId);

            return View(Model);
        }

        [Area("Admin")]
        [Route("/Admin/EditAnswerOfRepetitiveQuestions/{id}")]
        [HttpPost]
        public IActionResult EditAnswerOfRepetitiveQuestions(int id, AnswerOfRepetitiveQuestionsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorie = new SelectList(_answerOfRepetitiveQuestions.GetCategories(), "Value", "Text", model.CategoryId);
                return View(model);
            }

            _answerOfRepetitiveQuestions.Edit(model, id);

            return Redirect("/Admin/AnswerOfRepetitiveQuestions");
        }

        #endregion

        #region DeleteAnswerOfRepetitiveQuestions

        [Area("Admin")]
        [Route("/Admin/DeleteAnswerOfRepetitiveQuestions/{id}")]
        public IActionResult DeleteAnswerOfRepetitiveQuestions(int id)
        {
            _answerOfRepetitiveQuestions.Delete(id);
            return Redirect("/Admin/AnswerOfRepetitiveQuestions");
        }

        #endregion

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/MyImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }

    }
}
