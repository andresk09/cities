using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpLab.API.Application.Dtos;
using OpLab.Web.Helpers;
using System.Net.Http;

namespace OpLab.Web.Controllers
{
    public class PointsOfInterestController : Controller
    {
        public IActionResult Details(int cityId, int id)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response = client.GetAsync("/api/cities/" + cityId + "/pointsofinterest/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                PointOfInterestDto data = JsonConvert.DeserializeObject<PointOfInterestDto>(stringData);

                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
        }

        public IActionResult Create(int cityId)
        {
            ViewBag.CityId = cityId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int cityId, PointOfInterestDto pointOfInterest)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                string stringData = JsonConvert.SerializeObject(pointOfInterest);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/api/cities/" + cityId + "/pointsofinterest", contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Details", "Cities", new { Id = cityId });
            };
        }

        public ActionResult Edit(int cityId, int id)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response = client.GetAsync("/api/cities/" + cityId + "/pointsofinterest/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                PointOfInterestDto data = JsonConvert.DeserializeObject<PointOfInterestDto>(stringData);
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int cityId, PointOfInterestDto pointOfInterest)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = ApiClient.GetClient())
                {
                    string stringData = JsonConvert.SerializeObject(pointOfInterest);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync("/api/cities/" + cityId + "/pointsofinterest/" + pointOfInterest.Id, contentData).Result;
                    ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("Details", "Cities", new { Id = cityId });
                }
            }

            return View(pointOfInterest);
        }

        public IActionResult Delete(int cityId, int id)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response = client.GetAsync("/api/cities/" + cityId + "/pointsofinterest/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                PointOfInterestDto data = JsonConvert.DeserializeObject<PointOfInterestDto>(stringData);
                return View(data);
            };
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int cityId, int id)
        {
            using (HttpClient client = ApiClient.GetClient())
            {
                HttpResponseMessage response = client.DeleteAsync("/api/cities/" + cityId + "/pointsofinterest/" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Details", "Cities", new { Id = cityId });
            };
        }
    }
}
