using Proiect.BusinessLogic.Implementation.User;
using Proiect.Common.DTOs;

namespace Proiect.WebApp.Code.Base
{
    public class ControllerDependencies
    {
        public CurrentUserDto CurrentUser { get; set; }
        public ControllerDependencies(CurrentUserDto currentUser)
        {
            this.CurrentUser = currentUser;
           
        }
    }
}