using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class UserProduct
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime? AddDate { get; set; }

    public int? Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
