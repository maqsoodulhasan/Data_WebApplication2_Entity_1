using Data_WebApplication2.Models;
using Data_WebApplication2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Data_WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private static List<Person> People = new List<Person>()
            {
                new Person {Id = 1, Name = "Elin", PhoneNumber = "12345678", City = "Gothenburg" },
                new Person {Id = 2, Name = "Poulin", PhoneNumber = "87654321", City = "Stockholm" },
                new Person { Id = 3, Name = "john", PhoneNumber = "12348765", City = "Malmo" },
                new Person { Id = 4, Name = "sandra", PhoneNumber = "87345621", City = "jönkoping" },
                new Person { Id = 5, Name = "olof", PhoneNumber = "654321765", City = "Karlstad" }

            }; 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
          
            _logger = logger;
        }

        public IActionResult Index()
        {
            PersonDetailViewModel viewModels   = new PersonDetailViewModel();

           
            foreach (var item in People)
            {
                viewModels.PeopleViewModel.Add(new PeopleViewModel
                {
                    Id = item.Id,   
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    City = item.City,   
                    });
            }
         
            return View(viewModels);
        }
        [HttpPost]
        public IActionResult Index(string query)
        {
            PersonDetailViewModel viewModels = new PersonDetailViewModel();


            var filteredData = People.Where(x => x.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || x.City.Contains(query, StringComparison.OrdinalIgnoreCase) || x.PhoneNumber.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var item in filteredData)
            {
                viewModels.PeopleViewModel.Add(new PeopleViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    City = item.City,
                });
            }
            
            return View(viewModels);
        }

        [HttpPost]
        public IActionResult Sort()  //sorting with name alphabetically
        { 
            People = People.OrderBy(x => x.Name).ToList(); 
            return RedirectToAction("Index");
        }
           

        [HttpPost]
        public IActionResult AddPerson(PersonDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                People.Add(new Person
                {
                    Id = People.Max(x => x.Id) + 1,
                    Name = model.CreatePersonViewModel.Name,
                    PhoneNumber = model.CreatePersonViewModel.PhoneNumber,
                    City = model.CreatePersonViewModel.City
                });

            }
            return RedirectToAction("Index");
        }
    
        public IActionResult Delete(int id)
        {
            People.Remove(People.Where(x=>x.Id==id).First());
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Searchbox()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
