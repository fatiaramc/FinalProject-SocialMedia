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

        Notificacion notificacion;


        public HomeController()
        {
            UsuarioActual.GetUsuarioActual().ActualizarAmigos();
            UsuarioActual.GetUsuarioActual().ActualizarPostAmigos();
            UsuarioActual.GetUsuarioActual().ActualizarPostAmigos();
            UsuarioActual.GetUsuarioActual().ActualizarPersonas();
            UsuarioActual.GetUsuarioActual().ActualizarNotificaciones();
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

        public IActionResult Index1()
        {
            return View("Index1");
        }

        public IActionResult Perfil()
        {
            return View("Perfil");
        }

        public IActionResult BusquedaHashtag()
        {
            return View("BusquedaHashtag");
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
                    UsuarioActual.GetUsuarioActual().ActualizarNotificaciones();
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
            UsuarioActual.GetUsuarioActual().GetUser().descripcion = desc.Descripcion;

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
            var l = post.likes;
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult LikePost(AdapterDesc idPost)
        {
            var id = Convert.ToInt32(idPost.Descripcion);
            _dataService.LikePost(id);
            //var p = UsuarioActual.GetUsuarioActual().GetMisPosts().Find(item => item.idPost == id);
            //var l = p.likes;
            //p.likes++;
            UsuarioActual.GetUsuarioActual().ActualizarMisPost();
            UsuarioActual.GetUsuarioActual().ActualizarPostAmigos();

            var post = _dataService.GetPostWithId(id)[0];
            notificacion = new NotificacionLike(post.idPersona, UsuarioActual.GetUsuarioActual().GetUser().idPersona, id);
            notificacion.setTipo();
            notificacion = new Like(notificacion);
            notificacion.setTipo();
            notificacion.Notificar();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult CommentPost(AdapterId idPost)
        {
            var id = Convert.ToInt32(idPost.id);
            var c = idPost.comentario;
            _dataService.AgregarComentario(UsuarioActual.GetUsuarioActual().GetUser().idPersona, c, id);
            //_dataService.LikePost(id);
            UsuarioActual.GetUsuarioActual().ActualizarMisPost();
            UsuarioActual.GetUsuarioActual().ActualizarPostAmigos();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult SearchFriend(AdapterId busqueda)
        {
            UsuarioActual.GetUsuarioActual().ActualizarAmigos();

            var bus = busqueda.id;
            var op = Convert.ToInt32(busqueda.comentario);
            SearchClass search = new SearchClass(new SearchName(), bus);
            if (op == 2) search = new SearchClass(new SearchLastname(), bus);
            else if (op == 3) search = new SearchClass(new SearchCorreo(), bus);
            var result = search.Search();
            UsuarioActual.GetUsuarioActual().Buscar(result);
            return Json(new { success = true, res = result, redirecturl = Url.Action("Index1", "Home") });
        }

        [HttpPost]
        public IActionResult AgregarAmigo(AdapterId ids)
        {
            var id1 = Convert.ToInt32(ids.id);
            var id2 = Convert.ToInt32(ids.comentario);

            _dataService.AgregarAmigo(id1, id2);
            return Json(new { success = true });

        }
        [HttpPost]
        public IActionResult EliminarAmigo(AdapterId ids)
        {
            var id1 = Convert.ToInt32(ids.id);
            var id2 = Convert.ToInt32(ids.comentario);

            _dataService.EliminarAmigo(id1, id2);
            return Json(new { success = true });

        }

        [HttpPost]
        public IActionResult VerPerfil(AdapterDesc ids)
        {
            var id = Convert.ToInt32(ids.Descripcion);
            UsuarioActual.GetUsuarioActual().ObtenerPerfilAmigo(id);
            return Json(new { success = true, redirecturl = Url.Action("Index1", "Home") });
        }

        [HttpPost]
        public IActionResult AgregarPost(AdapterId r)
        {
            var m = r.id;
            var url = r.comentario;
            if(url == null)
            {
                url = "";
            }
            var res = _dataService.AgregarPost(m, UsuarioActual.GetUsuarioActual().GetUser().idPersona,url);
            UsuarioActual.GetUsuarioActual().ActualizarMisPost();
            BusquedaTexto busqueda = new BusquedaTexto(m);
            BuscarHashtag opBuscarHashtag = new BuscarHashtag(busqueda);
            BuscarEtiqueta opBuscarEtiqueta = new BuscarEtiqueta(busqueda);
            Invoker invoker = new Invoker();
            invoker.recibirOperacion(opBuscarHashtag);
            invoker.recibirOperacion(opBuscarEtiqueta);
            invoker.realizarOperaciones();
            var q = busqueda.resultado;
            var e = busqueda.resultadoEtiquetas;
            foreach(var h in q)
            {
                _dataService.AgregarHashtag(h, res.Item2);
            }
            foreach(var ee in e)
            {
                Persona amigo = UsuarioActual.GetUsuarioActual().GetAmigos().Find(item => item.Nombre == ee.Item1 && item.Apellido == ee.Item2);
                if(amigo != null)
                {
                    _dataService.AgregarEtiqueta(res.Item2, amigo.idPersona);
                    //id dueño, id que etiqueta, post
                    notificacion = new NotificacionLike(amigo.idPersona, UsuarioActual.GetUsuarioActual().GetUser().idPersona, res.Item2);
                    notificacion = new Mencion(notificacion);
                    notificacion.setTipo();
                    notificacion.Notificar();
                }
            }
            
            return Json(new { success = true });
        }

       [HttpPost]
       public IActionResult BuscarHashtags(AdapterDesc d)
       {
            var hash = d.Descripcion;
            var res = _dataService.GetHashtags(hash);
            var list = new List<IPost>();

            foreach(var id in res)
            {
                var aux = _dataService.GetPostWithId(id)[0];
                list.Add(aux);
            }
            UsuarioActual.GetUsuarioActual().SetBusquedaHashtags(list);
            return Json(new { success = true, redirecturl = Url.Action("BusquedaHashtag", "Home") });
       }

        [HttpPost]
        public IActionResult VerPerfilAlClick(AdapterDesc idPersona)
        {
            var id = Convert.ToInt32(idPersona.Descripcion);
            UsuarioActual.GetUsuarioActual().ObtenerPerfilAmigo(id);
            return Json(new { success = true, redirecturl = Url.Action("Perfil", "Home") });
        }
    }
}
