using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using storeUserInterface.Models;
using Newtonsoft.Json;
using System.Text;
using AcademyLog;

namespace storeUserInterface.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            List<Producto> lsp = new List<Producto>();
            UsuarioConectado userLoged = new UsuarioConectado();
            Usuario usuario = new Usuario();
            //Las propiedades tienen que hacer match con el DTO para poder comunicarse.
            usuario.UserName = username;
            usuario.Password = password;
            //Consumir api para obtener account y password y validar
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://academysecurity.azurewebsites.net/");
            //Mandamos la peticion por el body hacia la api de security
            string json = JsonConvert.SerializeObject(usuario);
            var httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync("api/Sessions/Login", httpcontent);
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<UsuarioConectado>(readresult);
            if (resultadoFinal.IsLogged != false)
            {
                Session["username"] = resultadoFinal.Username;
                Session["rol"] = resultadoFinal.Role;
                //Crearmos el objeto que recibira nuestro metodo para la llamada a la api
                LogEntity logUI = new LogEntity();
                logUI.aplicacion = "User Interface";
                logUI.mensaje = "El usuario" + resultadoFinal.Username + "ha iniciado sesion";
                logUI.fecha = DateTime.Now;
                //Instanciamos el Log para poder consumir el metodo de conexion a la Api del archivo Dll
                Log log = new Log();
                log.ConnectToWebAPI(logUI);
                //return PartialView("~/Views/Producto/Todos.cshtml",lsp);
                return RedirectToAction("Todos", "Producto",new { id = 1 });
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }
        //[Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("username");
            Session.Remove("rol");
            Session.Remove("idPage");
            return RedirectToAction("Index");
        }
    }
}