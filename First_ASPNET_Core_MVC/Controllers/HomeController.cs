using System.Diagnostics;  
using Animales.DAL;  
using First_ASPNET_Core_MVC.Models; 
using First_ASPNET_Core_MVC.Models.ViewModels;  
using Microsoft.AspNetCore.Mvc;  

namespace First_ASPNET_Core_MVC.Controllers
{
    // Definici�n del controlador principal de la aplicaci�n, HomeController.
    public class HomeController : Controller
    {
        // Inyecci�n de dependencias para el registro de logs (uso de ILogger).
        private readonly ILogger<HomeController> _logger;

        // Constructor que recibe un servicio ILogger para registrar informaci�n de logs.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;  // Asignaci�n del logger inyectado.
        }

        // Acci�n para la p�gina de inicio, que obtiene la lista de todos los animales.
        public IActionResult Index()
        {
            // Crear una instancia del DAL (Data Access Layer) para acceder a los datos de animales.
            AnimalDAL dal = new AnimalDAL();

            // Crear un ViewModel para almacenar los animales obtenidos.
            AnimalViewModel viewModel = new AnimalViewModel();
            viewModel.Animales = dal.GetAll();  // Obtener todos los animales desde la base de datos.

            // Devuelve la vista de inicio con el ViewModel que contiene la lista de animales.
            return View(viewModel);
        }

        // Acci�n para redirigir a la vista de detalles de un animal, pasando su ID.
        public IActionResult DetailsAction(int id)
        {
            // Redirige al controlador "Animal" y a la acci�n "Details" pasando el ID del animal.
            return RedirectToAction("Details", "Animal", new { id = id });
        }

        // Acci�n para redirigir a la vista de agregar un nuevo animal.
        public IActionResult AgregarAction()
        {
            // Redirige al controlador "Animal" y a la acci�n "Agregar", que muestra el formulario de agregar.
            return RedirectToAction("Agregar", "Animal");
        }

        // Acci�n para mostrar la p�gina de privacidad.
        public IActionResult Privacy()
        {
            return View();  // Devuelve la vista de privacidad.
        }

        // Acci�n para manejar errores, con la cache desactivada para no almacenar los errores.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Devuelve una vista de error con un identificador de solicitud actual o el identificador de traza.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
