using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class ProductInfo
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
