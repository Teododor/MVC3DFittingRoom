using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Iso { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}
