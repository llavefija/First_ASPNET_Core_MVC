using System.Diagnostics;
using Animales.DAL;
using First_ASPNET_Core_MVC.Models;
using First_ASPNET_Core_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace First_ASPNET_Core_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            AnimalDAL dal = new AnimalDAL();
            
            AnimalViewModel viewModel = new AnimalViewModel();
            viewModel.Animales = dal.GetAll();

            return View(viewModel);
        }

        public IActionResult DetailsAction(int id)
        {
            // Redirigir correctamente al controlador "Animal" y a la acción "Details".
            return RedirectToAction("Details", "Animal", new { id = id });
        }

        public IActionResult AgregarAction()
        {
            // Redirigir correctamente al controlador "Animal" y a la acción "Details".
            return RedirectToAction("Agregar", "Animal");
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
