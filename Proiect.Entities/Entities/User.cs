using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telephone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public string? ChatColor { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime? BirthDate { get; set; }

    public byte[]? Image { get; set; }

    public int? ImageId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();

    public virtual Image? ImageNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Product> ProductCreatedByNavigations { get; set; } = new List<Product>();

    public virtual ICollection<Product> ProductModifiedByNavigations { get; set; } = new List<Product>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserDimension> UserDimensions { get; set; } = new List<UserDimension>();

    public virtual ICollection<UserPayment> UserPayments { get; set; } = new List<UserPayment>();

    public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
