using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPWalksAPI.Data;
using NPWalksAPI.Models.Domain;
using NPWalksAPI.Models.DTO;
using NPWalksAPI.Repository;

namespace NPWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NPWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionController(NPWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        //Get all regions
        // Get : https://localhost:portnumber/api/Region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from model --Domain Model
            var regionsdomain = await _regionRepository.GetAllAsync();

            ////Map Domain to DTOs
            //var regiosnDto = new List<RegionDTO>();
            //foreach(var regionDomains in regionsdomain)
            //{
            //    regiosnDto.Add(new RegionDTO()
            //    {
            //        Id = regionDomains.Id,
            //        Name = regionDomains.Name,
            //        Code = regionDomains.Code,
            //        RegionImageUrl = regionDomains.RegionImageUrl
            //    });
            //}
            var regiosnDto = _mapper.Map<List<RegionDTO>>(regionsdomain);
            //return to client
            return Ok(regiosnDto);
        }

        //Get region by Id
        // Get : https://localhost:portnumber/api/Region/{Id}
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid Id)
        {
            //var region = _dbContext.Regions.Find(Id);
            //Get Region domain from databse
            var regionDomains = await _regionRepository.GetByIdAsync(Id);

            if (regionDomains == null)
            {
                return NotFound();
            }
            //Map/Convert region domain to region dtos
            //var regiondto = new RegionDTO
            //{
            //    Id = regionDomains.Id,
            //    Name = regionDomains.Name,
            //    Code = regionDomains.Code,
            //    RegionImageUrl = regionDomains.RegionImageUrl
            //};
            return Ok(_mapper.Map<RegionDTO>(regionDomains));
        }
        //Post to create region
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequest addRegion)
        {
            //Map or Convert Dto to Domain Model
            var regionDomainModel = _mapper.Map<Region>(addRegion);
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);


            //Map Domain model back to DTO
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);
            return CreatedAtAction(nameof(GetRegionById), new { Id = regionDto.Id }, regionDto);
        }
        //PUT to update the region
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid Id, [FromBody] UpdateRegionDto updateRegion)
        {
            //map dto to domain
            var regionDomainModel = _mapper.Map<Region>(updateRegion);
            regionDomainModel = await _regionRepository.UpdateAsync(Id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain model to dto
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);
        }
        //Delete the region
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteRegion(Guid Id)
        {
            var regionDomainModel = await _regionRepository.DeleteeAsync(Id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //return delete data bacnk
            //map to region dto
            return Ok(_mapper.Map<RegionDTO>(regionDomainModel));
        }
    }
}
