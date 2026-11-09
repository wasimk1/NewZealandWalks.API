using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTOs;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace NZWalks.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allregions = new List<RegionDto>();
            try
            {
                //Get all Region from Web API
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7061/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();

                var strResponseBody = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();
                allregions.AddRange(strResponseBody);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return View(allregions);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7061/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response is not null)
                {
                    return RedirectToAction("Index", "Region");
                }
                return View();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var responsebyid = await client.GetAsync($"https://localhost:7061/api/regions/{id}");
            responsebyid.EnsureSuccessStatusCode();

            var response = await responsebyid.Content.ReadFromJsonAsync<RegionDto>();
            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto regionDto)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7061/api/regions/{regionDto.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(regionDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        //[HttpGet] //this will directly delete the region without taking the permission like on button click the region get delete
        //public async Task<IActionResult> Delete(RegionDto regionDto)
        //{
        //    try
        //    {
        //        var client = httpClientFactory.CreateClient();
        //        var responsebyid = await client.DeleteAsync($"https://localhost:7061/api/regions/{regionDto.Id}");
        //        responsebyid.EnsureSuccessStatusCode();
        //        var response = await responsebyid.Content.ReadFromJsonAsync<RegionDto>();
        //        if (response is not null)
        //        {
        //            //return View(response);
        //            return RedirectToAction("Index");
        //        }
        //        return View(null);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }

        //}
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var response = await client.DeleteAsync($"https://localhost:7061/api/regions/{id}");
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
