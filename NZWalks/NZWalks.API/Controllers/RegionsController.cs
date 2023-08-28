using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll Regions Action Method Was Invoked");

            List<Region> regionsModel = await regionRepository.GetAllAsync();

            logger.LogInformation($"Finished GetAll Regions Request with Data: {JsonSerializer.Serialize(regionsModel)}");

            return Ok(mapper.Map<List<RegionDto>>(regionsModel));
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Region regionModel = await regionRepository.GetByIdAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionModel));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionDto request)
        {
            Region regionModel = await regionRepository.CreateAsync(mapper.Map<Region>(request));

            RegionDto regionDto = mapper.Map<RegionDto>(regionModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
        {
            Region regionModel = mapper.Map<Region>(request);

            regionModel = await regionRepository.UpdateAsync(id, regionModel);

            if (regionModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionModel));
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region region = await regionRepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
