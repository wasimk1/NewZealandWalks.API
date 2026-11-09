using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> Options) : base(Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var ReaderRoleId = "53bf5895-5fa6-4c80-85cb-488bab75a1f5";
            var WriterRoleId = "41a6c829-0088-4e48-81bc-a1a31880c28a";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole{
                    Id=ReaderRoleId,
                    ConcurrencyStamp=ReaderRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole{
                    Id=WriterRoleId,
                    ConcurrencyStamp=WriterRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
