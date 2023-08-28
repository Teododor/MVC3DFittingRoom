using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class AddressLine
{
    public int Id { get; set; }

    public string Street { get; set; } = null!;

    public string? Block { get; set; }

    public int Number { get; set; }

    public string? Entrance { get; set; }

    public string PostalCode { get; set; } = null!;

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}
