using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation.Image;
using Proiect.DataAccess;
using Proiect.WebApp.Code.Base;

namespace ProiectAcademie.Controllers
{
    public class ImageController : Controller
    {

        private readonly ImageService ImageService;

        public ImageController(ControllerDependencies dependencies, ImageService imageService) 
        {
            this.ImageService = imageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DownloadImage(int imageId)
        {
            var image = ImageService.GetImageById(imageId);
            if (image is not null)
                return File(image, "image/png");
            else
            {
                image = ImageService.GetImageById(imageId);
                return File(image, "image/png");
            }
        }
    }
}
