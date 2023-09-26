
using CartItems.Api.Models;
using System;
using System.Threading.Tasks;

namespace CartItems.Api.Interfaces.IPersistence
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<ItemModel> Items { get; }

        IBaseRepository<CartItemModel> CartItems { get; }

        IBaseRepository<UserModel> Users { get; }

        Task<bool> SaveAsync();

        void Dispose();
    }
}
