using Animales.DAL;  
using First_ASPNET_Core_MVC.Models.ViewModels; 
using First_ASPNET_Core_MVC.Models; 
using Microsoft.AspNetCore.Mvc;  
using System.Diagnostics; 

namespace First_ASPNET_Core_MVC.Controllers
{
    // Definición del controlador AnimalController, que maneja las acciones relacionadas con los animales.
    public class AnimalController : Controller
    {
        // Acción HTTP GET para obtener los detalles de un animal por su ID.
        [HttpGet]
        public IActionResult Details(int id)
        {
            // Instancia del DAL (Data Access Layer) para obtener los datos del animal.
            AnimalDAL dal = new AnimalDAL();

            // Crear una instancia del ViewModel con los datos del animal obtenidos a través del DAL.
            DetailsAnimalViewModel viewModel = new DetailsAnimalViewModel
            {
                animal = dal.GetById(id)  // Obtiene el animal por su ID y lo asigna al ViewModel.
            };

            // Devuelve la vista con el ViewModel que contiene los detalles del animal.
            return View("Views/Animal/Details.cshtml", viewModel);
        }

        // Acción HTTP GET para mostrar el formulario de agregar un nuevo animal.
        [HttpGet]
        public IActionResult Agregar()
        {
            // Crear instancia del DAL de TipoAnimal para obtener la lista de tipos de animales.
            TipoAnimalDAL tipoAnimalDal = new TipoAnimalDAL();

            // Obtener todos los tipos de animales desde la base de datos.
            List<TipoAnimal> tiposDeAnimales = tipoAnimalDal.GetAll();

            // Crear el ViewModel con la lista de tipos de animales.
            AgregarViewModel viewModel = new AgregarViewModel
            {
                TiposDeAnimales = tiposDeAnimales  // Asignamos la lista de tipos de animales al ViewModel.
            };

            // Devuelve la vista de "Agregar" pasando el ViewModel con la lista de tipos.
            return View(viewModel);
        }

        // Acción HTTP POST para confirmar y guardar el nuevo animal ingresado.
        [HttpPost]
        public IActionResult Confirmar(AgregarViewModel model)
        {
            // Verifica si el modelo es válido (incluye validaciones definidas en el ViewModel).
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, recarga la lista de tipos para mostrar la vista nuevamente.
                TipoAnimalDAL tipoAnimalDal = new TipoAnimalDAL();
                model.TiposDeAnimales = tipoAnimalDal.GetAll();  // Se vuelve a cargar la lista de tipos.

                // Devuelve la vista "Agregar" con el modelo actual para que el usuario corrija los errores.
                return View("Agregar", model);
            }

            // Si el modelo es válido, creamos la instancia del DAL de animales.
            AnimalDAL dal = new AnimalDAL();

            // Crear un nuevo objeto de tipo Animal con los datos proporcionados en el modelo.
            Animal animal = new Animal
            {
                NombreAnimal = model.NombreAnimal,  // Asignamos el nombre del animal.
                FechaNacimiento = model.FechaNacimiento,  // Asignamos la fecha de nacimiento.
                Raza = model.Raza,  // Asignamos la raza del animal.
                RIdTipoAnimal = model.SelectedTipoAnimalId  // Asignamos el ID del tipo de animal seleccionado.
            };

            // Llamamos al método de inserción del DAL para guardar el nuevo animal en la base de datos.
            dal.Insert(animal);

            // Redirige a la vista principal (por ejemplo, la lista de animales en la página de inicio).
            return RedirectToAction("Index", "Home");
        }

        // Acción para la página de privacidad.
        public IActionResult Privacy()
        {
            return View();  // Devuelve la vista de privacidad.
        }

        // Acción para manejar errores, con cache deshabilitada.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Devuelve una vista de error con el identificador de la solicitud actual o el identificador de traza.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
