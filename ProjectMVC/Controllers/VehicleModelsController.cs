﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProjectMVC.Models;
using ProjectService.Model;
using System.Linq.Expressions;
using System.Text;

namespace ProjectMVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7186/VehicleModel");

        Uri baseUrlForMaker = new Uri("https://localhost:7186/VehicleMake");

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
            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
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
        public async Task<IActionResult> Create()
        {
             PopulateDropdown();
            return View();
        }

        public void PopulateDropdown()
        {
            HttpResponseMessage getVehicleMake = _httpClient.GetAsync(baseUrlForMaker).Result;

            string VehicleMakeData = getVehicleMake.Content.ReadAsStringAsync().Result;

            var VehicleMakeList = JsonConvert.DeserializeObject<List<VehicleMakeDTORead>>(VehicleMakeData);

            //IEnumerable <SelectMakerView> enumerateVehicleMake = VehicleMakeList.Select(i => 
            //new SelectMakerView
            //{
            //    Id = i.Id,
            //    Abrv = i.Abrv
            //});

            ViewBag.VehicleMakerList = new SelectList(VehicleMakeList, "Id", "Abrv");

        }

        [HttpPost]
        public async Task <IActionResult> Create(VehicleModelDTOInsert dto)
        {
            try
            {
                HttpResponseMessage getVehicleMake = await _httpClient.GetAsync(baseUrl);

                string VehicleMakeData = await getVehicleMake.Content.ReadAsStringAsync();

                var VehicleMakeList = JsonConvert.DeserializeObject<List<VehicleMakeDTORead>>(VehicleMakeData);
                

                var entity = JsonConvert.SerializeObject(dto);
                StringContent content = new StringContent(entity, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(baseUrl, content);

                return RedirectToAction("Index");   
                    
            } 
            catch (Exception ex)
            {
                return View();
                throw new Exception(ex.Message);
               
            }
        }

        [HttpGet] 

        public async Task <IActionResult> Edit(int id) 
        { 
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "/FindModel/" + id);

                if(!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var data =  await response.Content.ReadAsStringAsync();
                var entityFromDB = JsonConvert.DeserializeObject<VehicleModelDTOInsert>(data);

                return View(entityFromDB);
            } 
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpPost]
        public async Task <IActionResult> Edit(VehicleModelDTOInsert dto, int id)
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
            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "/FindModel/" + id);

            if(!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var data = await response.Content.ReadAsStringAsync();
            var entityFromDb = JsonConvert.DeserializeObject<VehicleModelDTOReadWithoutID>(data);

            return View(entityFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public async Task <IActionResult> DeleteConfirm(int id)
        {
            HttpResponseMessage response =  await _httpClient.DeleteAsync(baseUrl + "/DeleteVehicleModel/" + id);

            if(!response.IsSuccessStatusCode) 
            { 
            return View();
            }
            return RedirectToAction("Index");

        }

    }
}






