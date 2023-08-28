using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class Dimension
{
    public int Id { get; set; }

    public decimal? BustSize { get; set; }

    public decimal? WaistSize { get; set; }

    public decimal? HipSize { get; set; }

    public virtual ICollection<UserDimension> UserDimensions { get; set; } = new List<UserDimension>();
}
