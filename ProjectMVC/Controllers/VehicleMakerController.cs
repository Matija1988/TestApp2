using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectService.Model;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VehicleMakeDTORead> entityList = new List<VehicleMakeDTORead>();
            HttpResponseMessage response = _httpClient.GetAsync(baseUrl).Result;

            if(response.IsSuccessStatusCode)
            {
                string data =  response.Content.ReadAsStringAsync().Result;
                entityList = JsonConvert.DeserializeObject<List<VehicleMakeDTORead>>(data);

                if(entityList is null)
                {
                    return NotFound("No data in entity list!!!");
                }
            }

            return View(entityList);
        }

        [HttpPost]

        public async Task<IActionResult> Create(VehicleMakeDTOInsert dto) 
        {
            var entity = JsonConvert.SerializeObject(dto);
            HttpContent data = new StringContent(entity);

            var response =  _httpClient.PostAsync(baseUrl, data).Result;
            
            if(!response.IsSuccessStatusCode) 
            {
                return View();
            }



            return RedirectToAction("Index");
        }

    }
}
