using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy=null, bool isAscending=true, int PageNumber=1, int PageSize=1000);
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk> CreateWalkSync(Walk walk);
        Task<Walk?> UpdateWalkByIdAsync(Guid id, Walk walk);
        Task<Walk?> DeleteWalkByIdAsync(Guid id);
    }
}
