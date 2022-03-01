using Data_WebApplication2.Models;
using Data_WebApplication2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_WebApplication2.Controllers
{
    public class PersonController : Controller
    {
        private readonly IMyDbRepository _dbRepository;

        public PersonController(IMyDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public IActionResult Index()
        {
            var persons = _dbRepository.GetPersons();

            return View(persons);
        }

        public IActionResult Create()
        {
            CreatePersonViewModel model = new CreatePersonViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreatePersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person
                {
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    City = model.City
                };

                _dbRepository.AddPerson(person);
                if (_dbRepository.Save())
                {
                    TempData["Message"] = "Person record has been saved";
                    return RedirectToAction("Index", "Person");
                }
                else
                {
                    ModelState.AddModelError("errror", "An error occurred while saving the data");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            CreatePersonViewModel model = new CreatePersonViewModel();

            var person = _dbRepository.GetPersonById(id);
            if (person != null)
            {
                model.Id = person.Id;
                model.Name = person.Name;
                model.PhoneNumber = person.PhoneNumber;
                model.City = person.City;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CreatePersonViewModel model)
        {
            Person person = null;

            if (ModelState.IsValid)
            {
                person = _dbRepository.GetPersonById(model.Id);
                person.Name = model.Name;
                person.PhoneNumber = model.PhoneNumber;
                person.City = model.City;

                _dbRepository.UpdatePerson(person);
                if (_dbRepository.Save())
                {
                    TempData["Message"] = "Person record has been saved";
                    return RedirectToAction("Index", "Person");
                }
                else
                {
                    ModelState.AddModelError("errror", "An error occurred while saving the data");
                    return View(model);
                }

            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            var person = _dbRepository.GetPersonById(id);
            if (person != null)
            {
                _dbRepository.DeletePerson(person);
                if (_dbRepository.Save())
                {
                    TempData["Message"] = "Person record has been deleted successfully";

                }
                else
                {
                    ModelState.AddModelError("errror", "An error occurred while deleting the data");
                }
            }

            return RedirectToAction("Index", "Person");
        }
    }
}

