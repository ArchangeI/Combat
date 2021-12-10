using System;
using System.Collections.Generic;
using System.Linq;
using Combat.DAL.Interfaces;
using Combat.DAL.Entities;
using Combat.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace Combat.DAL.Repositories
{
    public class PlayerRepository : IRepository<PlayerDAL>
    {
        private CombatContext db;

        public PlayerRepository(CombatContext context)
        {
            db = context;
        }

        public void Create(PlayerDAL player)
        {
            db.Players.Add(player);
        }

        public void Delete(int id)
        {
            PlayerDAL player = db.Players.Find(id);

            if (player != null)
                db.Players.Remove(player);
        }

        public IEnumerable<PlayerDAL> Find(Func<PlayerDAL, bool> predicate)
        {
            return db.Players.Where(predicate).ToList();
        }

        public PlayerDAL Get(int id) => db.Players.Find(id);
        
        public IEnumerable<PlayerDAL> GetAll() => db.Players;

        public void Update(PlayerDAL player)
        {
            db.Entry(player).State = EntityState.Modified;
        }
    }
}
