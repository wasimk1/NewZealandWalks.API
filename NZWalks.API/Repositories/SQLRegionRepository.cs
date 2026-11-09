using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            var getallRegions = await dbContext.Regions.ToListAsync();
            return getallRegions;
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            var getregionbyid = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return getregionbyid;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegionByIdAsync(Guid id, Region region)
        {
            var existregion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existregion == null)
            {
                return null;
            }
            existregion.Code= region.Code;
            existregion.Name= region.Name;
            existregion.RegionImageUrl= region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existregion;

        }

        public async Task<Region?> DeleteRegionByIdSync(Guid id)
        {
            var getregionbyid = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(getregionbyid == null)
            {
                return null;
            }
            dbContext.Regions.Remove(getregionbyid);
            await dbContext.SaveChangesAsync();
            return getregionbyid;
        }
    }
}
