using Microsoft.EntityFrameworkCore;
using NPWalksAPI.Data;
using NPWalksAPI.Models.Domain;

namespace NPWalksAPI.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NPWalksDbContext _context;
        public RegionRepository(NPWalksDbContext context = null)
        {
            _context = context;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteeAsync(Guid Id)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingRegion == null)
            {
                return null;
            }
             _context.Regions.Remove(existingRegion);
            await _context.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid Id)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == Id);
        }

        public async Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id ==Id);
            if (existingRegion==null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();
            return existingRegion;
        }
    }
}
