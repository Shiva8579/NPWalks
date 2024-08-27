using NPWalksAPI.Models.Domain;

namespace NPWalksAPI.Repository
{
    public interface IRegionRepository
    {
       Task<List<Region>> GetAllAsync();
        Task<Region?>GetByIdAsync(Guid Id);
        Task<Region>CreateAsync(Region region);
        Task<Region?>UpdateAsync(Guid Id,Region region);
        Task<Region?>DeleteeAsync(Guid Id);
    }
}
