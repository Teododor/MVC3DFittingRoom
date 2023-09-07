using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.User.Models
{
    public class EditUserByAdminModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string? ModifiedPassword { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public byte[]? Image { get; set; }
    }
}
