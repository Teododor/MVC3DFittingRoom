using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class UserAction
{
    public int Id { get; set; }

    public int CreatedBy { get; set; }

    public int ModifiedBy { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
