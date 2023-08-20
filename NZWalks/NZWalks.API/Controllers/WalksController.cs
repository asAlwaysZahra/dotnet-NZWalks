using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}
