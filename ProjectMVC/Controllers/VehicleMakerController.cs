﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectMVC.Models;
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

        public async Task<IActionResult> Index(string condition, string sortOrder, int pageNumber)
        {
            List<VehicleMakeDTORead> entityList = new List<VehicleMakeDTORead>();

            if(pageNumber < 0)
            {
                pageNumber = 1;
            }

            int pageSize = 5;

            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl);

            if(response.IsSuccessStatusCode)
            {
                string data =  await response.Content.ReadAsStringAsync();
                entityList = JsonConvert.DeserializeObject<List<VehicleMakeDTORead>>(data);

                if (entityList is null)
                {
                    return NotFound("No data in entity list!!!");
                }

                if(condition is not null && condition.Length > 0)
                {
                  entityList = await Filter(condition, entityList);
                  
                }

               entityList = await SortByAbrv(sortOrder, entityList);
               
            }

            return View(await PaginatedListViewModel<VehicleMakeDTORead>.Paginate(entityList, pageNumber, pageSize));
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
                return View();
                
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

        [HttpGet]

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
        private async Task<List<VehicleMakeDTORead>> Filter(string condition, List<VehicleMakeDTORead> list)
        {
            condition = condition.ToLower();

            return list = list.Where(e => e.Abrv.ToLower().Contains(condition)
            || e.Name.ToLower().Contains(condition))
                .OrderBy(e => e.Abrv)
                .ToList();
        }

        private async Task<List<VehicleMakeDTORead>> SortByAbrv(string sortOrder, List<VehicleMakeDTORead> entityList)
        {
            ViewData["AbrvSortParam"] = string.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";

            switch (sortOrder)
            {
                case "abrv_desc":
                    entityList = entityList.OrderByDescending(e => e.Abrv).ToList();
                    break;

                default:
                    entityList = entityList.OrderBy(e => e.Abrv).ToList();
                    break;
            }

            return entityList;

        }

    }
}






