using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class UserPayment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PaymentTypeId { get; set; }

    public string? Provider { get; set; }

    public string? AccountNo { get; set; }

    public DateTime? Expiry { get; set; }

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
