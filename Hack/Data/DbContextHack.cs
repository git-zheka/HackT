using Hack.Models;
using Microsoft.EntityFrameworkCore;

namespace Hack.Data
{
    public class DbContextHack : DbContext
    {
        public DbContextHack(DbContextOptions options) : base(options)
        {
        }

        public DbSet<HistoryCryptoEvent> HistoryCryptoEvents { get; set; }
        public DbSet<HistoryEvent> HistoryEvents { get; set; }
        public DbSet<UserCashState> UserCashStates { get; set; }
        public DbSet<UserCryptoState> UserCryptoStates { get; set; }
    }
}
