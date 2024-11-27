using Animales.DAL;
using First_ASPNET_Core_MVC.Models.ViewModels;
using First_ASPNET_Core_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace First_ASPNET_Core_MVC.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet]
        public IActionResult Details(int id)
        {
            AnimalDAL dal = new AnimalDAL();

            DetailsAnimalViewModel viewModel = new DetailsAnimalViewModel
            {
                animal = dal.GetById(id)
            };

            // Devuelve la vista con el ViewModel que contiene los detalles del animal
            return View("Views/Animal/Details.cshtml", viewModel);
        }


        [HttpGet]
        public IActionResult Agregar()
        {
            // Crear instancia del DAL de TipoAnimal.
            TipoAnimalDAL tipoAnimalDal = new TipoAnimalDAL();

            // Obtener la lista de tipos de animales.
            List<TipoAnimal> tiposDeAnimales = tipoAnimalDal.GetAll();

            // Crear el ViewModel con la lista de tipos de animales.
            AgregarViewModel viewModel = new AgregarViewModel
            {
                TiposDeAnimales = tiposDeAnimales
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Confirmar(AgregarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, recarga la lista de tipos para mostrar la vista nuevamente.
                TipoAnimalDAL tipoAnimalDal = new TipoAnimalDAL();
                model.TiposDeAnimales = tipoAnimalDal.GetAll();

                return View("Agregar", model);
            }

            // Crear instancia del DAL y objeto Animal.
            AnimalDAL dal = new AnimalDAL();

            Animal animal = new Animal
            {
                NombreAnimal = model.NombreAnimal,
                FechaNacimiento = model.FechaNacimiento,
                Raza = model.Raza,
                RIdTipoAnimal = model.SelectedTipoAnimalId
            };

            dal.Insert(animal);

            // Redirigir a otra vista, como la lista de animales.
            return RedirectToAction("Index", "Home");
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
}
