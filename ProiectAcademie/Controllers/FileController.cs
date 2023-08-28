using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation.Image;
using Proiect.DataAccess;
using Proiect.WebApp.Code.Base;

namespace ProiectAcademie.Controllers
{
    public class FileController : Controller
    {

        private readonly ImageService ImageService;

        public FileController(ControllerDependencies dependencies, ImageService imageService)
        {
            this.ImageService = imageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public byte[] DownloadImage(int imageId)
        {
            return ImageService.GetImageById(imageId);
        }
    }
}
