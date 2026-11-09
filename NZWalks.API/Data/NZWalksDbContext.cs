using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        //public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options) { 

        //}
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties //Initial values, inserting defualt data into the database
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>
            {
                new Difficulty
                {
                    Id=Guid.Parse("05c3ecb3-2140-442b-b6d0-f5ae363d0852"), //Hardcoded
                    Name="Easy"
                },
                new Difficulty
                {
                    Id=Guid.Parse("c8a3553f-c5d9-48c1-a166-7bb29e40cf58"),
                    Name="Medium"
                },
                new Difficulty
                {
                    Id=Guid.Parse("341dffdd-be80-4697-b6e6-7eb220a1ac9d"),
                    Name="Hard"
                }

            };
            //Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>
            {

                new Region
                {
                    Id=Guid.Parse("2b18d4a3-a4ca-4603-8e5e-ab337221b0ea"),
                    Name="Auckland",
                    Code="AUK",
                    RegionImageUrl="aukland-image.png"
                },
                new Region
                {
                    Id=Guid.Parse("93db5ab0-e66d-4d49-9b44-ca94538f52b2"),
                    Name="Wellington",
                    Code="WGN",
                    RegionImageUrl="Wellington-image.png"
                },
                 new Region
                {
                    Id=Guid.Parse("570181fe-2035-4ff2-9638-abab61a78489"),
                    Name="Northland",
                    Code="NL",
                    RegionImageUrl="Northland-image.png"
                },
                  new Region
                {
                    Id=Guid.Parse("a3ba384f-6e56-451f-bad0-fa206f0cdeec"),
                    Name="Southland",
                    Code="STL",
                    RegionImageUrl="Southland-image.png"
                },
                   new Region
                {
                    Id=Guid.Parse("86b65284-6442-4539-a2d9-69a0b29d58d2"),
                    Name="West Coast",
                    Code="WTC",
                    RegionImageUrl="West-Coast-image.png"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
