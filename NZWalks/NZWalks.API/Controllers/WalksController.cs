using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Craete([FromBody] AddWalkDto request)
        {
            Walk walkModel = mapper.Map<Walk>(request);

            await walkRepository.CreateAsync(walkModel);

            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Walk> walksModel = await walkRepository.GetAllAsync();
            return Ok(mapper.Map<List<WalkDto>>(walksModel));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Walk walkModel = await walkRepository.GetByIdAsync(id);

            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto request)
        {
            Walk walkModel = mapper.Map<Walk>(request);

            walkModel = await walkRepository.UpdateAsync(id, walkModel);

            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Walk walkModel = await walkRepository.DeleteAsync(id);

            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
