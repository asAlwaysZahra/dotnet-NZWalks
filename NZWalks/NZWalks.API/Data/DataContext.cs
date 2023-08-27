using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Difficulty> difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("3a139880-627e-4cc7-a0b9-f8376bd5f5ab"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("5bd76ab1-9491-4f73-bfcc-ae8950123298"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("9c7ddd09-242c-4e99-a7c8-19f899cd2fc0"),
                    Name = "Hard"
                }
            };

            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for regions
            List<Region> regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("88c597da-60c3-4b23-b03b-cd96c5d9bb25"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    ImageUrl = "bop-img"
                },
                new Region()
                {
                    Id = Guid.Parse("c88de96a-0b49-4fbe-afe8-79515f052b37"),
                    Name = "Auckland",
                    Code = "AKL",
                    ImageUrl = "akl-img"
                },
                new Region()
                {
                    Id = Guid.Parse("80d1aa52-e48e-42ae-a49a-ea185151be97"),
                    Name = "Northland",
                    Code = "NTL",
                    ImageUrl = "ntl-image"
                },
                new Region()
                {
                    Id = Guid.Parse("2fd626a6-b60c-43cd-aa9e-0cf11b19e13e"),
                    Name = "Nelson",
                    Code = "NSN",
                    ImageUrl = "nsn-img"
                },
                new Region()
                {
                    Id = Guid.Parse("713e5460-bc6c-45f0-b55c-3b281f618e8b"),
                    Name = "Southland",
                    Code = "STL",
                    ImageUrl = "stl-img"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
