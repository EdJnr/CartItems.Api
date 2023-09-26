using CartItems.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CartItems.Api.Database
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
        {
        }

        public DbSet<CartItemModel> CartItem { get; set; }

        public DbSet<ItemModel> Item { get; set; }

        public DbSet<UserModel> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //relationships
            modelBuilder.Entity<CartItemModel>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId);

            modelBuilder.Entity<CartItemModel>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);

            //primary keys
            modelBuilder.Entity<ItemModel>()
                .HasKey(item => item.ItemId);

            modelBuilder.Entity<UserModel>()
                .HasKey(user => user.UserId);

            modelBuilder.Entity<CartItemModel>()
                .HasKey(cartItem => cartItem.CartId);
        }

    }
}
