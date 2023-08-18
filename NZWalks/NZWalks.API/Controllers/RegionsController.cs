using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly DataContext context;

        public RegionsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Region> regionsDomain = await context.Regions.ToListAsync();
            List<RegionDto> regionsDto = new List<RegionDto>();
            foreach (Region region in regionsDomain)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    ImageUrl = region.ImageUrl
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Region regionDomain = await context.Regions.FindAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            RegionDto regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                ImageUrl = regionDomain.ImageUrl
            };

            return Ok(regionDto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDto request)
        {
            Region region = new Region
            {
                Code = request.Code,
                Name = request.Name,
                ImageUrl = request.ImageUrl
            };

            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();

            RegionDto regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                ImageUrl = region.ImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
        {
            Region region = await context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            region.Code = request.Code;
            region.Name = request.Name;
            region.ImageUrl = request.ImageUrl;

            context.Regions.Update(region); // needed?
            await context.SaveChangesAsync();

            RegionDto regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                ImageUrl = region.ImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region region = await context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            context.Regions.Remove(region);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
