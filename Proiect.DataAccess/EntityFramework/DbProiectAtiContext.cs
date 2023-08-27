using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proiect.Entities;

namespace Proiect.DataAccess;

public partial class DbProiectAtiContext : DbContext
{
    public DbProiectAtiContext()
    {
    }

    public DbProiectAtiContext(DbContextOptions<DbProiectAtiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressLine> AddressLines { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Dimension> Dimensions { get; set; }

    public virtual DbSet<FavoriteProduct> FavoriteProducts { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDiscount> ProductDiscounts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TestTable> TestTables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserDimension> UserDimensions { get; set; }

    public virtual DbSet<UserPayment> UserPayments { get; set; }

    public virtual DbSet<UserProduct> UserProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-DLKH69L\\SQLEXPRESS;Initial Catalog=dbProiectATI;Integrated Security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressLine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AddressL__3214EC0727BA9AC4");

            entity.ToTable("AddressLine");

            entity.Property(e => e.Block).HasMaxLength(100);
            entity.Property(e => e.Entrance).HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasMaxLength(100);
            entity.Property(e => e.Street).HasMaxLength(100);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3214EC078B529D4A");

            entity.ToTable("City");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId }).HasName("PK__Comment__DCC800208DB47066");

            entity.ToTable("Comment");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Review).HasMaxLength(2500);

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Comment__Product__7D98A078");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comment__UserId__7CA47C3F");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC0770EF5982");

            entity.ToTable("Country");

            entity.Property(e => e.Iso)
                .HasMaxLength(50)
                .HasColumnName("ISO");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Currency__3214EC07EFC35484");

            entity.ToTable("Currency");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CurrencySymbol)
                .HasMaxLength(10)
                .HasColumnName("currency_symbol");
            entity.Property(e => e.ExchangeRate)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("exchange_rate");
            entity.Property(e => e.FullName).HasMaxLength(250);
        });

        modelBuilder.Entity<Dimension>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dimensio__3214EC0725D09F74");

            entity.Property(e => e.BustSize).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HipSize).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WaistSize).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<FavoriteProduct>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId });

            entity.Property(e => e.DateAddFavProduct).HasColumnType("date");

            entity.HasOne(d => d.Product).WithMany(p => p.FavoriteProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__FavoriteP__Produ__00750D23");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteProducts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteP__UserI__7F80E8EA");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ContentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageName)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC0755CC902E");

            entity.Property(e => e.OrderTime).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ORDERDETAILS_USER");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC079D9F7EC7");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_ORDERITEMS_ORDERDETAILS");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDERITEMS_PRODUCT");

            entity.HasOne(d => d.User).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDERITEMS_USER");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentT__3214EC076CA1B7FA");

            entity.ToTable("PaymentType");

            entity.Property(e => e.Card).HasMaxLength(20);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07B81D14B0");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07BFE1D129");

            entity.ToTable("Product");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_USER_CB");

            entity.HasOne(d => d.Currency).WithMany(p => p.Products)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_CURRENCY");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ProductModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_USER_MB");

            entity.HasOne(d => d.ProductDiscountNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductDiscount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_DISCOUNT");
        });

        modelBuilder.Entity<ProductDiscount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductD__3214EC0745FEA952");

            entity.ToTable("ProductDiscount");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");
            entity.Property(e => e.DeletedAt).HasColumnType("smalldatetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DiscountPercent).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ModifiedAt).HasColumnType("smalldatetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0739843F69");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "UC_Name").IsUnique();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK_ROLEPERMISSIONS_PERMISSIONS"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_ROLEPERMISSIONS_ROLE"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId");
                        j.ToTable("RolePermissions");
                    });

            entity.HasMany(d => d.Users).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserRoles__UserI__0BE6BFCF"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__UserRoles__RoleI__0AF29B96"),
                    j =>
                    {
                        j.HasKey("RoleId", "UserId").HasName("PK__UserRole__5B8242DE13527BC3");
                        j.ToTable("UserRoles");
                    });
        });

        modelBuilder.Entity<TestTable>(entity =>
        {
            entity.ToTable("TestTable");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07B648AA78");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UC_User_Email").IsUnique();

            entity.Property(e => e.BirthDate)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnType("date")
                .HasColumnName("birthDate");
            entity.Property(e => e.ChatColor).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(500);
            entity.Property(e => e.LastName).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(2500);
            entity.Property(e => e.Telephone).HasMaxLength(100);

            entity.HasOne(d => d.ImageNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_User_User");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAddr__3214EC07EA91ED74");

            entity.ToTable("UserAddress");

            entity.Property(e => e.MobileNo).HasMaxLength(100);

            entity.HasOne(d => d.AddressLine).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.AddressLineId)
                .HasConstraintName("FK_USERADDRESS_ADDRESSLINE");

            entity.HasOne(d => d.City).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_USERADDRESS_CITY");

            entity.HasOne(d => d.Country).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_USERADDRESS_COUNTRY");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USERADDRESS_USER");
        });

        modelBuilder.Entity<UserDimension>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserDime__3214EC0791A5A11F");

            entity.HasOne(d => d.Dimensions).WithMany(p => p.UserDimensions)
                .HasForeignKey(d => d.DimensionsId)
                .HasConstraintName("FK_USERDIMENSIONS_DIMENSIONS");

            entity.HasOne(d => d.User).WithMany(p => p.UserDimensions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USERDIMENSIONS_USER");
        });

        modelBuilder.Entity<UserPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPaym__3214EC07E822C4FF");

            entity.ToTable("UserPayment");

            entity.Property(e => e.AccountNo).HasMaxLength(100);
            entity.Property(e => e.Expiry).HasColumnType("date");
            entity.Property(e => e.Provider).HasMaxLength(100);

            entity.HasOne(d => d.PaymentType).WithMany(p => p.UserPayments)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("FK_USERPAYMENT_PAYMENTTYPE");

            entity.HasOne(d => d.User).WithMany(p => p.UserPayments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USERPAYMENT_USER");
        });

        modelBuilder.Entity<UserProduct>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId }).HasName("PK__UserProd__DCC80020C4D6F8A4");

            entity.Property(e => e.AddDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Product).WithMany(p => p.UserProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__UserProdu__Produ__33F4B129");

            entity.HasOne(d => d.User).WithMany(p => p.UserProducts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserProdu__UserI__33008CF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
