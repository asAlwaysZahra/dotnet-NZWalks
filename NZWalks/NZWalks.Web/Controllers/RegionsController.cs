using Microsoft.AspNetCore.Mvc;
using NZWalks.Web.Models.DTO;

namespace NZWalks.Web.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        public RegionsController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();

            try
            {
                // get all regions from web api
                var client = clientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7080/Regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {
                // log ..
            }

            return View(response);
        }
    }
}
