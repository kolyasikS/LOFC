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
        public DALContext() { }
        public DALContext(DbContextOptions<DALContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;User Id=postgres;Password=optimus;Database=LOFC;");
        }
    }
}
