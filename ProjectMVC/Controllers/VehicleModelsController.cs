using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Versioning;
using ProjectMVC.Models;
using ProjectService.Model;
using System.Drawing.Printing;
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

        public async Task<IActionResult> Index(string condition, string sortOrder, int pageNumber)
        {
            List<VehicleModelDTORead> entityList = new List<VehicleModelDTORead>();

            int pageSize = 5;

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

                if (condition is not null && condition.Length > 0)
                {
                    entityList = await Filter(condition, entityList);
                }

                entityList = await SortByAbrv(sortOrder, entityList);

            }

            return View(await PaginatedListViewModel<VehicleModelDTORead>.Paginate(entityList, pageNumber, pageSize));
        }

//        [HttpPost]

        //public async Task<IActionResult> PaginateRequest(int pageNumber, int pageSize)
        //{
        //    if (pageNumber < 1)
        //    {
        //        pageNumber = 1;
        //    }

        //    if(pageSize < 1)
        //    {
        //        pageSize = 1; 
        //    }
        //    List<VehicleModelDTORead> entityList = new List<VehicleModelDTORead>();

        //    HttpResponseMessage sendPageNumber 
        //        = await _httpClient.GetAsync(baseUrl + "/Paginate/" + pageNumber + "/" + pageSize);

        //    if (sendPageNumber.IsSuccessStatusCode)
        //    {
        //        string data = await sendPageNumber.Content.ReadAsStringAsync();
        //        entityList = JsonConvert.DeserializeObject<List<VehicleModelDTORead>>(data) ??
        //            throw new Exception("No data found in database!!!");
        //    }



        //    return null;
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdown();
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create(VehicleModelDTOInsert dto)
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

        public async Task<IActionResult> Edit(int id)
        {
           // id = 14;

            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "/FindModel/" + id);

            if (!response.IsSuccessStatusCode)
            {
               return RedirectToAction("Index");
            }

            var data = await response.Content.ReadAsStringAsync();
            var entityFromDB = JsonConvert.DeserializeObject<VehicleModelDTOInsert>(data);

            int makeId = entityFromDB.MakeId;

            await PopulateDropdown(makeId);
            return View(entityFromDB);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehicleModelDTOInsert dto, int id)
        {
            string data = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(baseUrl + "/" + id, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(baseUrl + "/DeleteVehicleModel/" + id);

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }
            return RedirectToAction("Index");

        }
        public async Task PopulateDropdown()
        {
            HttpResponseMessage getVehicleMake = await _httpClient.GetAsync(baseUrlForMaker);

            string VehicleMakeData = await getVehicleMake.Content.ReadAsStringAsync();

            var VehicleMakeList = JsonConvert.DeserializeObject<List<VehicleMakeDTORead>>(VehicleMakeData);

            ViewBag.VehicleMakerList = new SelectList(VehicleMakeList, "Id", "Abrv");

        }

        public async Task PopulateDropdown(int makeId)
        {
            HttpResponseMessage getVehicleMake = await _httpClient.GetAsync(baseUrlForMaker);

            string VehicleMakeData = await getVehicleMake.Content.ReadAsStringAsync();

            var VehicleMakeList = JsonConvert.DeserializeObject<List<VehicleMakeDTORead>>(VehicleMakeData);

            ViewBag.VehicleMakerList = new SelectList(VehicleMakeList, "Id", "Abrv");


        }

        private async Task<List<VehicleModelDTORead>> Filter(string condition, List<VehicleModelDTORead> list)
        {
            condition = condition.ToLower();

            return list = list.Where(n => n.Abrv.ToLower().Contains(condition)
            || n.Name.ToLower().Contains(condition)
            || n.Maker.ToLower().Contains(condition))
                .OrderBy(n => n.Maker)
                .ToList();
        }

        private async Task<List<VehicleModelDTORead>> SortByAbrv(string sortOrder, List<VehicleModelDTORead> entityList)
        {
            ViewData["MakerSortParam"] = string.IsNullOrEmpty(sortOrder) ? "maker_desc" : "";

            switch (sortOrder)
            {
                case "maker_desc":
                    entityList = entityList.OrderByDescending(e => e.Maker).ToList();
                    break;

                default:
                    entityList = entityList.OrderBy(e => e.Maker).ToList();
                    break;
            }

            return entityList;

        }

    }
}






