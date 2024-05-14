using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ninject;
using ProjectService.Controllers;
using ProjectService.Model;
using ProjectService.Service;

namespace ProjectMVC.Controllers
{
   
    public class VehicleMakerController : Controller
    {


        Uri baseUrl = new Uri("https://localhost:7186/VehicleMake");
        
        private readonly HttpClient _httpClient;

        public VehicleMakerController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<VehicleMakeDTORead> entityList = new List<VehicleMakeDTORead>();
            HttpResponseMessage response = _httpClient.GetAsync(baseUrl).Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                entityList = JsonConvert.DeserializeObject<List<VehicleMakeDTORead>>(data);

                if(entityList is null)
                {
                    return NotFound("No data in entity list!!!");
                }
            }


            return View(entityList);
        }
    }
}
