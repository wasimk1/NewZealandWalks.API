using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int PageNumber = 1, int PageSize = 1000)
        {
            //return await dbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();//without queryable

            //Filtering
            var allquerywalks = dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();
            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery) == false)
            //if (filterOn != "" && filterQuery != "")
            {
                if (filterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    allquerywalks = allquerywalks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) ==false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    allquerywalks = isAscending ? allquerywalks.OrderBy(x => x.Name) : allquerywalks.OrderByDescending(x=>x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    allquerywalks = isAscending ? allquerywalks.OrderBy(x => x.LengthInKm) : allquerywalks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination

            var skipresults = (PageNumber - 1) * PageSize;

            return await allquerywalks.Skip(skipresults).Take(PageSize).ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var getbyId = await dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);

            return getbyId;
        }
        public async Task<Walk> CreateWalkSync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateWalkByIdAsync(Guid id, Walk walk)
        {
            var getwalkbyId = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (getwalkbyId == null)
            {
                return null;
            }
            getwalkbyId.Name = walk.Name;
            getwalkbyId.Description = walk.Description;
            getwalkbyId.LengthInKm = walk.LengthInKm;
            getwalkbyId.WalkImageUrl = walk.WalkImageUrl;
            getwalkbyId.DifficultyId = walk.DifficultyId;
            getwalkbyId.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return getwalkbyId;
        }

        public async Task<Walk?> DeleteWalkByIdAsync(Guid id)
        {
            var GetWalkbyId = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (GetWalkbyId == null)
            {
                return null;
            }
            dbContext.Walks.Remove(GetWalkbyId);
            await dbContext.SaveChangesAsync();
            return GetWalkbyId;
        }
    }
}
