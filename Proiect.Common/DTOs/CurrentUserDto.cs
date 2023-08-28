using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Common.DTOs
{
    public class CurrentUserDto
    {
        public CurrentUserDto()
        {
            Roles = new List<string>();
        }

        public int Id { get; set; } 
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }

        public List<string> Roles { get; set; }

    }
}
