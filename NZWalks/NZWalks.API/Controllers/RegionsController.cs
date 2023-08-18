using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            List<Region> regionsDomain = context.Regions.ToList();
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
        public IActionResult GetById([FromRoute] Guid id)
        {
            Region regionDomain = context.Regions.Find(id);

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
        public IActionResult Create([FromBody] AddRegionDto request)
        {
            Region region = new Region
            {
                Code = request.Code,
                Name = request.Name,
                ImageUrl = request.ImageUrl
            };

            context.Regions.Add(region);
            context.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
        {
            Region region = context.Regions.FirstOrDefault(r => r.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            region.Code = request.Code;
            region.Name = request.Name;
            region.ImageUrl = request.ImageUrl;

            context.Regions.Update(region); // needed?
            context.SaveChanges();

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            Region region = context.Regions.FirstOrDefault(r => r.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }
    }
}
