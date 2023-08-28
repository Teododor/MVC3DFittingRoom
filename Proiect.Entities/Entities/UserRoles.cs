using Proiect.Common;
using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class UserRoles : IEntity
{
    public int RoleId { get; set; }

    public int UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
