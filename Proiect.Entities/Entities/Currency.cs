using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class Currency
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Code { get; set; }

    public string? CurrencySymbol { get; set; }

    public decimal? ExchangeRate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
