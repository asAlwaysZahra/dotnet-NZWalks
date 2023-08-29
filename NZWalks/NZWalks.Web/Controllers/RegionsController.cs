using Microsoft.AspNetCore.Mvc;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.Web.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        public RegionsController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpGet]
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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            try
            {
                var client = clientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7080/Regions"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };

                var httpResponseMessage = await client.SendAsync(httpRequestMessage);

                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response != null)
                {
                    return RedirectToAction("Index", "Regions");
                }
            }
            catch (Exception ex)
            {
                // log error ...
            }

            return View();
        }
    }
}
