using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Linq.Expressions;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext,
            IRegionRepository regionRepository, 
            IMapper mapper, 
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger.LogInformation("getall action methods was invoked");
                var allregions = await regionRepository.GetAllAsync(); 
                //getting the data from Repository 
                //var allregions = await dbContext.Regions.ToListAsync(); //get data from database- domain model entity
                //var regionDtos = new List<RegionDto>(); //map domains model into DTOs
                //foreach (var item in allregions)
                //{
                //    regionDtos.Add(new RegionDto()
                //    {
                //        Id = item.Id,
                //        Code = item.Code,
                //        Name = item.Name,
                //        RegionImageUrl = item.RegionImageUrl
                //    });
                //}
                logger.LogInformation($"Finished getallregions request with data : {JsonSerializer.Serialize(allregions)} ");
                var regionDtos = mapper.Map<List<RegionDto>>(allregions); //auto mapping concept 
                return Ok(regionDtos); //Return dtos instead of Domain model direct (entity)
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet]
        //[Route("{id}")]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        //[HttpGet("id")] //normal and common attribute
        //[HttpGet("{id:Guid}")] //pro level
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            try
            {
                var getregionid = await regionRepository.GetRegionByIdAsync(id); // from repository
                //var getregionid = await dbContext.Regions.FindAsync(id); //first method
                //var getregionid = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);//second method
                if (getregionid == null)
                {
                    return StatusCode(404, "Not Found");
                }
                //var regiondto = new RegionDto() //this object create a dto from entity model
                //{
                //    Id = getregionid.Id,
                //    Code = getregionid.Code,
                //    Name = getregionid.Name,
                //    RegionImageUrl = getregionid.RegionImageUrl
                //};
                var regiondto = mapper.Map<RegionDto>(getregionid); //auto mapping concept
                return Ok(regiondto); //returing the dto data to client
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateRegion(AddRegionRequestDto addRegionRequestDto)
        {
            try
            {
                //map or convert DTO to domain model
                //var regiondomainmodel = new Region()
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
                //};
                var regiondomainmodel = mapper.Map<Region>(addRegionRequestDto);
                //use domain model to create region or add into database entity
                regiondomainmodel = await regionRepository.CreateRegionAsync(regiondomainmodel);

                //var regionDto = new RegionDto()
                //{
                //    Id = regiondomainmodel.Id,
                //    Code = regiondomainmodel.Code,
                //    Name = regiondomainmodel.Name,
                //    RegionImageUrl = regiondomainmodel.RegionImageUrl
                //};
                var regionDto = mapper.Map<RegionDto>(regiondomainmodel);
                //return Ok(regiondomainmodel); //this also works but when we create using POST we need to send the 201 status
                return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);

            }
            catch (Exception ex)
            {
                return StatusCode(505, ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegionById(Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {

            //var getregionbyid = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //Map DTO to Domain model
            //var regiondomainmodel = new Region()
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};
            var regiondomainmodel = mapper.Map<Region>(updateRegionRequestDto);
            regiondomainmodel = await regionRepository.UpdateRegionByIdAsync(id, regiondomainmodel);
            if (regiondomainmodel is null)
            {
                return NotFound();
            }
            ////map dto to domain model entity

            //regiondomainmodel.Code = updateRegionRequestDto.Code;
            //regiondomainmodel.Name = updateRegionRequestDto.Name;
            //regiondomainmodel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //await dbContext.SaveChangesAsync();

            //convert region model into DTO
            //var regionDto = new RegionDto()
            //{
            //    Id = regiondomainmodel.Id,
            //    Code = regiondomainmodel.Code,
            //    Name = regiondomainmodel.Name,
            //    RegionImageUrl = regiondomainmodel.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regiondomainmodel);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteRegionById([FromRoute] Guid id)
        {
            //var regionById = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionById = await regionRepository.DeleteRegionByIdSync(id);
            if (regionById is null)
            {
                return NotFound();
            }
            //dbContext.Regions.Remove(regionById);
            //await dbContext.SaveChangesAsync();
            //var regionDTO = new RegionDto
            //{
            //    Id = regionById.Id,
            //    Code = regionById.Code,
            //    Name = regionById.Name,
            //    RegionImageUrl = regionById.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDto>(regionById);
            return Ok(regionDTO);
        }


    }
}
