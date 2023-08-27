using System;
using System.Collections.Generic;

namespace Proiect.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int CurrencyId { get; set; }

    public int? StockNo { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int CreatedBy { get; set; }

    public int ModifiedBy { get; set; }

    public int ProductDiscount { get; set; }

    public int ProductInventory { get; set; }

    public int ProductDimensions { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;

    public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();

    public virtual User ModifiedByNavigation { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ProductDiscount ProductDiscountNavigation { get; set; } = null!;

    public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
}
