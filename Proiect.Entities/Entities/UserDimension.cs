using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class UserDimension
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int DimensionsId { get; set; }

    public virtual Dimension Dimensions { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
