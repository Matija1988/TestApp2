using Microsoft.EntityFrameworkCore;
using ProjectService.Model;

namespace ProjectService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optons) : base(optons) 
        { 
        
        }

        public DbSet<VehicleMake> VehicleMakers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleMake>().HasData(
                new VehicleMake
                {
                    Id = 1,
                    Name = "Bayerische Motoren Werke AG",
                    Abrv = "BMW"
                }
                ) ;
        }
    }
}
