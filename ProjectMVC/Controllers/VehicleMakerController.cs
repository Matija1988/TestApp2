using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using ProjectMVC.Models;
using ProjectService.Model;
using System.Drawing.Printing;
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

        public async Task<IActionResult> Index()
        {

            return View();

        }

        [HttpPost]

        public async Task<IActionResult> Create(VehicleMakeDTOInsert dto) 
        {
            try { 
            var entity = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(entity, Encoding.UTF8, "application/json");
          
            var response = await  _httpClient.PostAsync(baseUrl, content);
            
                return RedirectToAction("Index");               
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "/FindMakerByID/" + id);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                var data = await response.Content.ReadAsStringAsync();
                var entityFromDB = JsonConvert.DeserializeObject<VehicleMakeDTOReadWithoutID>(data);
                return View(entityFromDB);
            }
            catch (Exception)
            {
                throw;
            }
          
        }

        [HttpPost]

        public async Task<IActionResult> Edit(VehicleMakeDTOInsert dto, int id)
        {
            string data = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json"); 

            HttpResponseMessage response = await _httpClient.PutAsync(baseUrl + "/" + id, content); 
            
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            
            HttpResponseMessage response = await _httpClient.DeleteAsync(baseUrl + "/DeleteVehicleMake/" + id);

            var data = await response.Content.ReadAsStringAsync();
            var entityFromDB = JsonConvert.DeserializeObject(data);

            if (!response.IsSuccessStatusCode)
            {
                
                return View();
            }

            return RedirectToAction("Index");

        }

        #region API Call

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<VehicleMakeDTORead> entityList = new List<VehicleMakeDTORead>();

            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl);

            if (entityList is null) 
            {
                return NotFound("No data in entity list!!!");
            }

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                entityList = JsonConvert.DeserializeObject<List<VehicleMakeDTORead>>(data);

            }
            return Json(new {data=entityList });
        }

        #endregion

    }
}






