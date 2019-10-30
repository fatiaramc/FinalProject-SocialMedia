using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Facebook.Models;
using Facebook.DataAccess;
using System.Data.SqlClient;

namespace Facebook.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonaDataService _dataService;

        public HomeController()
        {
            string connectionString = null;
            //SqlConnection cnn;
            connectionString = "Data Source = DESKTOP-F4DEC2L\\SQLEXPRESS; initial catalog = Facebook;integrated security = True";
            _dataService = PersonaDataService.GetPersonaDataService(connectionString);
        }

        public IActionResult Index()
        {

            /*var x = _dataService.RegistrarPersona(new Persona
            {
                Nombre = "Fatima",
                Apellido = "Arambula",
                correo_electronico = "fa.a@a.com",
                contraseña = "pass123",
                fecha_nac = "01/04/1998"
            });*/

            /*var y = _dataService.RegistrarPersona(new Persona
            {
                Nombre = "Liliana",
                Apellido = "Coronado",
                correo_electronico = "l@c.c",
                contraseña = "pass123",
                fecha_nac = "08/22/1973"
            });*/

            //aqui tengo que modificar que mande el id que es y no 1
            var amigos = _dataService.GetAmigos(1);
            ViewData["Message"] = "Message";
            return View(amigos);
        }

        [HttpPost]
        public ActionResult AddView(Persona persona)
        {
            if (!ModelState.IsValid) return View(persona);

            var result = _dataService.RegistrarPersona(persona);
            if (result)
                return RedirectToAction("Index");

            return View(persona);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
