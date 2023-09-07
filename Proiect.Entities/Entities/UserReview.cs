using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class UserReview
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int? StarsGiven { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
