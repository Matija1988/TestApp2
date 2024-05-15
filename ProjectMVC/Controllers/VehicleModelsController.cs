using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectService.Model;
using System.Text;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VehicleModelDTOInsert dto)
        {
            try
            {
                var entity = JsonConvert.SerializeObject(dto);
                StringContent content = new StringContent(entity, Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync(baseUrl, content).Result;

                return RedirectToAction("Index");   
                    
            } 
            catch (Exception ex)
            {
                return View();
                throw new Exception(ex.Message);
               
            }
        }

        [HttpGet] 

        public IActionResult Edit(int id) 
        { 
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(baseUrl + "/FindModel/" + id).Result;

                if(!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var data = response.Content.ReadAsStringAsync().Result;
                var entityFromDB = JsonConvert.DeserializeObject<VehicleModelDTOInsert>(data);

                return View(entityFromDB);
            } 
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpPost]
        public IActionResult Edit(VehicleModelDTOInsert dto, int id)
        {
            string data = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(baseUrl + "/" + id, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = _httpClient.GetAsync(baseUrl + "/FindModel/" + id).Result;

            if(!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var data = response.Content.ReadAsStringAsync().Result;
            var entityFromDb = JsonConvert.DeserializeObject<VehicleModelDTOReadWithoutID>(data);

            return View(entityFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(baseUrl + "/DeleteVehicleModel/" + id).Result;

            if(!response.IsSuccessStatusCode) 
            { 
            return View();
            }
            return RedirectToAction("Index");

        }

    }
}






