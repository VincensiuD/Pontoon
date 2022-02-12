using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pontoon.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Wager> Wagers { get; set; }
        public DbSet<CardSequence> CardSequences { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)

        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new DataSeeder(builder);
        }
    }
}