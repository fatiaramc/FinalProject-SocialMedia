using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Facebook.Models;
using Facebook.DataAccess;
using System.Data.SqlClient;
using System.Web;

namespace Facebook.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonaDataService _dataService;

        public Persona _usuario = null;

        public UsuarioActual _usuarioActual = null;

        public HomeController()
        {
            //string connectionString = null;
            //SqlConnection cnn;
            //connectionString = "Data Source = DESKTOP-F4DEC2L\\SQLEXPRESS; initial catalog = Facebook;integrated security = True";
            _dataService = PersonaDataService.GetPersonaDataService();
            _usuarioActual = UsuarioActual.GetUsuarioActual();
        }

        public IActionResult Index()
        {
            UsuarioActual.GetUsuarioActual().ClearUser();

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
            //var amigos = _dataService.GetAmigos(1);
            //Post p = new ImagePostCreator("Nuevo post", 4, "blablablaimagen");
            //var k = p.CreatePost();
            //k.PublishPost();
            /*Post p = new MessagePostCreator("test post", 4);
            var k = p.CreatePost();
            k.PublishPost();
            var posts = _dataService.GetPosts(1);*/

            
            ViewData["Message"] = "Message";
            return View();
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
            ViewData["Desc"] = "";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            //ViewData["UserName"] = _usuario.Nombre;
            //j = sessionStorage.getItem('idusuario');
            return View("Contact");
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

        [HttpPost]
        public void RegistrarPersona(Persona p)
        {
            DataAccess.PersonaDataService.GetPersonaDataService().RegistrarPersona(p);
            Console.Write("HOLA");
        }

        [HttpPost]
        public IActionResult LoginWithEmail(Persona p)
        {
            var x = DataAccess.PersonaDataService.GetPersonaDataService().GetPersonaWithEmail(p);
            if (x.Count != 0)
            {
                //return Redirect("https://www.w3schools.com");
                //Response.Redirect("Contact");
                //Response.Redirect("~/HomeController/Contact");
                //return View("Contact");
                //esto regresa a ajax function
                //return Json(new { success = true, responseText = "Done" });
                if(x[0].contraseña == p.contraseña)
                {
                    _usuario = x[0];
                    _usuarioActual.SetUserActual(_usuario);
                    return Json(new { success = true, redirecturl = Url.Action("Contact", "Home"), responseText = "Done", usuario = _usuario });
                    //return Json(new { success = true, redirecturl = Url.Action("About", "Home"), responseText = "Done", usuario = _usuario });
                }
                else
                {
                    //error en contraseña
                }
                

            }
            return Content("Error");
            
        }

        [HttpPost]
        public void VerifyEmail(Persona p)
        {
            var x = DataAccess.PersonaDataService.GetPersonaDataService().GetPersonaWithEmail(p);
        }

        [HttpPost]
        public IActionResult GetUserWithID(string id)
        {
            var id2 = -1;
            Int32.TryParse(id,out id2);
            var x = DataAccess.PersonaDataService.GetPersonaDataService().GetPersonaWithId(id2);
            if(x.Count != 0)
            {
                _usuario = x[0];
                return Json(new { success = true, usuario = _usuario, id = _usuario.idPersona });
            }
            return Content("Error");
        }

        [HttpPost]
        public IActionResult EditUserDesc(AdapterDesc desc)
        {
            Persona p = UsuarioActual.GetUsuarioActual().GetUser();
            p.descripcion = desc.Descripcion;

            var result = DataAccess.PersonaDataService.GetPersonaDataService().EditarPersona(p);
            if (!result)
            {
                return Json(new { success = true });
            }
            return Content("Error");
        }

        [HttpPost]
        public IActionResult EditUser(Persona p)
        {
            var result = DataAccess.PersonaDataService.GetPersonaDataService().EditarPersona(p);
            if (result)
            {
                UsuarioActual.GetUsuarioActual().ActualizarUsuario();
                return Json(new { success = true });
            }
            return Content("Error");
        }

        [HttpPost]
        public IActionResult Like(IPost post)
        {
            post.Like();
            return Json(new { success = true });
        }
    }
}
