using CartItems.Api.Database;
using CartItems.Api.Interfaces.IPersistence;
using CartItems.Api.Models;
using System;
using System.Threading.Tasks;

namespace CartItems.Api.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDatabaseContext _dbContext;

        public UnitOfWork(ApplicationDatabaseContext context)
        {
            _dbContext = context;
        }

        public IBaseRepository<ItemModel> Items => new BaseRepository<ItemModel>(_dbContext);

        public IBaseRepository<CartItemModel> CartItems => new BaseRepository<CartItemModel>(_dbContext);

        public IBaseRepository<UserModel> Users => new BaseRepository<UserModel>(_dbContext);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
