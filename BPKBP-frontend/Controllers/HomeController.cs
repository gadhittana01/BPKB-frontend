using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BPKBP_frontend.Models;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;
using System.Runtime.InteropServices.ComTypes;
using BPKBP_frontend.ViewModels;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace BPKBP_frontend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private const string URL = "https://localhost:7244/api/bpkb/";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<BPKB> dataObjects;
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(URL);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        // List data response.
        HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        if (response.IsSuccessStatusCode)
        {
            // Parse the response body.
            dataObjects = JsonConvert.DeserializeObject<IEnumerable<BPKB>>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            throw new Exception("Something went wrong...");
        }
        client.Dispose();

        return View(dataObjects);
    }

    public async Task<IEnumerable<Location>> GetAllLocation()
    {
        IEnumerable<Location> dataObjects;
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(URL);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        // List data response.
        HttpResponseMessage response = client.GetAsync("location").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        if (response.IsSuccessStatusCode)
        {
            // Parse the response body.
            dataObjects = JsonConvert.DeserializeObject<IEnumerable<Location>>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            throw new Exception("Something went wrong...");
        }
        client.Dispose();
        return dataObjects;
    }

    public async Task<IActionResult> BPKBIndex()
    {
        

        CreateViewModel createViewModel = new CreateViewModel()
        {
            locations = await GetAllLocation(),
        };

        return View("Create", createViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateViewModel request)
    {
        BPKB reqBody = request.bpkb;
        BPKB dataObject;
        HttpClient client = new HttpClient();

        if (ModelState.IsValid)
        {
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("POST"), "");

            var BPKBDate = DateTime.ParseExact(reqBody.BPKBDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var BPKBDateIn = DateTime.ParseExact(reqBody.BPKBDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var FakturDate = DateTime.ParseExact(reqBody.BPKBDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            BPKBInsertDto bPKBInsertDto = new BPKBInsertDto()
            {
                AgreementNumber = reqBody.AgreementNumber,
                LocationFK = reqBody.LocationFK,
                BPKBDate = BPKBDate,
                BPKBDateIn = BPKBDateIn,
                BPKBNo = reqBody.BPKBNo,
                FakturDate = FakturDate,
                FakturNo = reqBody.FakturNo,
                BranchID = reqBody.BranchID,
                PoliceNo = reqBody.PoliceNo
            };

            requestMessage.Content = JsonContent.Create(bPKBInsertDto);

            // List data response.
            HttpResponseMessage response = client.SendAsync(requestMessage).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                dataObject = JsonConvert.DeserializeObject<BPKB>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.Content.ToString());
            }
            client.Dispose();

            return RedirectToAction("Index");
        }

        request.locations = await GetAllLocation();

        return View(request);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

