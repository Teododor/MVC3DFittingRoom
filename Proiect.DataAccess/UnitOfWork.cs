using Proiect.Common;
using Proiect.Entities;
using System.Security;

namespace Proiect.DataAccess
{
    public class UnitOfWork
    {
        private readonly DbProiectAtiContext Context;
       
        public UnitOfWork(DbProiectAtiContext context)
        {
            this.Context = context;
        }

        private IRepository<User> users;

        public IRepository<User> Users => users ?? (users = new BaseRepository<User>(Context));

        private IRepository<Product> products;

        public IRepository<Product> Products => products ?? (products = new BaseRepository<Product>(Context));

        private IRepository<Comment> comments;
        public IRepository <Comment> Comments => comments ?? (comments = new BaseRepository<Comment>(Context));

        public IRepository<FavoriteProduct> favoriteProducts;

        public IRepository<FavoriteProduct> FavoriteProducts => favoriteProducts ?? (favoriteProducts = new BaseRepository<FavoriteProduct>(Context));

        public IRepository<UserProduct> userProducts;

        public IRepository<UserProduct> UserProducts => userProducts ?? (userProducts = new BaseRepository<UserProduct>(Context));

        public IRepository<Image> images;

        public IRepository<Image> Images => images ?? (images = new BaseRepository<Image>(Context));

        public IRepository<Permission> permissions;
        public IRepository<Permission> Permissions => permissions ?? (permissions = new BaseRepository<Permission>(Context));

        public IRepository<Role> roles;
        public IRepository<Role> Roles => roles ?? (roles = new BaseRepository<Role>(Context));

        public IRepository<RolePermission> rolepermissions;
        public IRepository<RolePermission> RolePermissions => rolepermissions ?? (rolepermissions = new BaseRepository<RolePermission>(Context));

        public IRepository<UserRoles> userroles;
        public IRepository <UserRoles> UserRoles => userroles ?? (userroles = new BaseRepository<UserRoles>(Context));

        public IRepository<UserReview> userreviews;
        public IRepository<UserReview> UserReviews => userreviews ?? (userreviews = new BaseRepository<UserReview>(Context));
        
        public IRepository<ProductInfo> productinfo;
        public IRepository<ProductInfo> ProductInfo => productinfo ?? (productinfo = new BaseRepository<ProductInfo>(Context));

        public IRepository<Country> country;
        public IRepository <Country> Country => country ?? (country = new BaseRepository<Country>(Context));     
        
        public IRepository <City> city;
        public IRepository<City> City => city ?? (city = new BaseRepository<City>(Context));

        public IRepository<AddressLine> addressline;

        public IRepository<AddressLine> AddressLine =>
            addressline ?? (addressline = new BaseRepository<AddressLine>(Context));


        public IRepository<UserAddress> useraddress;

        public IRepository<UserAddress> UserAddress =>
            useraddress ?? (useraddress = new BaseRepository<UserAddress>(Context));
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

    }
}
