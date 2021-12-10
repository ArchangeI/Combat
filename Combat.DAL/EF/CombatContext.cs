using Combat.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Combat.DAL.EF
{
    public class CombatContext : DbContext
    {
        public DbSet<PlayerDAL> Players { get; set; }

        public CombatContext(DbContextOptions<CombatContext> options)
            : base(options)
        {

        }
    }
}
