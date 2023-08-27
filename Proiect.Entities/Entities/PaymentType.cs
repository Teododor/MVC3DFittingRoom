using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class PaymentType
{
    public int Id { get; set; }

    public string? Card { get; set; }

    public bool? Repay { get; set; }

    public virtual ICollection<UserPayment> UserPayments { get; set; } = new List<UserPayment>();
}
