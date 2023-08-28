using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class Comment
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string? Review { get; set; }

    public int? Mark { get; set; }

    public int? LastModifiedBy { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
