using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation;
using Proiect.BusinessLogic.Implementation.Implementation;
using Proiect.BusinessLogic.Implementation.User.Models;
using Proiect.Common.DTOs;
using Proiect.WebApp.Code.Base;

namespace ProiectAcademie.Controllers
{
	public class UserController : BaseController
    {
        protected readonly UserService Service;
        protected readonly UserAccountService USERACCOUNTSERVICE;

        public UserController(ControllerDependencies dependencies, UserService service, UserAccountService useraccountservice )
           : base(dependencies)
        {
            this.Service = service;
            this.USERACCOUNTSERVICE = useraccountservice;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var users = Service.GetUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult ModifyData()
        {
            return View();
        }

        /*        [Authorize(Roles = "Admin")]*/
        public IActionResult Edit()
        {
            var users = Service.GetUsers();
            return View(users);
        }

        [HttpGet]
/*        [Authorize(Roles = "Admin")]*/
        public IActionResult EditUser(int id)
        {
            var user = Service.GetEditUserModelById(id);
            return View(user);
        }

        public IActionResult RolePermissions()
        {
            return View();
        }

        [HttpPost]
/*        [Authorize(Roles ="Admin")]*/
        public IActionResult EditUser(EditUserModel model)
        {
            if(model == null)
            {
                return View("Error_NotFound");
            }

            Service.UpdateUser(model);
            return RedirectToAction("Edit", "User");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            var model = new AddUserModel();
            return View("AddUser", model);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserModel model)
        {
            Service.AddNewUser(model);
            return RedirectToAction("AddUser", "User");
        }

        [HttpGet]
        public IActionResult DeleteUesr(int id) 
        {
            Service.DeleteUser(id);
            return RedirectToAction("Edit", "user");
        }

        [HttpGet]
        public IActionResult DisplayUserImageIcon()
        {
            var image = USERACCOUNTSERVICE.getUserImageService();
            return File(image,"image/png");
        }


    }
}
