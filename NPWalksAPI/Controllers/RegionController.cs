using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPWalksAPI.Data;
using NPWalksAPI.Models.Domain;
using NPWalksAPI.Models.DTO;

namespace NPWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NPWalksDbContext _dbContext;
        public RegionController(NPWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Get all regions
        // Get : https://localhost:portnumber/api/Region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from model --Domain Model
            var regionsdomain = await _dbContext.Regions.ToListAsync();

            //Map Domain to DTOs
            var regiosnDto = new List<RegionDTO>();
            foreach(var regionDomains in regionsdomain)
            {
                regiosnDto.Add(new RegionDTO()
                {
                    Id = regionDomains.Id,
                    Name = regionDomains.Name,
                    Code = regionDomains.Code,
                    RegionImageUrl = regionDomains.RegionImageUrl
                });
            }
            //return to client
            return Ok(regiosnDto);
        }

        //Get region by Id
        // Get : https://localhost:portnumber/api/Region/{Id}
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid Id)
        {
            //var region = _dbContext.Regions.Find(Id);
            //Get Region domain from databse
            var regionDomains = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            {
                if(regionDomains == null)
                {
                    return NotFound();
                }
                //Map/Convert region domain to region dtos
                var regiondto = new RegionDTO
                {
                    Id = regionDomains.Id,
                    Name = regionDomains.Name,
                    Code = regionDomains.Code,
                    RegionImageUrl = regionDomains.RegionImageUrl
                };
                return Ok(regiondto);
            }
        }
        //Post to create region
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequest addRegion)
        {
            //Map or Convert Dto to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegion.Code,
                Name = addRegion.Name,
                RegionImageUrl = addRegion.RegionImageUrl
            };
            await _dbContext.Regions.AddAsync(regionDomainModel);
            await _dbContext.SaveChangesAsync();

            //Map Domain model back to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetRegionById),new {Id = regionDto.Id}, regionDto);
        }
        //PUT to update the region
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid Id, [FromBody] UpdateRegionDto updateRegion)
        {
            var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Map dto to domain model
            regionDomainModel.Code = updateRegion.Code;
            regionDomainModel.Name = updateRegion.Name;
            regionDomainModel.RegionImageUrl = updateRegion.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            //Convert Domain model to dto
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl=regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
        //Delete the region
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteRegion(Guid Id)
        {
            var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            _dbContext.Regions.Remove(regionDomainModel);
            await _dbContext.SaveChangesAsync();

            //return delete data bacnk
            //map to region dto
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }
    }
}
