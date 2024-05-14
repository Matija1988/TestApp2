using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectService.Model;

namespace ProjectMVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7186/VehicleModel");

        private readonly HttpClient _httpClient;

        public VehicleModelsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VehicleModelDTORead> entityList = new List<VehicleModelDTORead>();
            HttpResponseMessage response = _httpClient.GetAsync(baseUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                entityList = JsonConvert.DeserializeObject<List<VehicleModelDTORead>>(data) ??
                    throw new Exception("No data found in database!!!");

                if (entityList is null)
                {
                    return NotFound("No data found in database");
                }
            }
            return View(entityList);
        }
    }
}
