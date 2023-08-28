using Microsoft.AspNetCore.Mvc;
using Proiect.Common.DTOs;

namespace Proiect.WebApp.Code.Base
{
    public class BaseController : Controller
    {
        protected readonly CurrentUserDto CurrentUser;

        public BaseController(ControllerDependencies dependencies)
            : base()
        {
            this.CurrentUser = dependencies.CurrentUser;
        }
    }
}
