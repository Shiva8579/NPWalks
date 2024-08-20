using Microsoft.EntityFrameworkCore;
using NPWalksAPI.Models.Domain;

namespace NPWalksAPI.Data
{
    public class NPWalksDbContext : DbContext
    {
        public NPWalksDbContext(DbContextOptions dbContextOptions): base (dbContextOptions)
        {
                
        }
        public  DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions{ get; set; }
        public DbSet<Walk> Walks{ get; set; }
    }
}
