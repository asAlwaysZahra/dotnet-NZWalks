using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Data;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly ILogger<WalksController> logger;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, ILogger<WalksController> logger)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Craete([FromBody] AddWalkDto request)
        {
            try
            {
                Walk walkModel = mapper.Map<Walk>(request);

                await walkRepository.CreateAsync(walkModel);

                return Ok(mapper.Map<WalkDto>(walkModel));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            try
            {
                List<Walk> walksModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                    pageNumber, pageSize);

                return Ok(mapper.Map<List<WalkDto>>(walksModel));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                Walk walkModel = await walkRepository.GetByIdAsync(id);

                if (walkModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(walkModel));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto request)
        {
            try
            {
                Walk walkModel = mapper.Map<Walk>(request);

                walkModel = await walkRepository.UpdateAsync(id, walkModel);

                if (walkModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(walkModel));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                Walk walkModel = await walkRepository.DeleteAsync(id);

                if (walkModel == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
