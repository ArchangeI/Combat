using Combat.DAL.EF;
using Combat.DAL.Entities;
using Combat.DAL.Interfaces;
using System;

namespace Combat.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private CombatContext db; /*= new CombatContext()*/
        private PlayerRepository playerRepository;

        public IRepository<PlayerDAL> Players
        {
            get
            {
                if (playerRepository == null)
                    playerRepository = new PlayerRepository(db);
                return playerRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
