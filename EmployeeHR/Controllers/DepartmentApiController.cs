﻿using EmployeeHR.Models;
using EmployeeHR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace EmployeeHR.Controllers
{
    public class DepartmentApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public DepartmentApiController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7191/api");

        }
        public IActionResult Index()
        {

            HttpResponseMessage responseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + "/Department/GetAll").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                var departments = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(content);
                return View(departments);
            }
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DepartmentViewModel department)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_httpClient.BaseAddress + "/Department");

                var responseMessage = httpClient.PostAsJsonAsync<DepartmentViewModel>("department", department);
                responseMessage.Wait();
                var response = responseMessage.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            HttpResponseMessage responseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + $"/Department/GetById/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                var department = JsonConvert.DeserializeObject<DepartmentViewModel>(content);
                return View(department);
            }

            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id, DepartmentViewModel department)
        {
            var responseMessage = _httpClient.PutAsJsonAsync<DepartmentViewModel>(_httpClient.BaseAddress + "/Department", department);
            //var responseMessage = _httpClient.PatchAsJsonAsync<DepartmentViewModel>(_httpClient.BaseAddress + "/Department", department);
            responseMessage.Wait();
            var resp = responseMessage.Result;
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage responseMessage = _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/Department?Id={id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }



    }
}
