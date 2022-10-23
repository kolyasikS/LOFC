using Microsoft.EntityFrameworkCore;
using BLL.Entities;

namespace DAL.DataBase
{
    public class DALContext : DbContext
    {
        public DbSet<League> Leagues { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Couch> Couches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DALContext() { }
        public DALContext(DbContextOptions<DALContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;User Id=postgres;Password=optimus;Database=LOFC;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>().HasOne(club => club.Owner).WithOne(owner => owner.Club).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
