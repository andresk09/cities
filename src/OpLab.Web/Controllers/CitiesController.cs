using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using OpLab.Web.Helpers;
using OpLab.API.Application.Dtos;
using Newtonsoft.Json;

namespace OpLab.Web.Controllers
{
    public class CitiesController : Controller
    {
        public IActionResult Index()
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response = client.GetAsync("/api/cities").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<CityDto> data = JsonConvert.DeserializeObject<List<CityDto>>(stringData);
                return View(data);
            }
        }

        public IActionResult Details(int id)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response = client.GetAsync("/api/cities/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                CityDto data = JsonConvert.DeserializeObject<CityDto>(stringData);

                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CityDto city)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                string stringData = JsonConvert.SerializeObject(city);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/api/cities", contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Index");
            };
        }

        public ActionResult Edit(int id)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response = client.GetAsync("/api/cities/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                CityDto data = JsonConvert.DeserializeObject<CityDto>(stringData);
                return View(data);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CityDto city)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = ApiClient.GetClient())
                {
                    string stringData = JsonConvert.SerializeObject(city);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync("/api/cities/" + city.Id, contentData).Result;
                    ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("Details", new { id = city.Id });
                }
            }

            return View(city);
        }

        public IActionResult Delete(int id)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response = client.GetAsync("/api/cities/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                CityDto data = JsonConvert.DeserializeObject<CityDto>(stringData);
                return View(data);
            };
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response =client.DeleteAsync("/api/cities/"+ id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Index");
            };
        }

    }
}
