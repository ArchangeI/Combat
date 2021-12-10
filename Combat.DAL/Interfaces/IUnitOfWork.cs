using Combat.DAL.Entities;
using System;

namespace Combat.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<PlayerDAL> Players { get; }

        public void Save();
    }
}
