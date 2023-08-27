using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation.Comment;
using Proiect.WebApp.Code.Base;

namespace ProiectAcademie.Controllers
{
    public class CommentController : Controller
    {
        protected readonly CommentService Service;

        public CommentController(ControllerDependencies dependencies, CommentService service)
        {
            this.Service = service;
        }
        public IActionResult Index()
        {
            var comments = Service.GetComments();   
            return View(comments);
        }
    }
}
