using Proiect.Common;
using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class Image : IEntity
{
    public int Id { get; set; }

    public string? ImageName { get; set; }

    public string? ContentType { get; set; }

    public byte[]? ImageContent { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
