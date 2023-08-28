using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class OrderItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int NumberOfItemsBought { get; set; }

    public virtual OrderDetail Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
