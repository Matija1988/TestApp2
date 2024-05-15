using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectService.Model;
using System.Text;

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
        public IActionResult Create()
        {
            return View();
        }


        //[HttpGet]

        //public IActionResult Edit()
        //{
        //    return View();
        //}

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
            try { 
            var entity = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(entity, Encoding.UTF8, "application/json");
          
            var response =  _httpClient.PostAsync(baseUrl, content).Result;
            
                return RedirectToAction("Index");               
            
            }
            catch (Exception ex)
            {
                return View();
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {

            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(baseUrl + "/FindMakerByID/" + id).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                var data = response.Content.ReadAsStringAsync().Result;
                var entityFromDB = JsonConvert.DeserializeObject<VehicleMakeDTOReadWithoutID>(data);
                return View(entityFromDB);
            }
            catch (Exception)
            {
                throw;
            }
          
        }

        [HttpPost]

        public IActionResult Edit(VehicleMakeDTOInsert dto, int id)
        {
            string data = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json"); 

            HttpResponseMessage response = _httpClient.PutAsync(baseUrl, content).Result; 
            
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            return View();
        }


    }
}






