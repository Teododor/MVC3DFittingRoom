using Proiect.Common;
using Proiect.Entities;

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
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

    }
}
