using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class UserAddress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AddressLineId { get; set; }

    public int CountryId { get; set; }

    public int CityId { get; set; }

    public string? MobileNo { get; set; }

    public virtual AddressLine AddressLine { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
