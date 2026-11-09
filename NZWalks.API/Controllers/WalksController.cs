using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Net;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        // /api/Walks?filterOn=Name&FilterQuery=Track&sortBy=Name&isAscending=true
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? FilterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {

            var getallwalksdomain = await walkRepository.GetAllWalksAsync(filterOn, FilterQuery, sortBy, isAscending ?? true,
                    pageNumber, pageSize);
            //throw new Exception("This is a test walk exception");

            var getwalksdto = mapper.Map<List<WalkDto>>(getallwalksdomain);
            return Ok(getwalksdto);

        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var getwalkdominbyId = await walkRepository.GetWalkByIdAsync(id);
            if (getwalkdominbyId == null)
            {
                return NotFound();
            }
            var getWalkDto = mapper.Map<WalkDto>(getwalkdominbyId);
            return Ok(getWalkDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalks([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            ///Map DTO to Domain model
            var WalkDomainmodel = mapper.Map<Walk>(addWalkRequestDto);
            WalkDomainmodel = await walkRepository.CreateWalkSync(WalkDomainmodel);
            //Map Domain model to DTO
            var WalkDto = mapper.Map<WalkDto>(WalkDomainmodel);
            //return Ok(WalkDto);
            return CreatedAtAction(nameof(GetWalkById), new { id = WalkDto.Id }, WalkDto); //201 for POST action verb
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalkById([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

            var walkregionbyId = mapper.Map<Walk>(updateWalkRequestDto);
            walkregionbyId = await walkRepository.UpdateWalkByIdAsync(id, walkregionbyId);
            if (walkregionbyId == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<WalkDto>(walkregionbyId);
            return Ok(walkDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkById(Guid id)
        {
            var getWalkbyId = await walkRepository.DeleteWalkByIdAsync(id);
            if (getWalkbyId == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(getWalkbyId);
            return Ok(walkDto);
        }
    }
}
