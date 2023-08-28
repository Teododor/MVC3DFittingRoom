using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal? Total { get; set; }

    public decimal? OrderTime { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
