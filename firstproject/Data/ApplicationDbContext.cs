using Microsoft.EntityFrameworkCore;
using firstproject.Models;

namespace firstproject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ): base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        public DbSet<VillaNumber> VillaNumbers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "royal villa",
                    Details = "azeazeazeazeazeazeazeazeazeaz",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Name = "azetyyutyut",
                    Details = "tyututyutyutyuytu",
                    ImageUrl = "",
                    Occupancy = 6,
                    Rate = 100,
                    Sqft = 350,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                }
                );
        }
    }
}
