using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using storeUserInterface.Models;
using Newtonsoft.Json;

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
            /*UsuarioConectado userLoged = new UsuarioConectado();
            Usuario usuario = new Usuario();
            usuario.user = username;
            usuario.password = password;
            //Consumir api para obtener account y password y validar
            var client = new HttpClient();
            client.BaseAddress = new Uri("AQUI VA EL URL DE LA API");
            var response = client.GetAsync("url?"+usuario);
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<UsuarioConectado>(readresult);
            if (resultadoFinal.isLoged != false)
            {
                Session["username"] = resultadofinal.username;
                Session["roll"] = resultadofinal.roll;
                return View("Productos");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }*/
            if (username.Equals("acc1") && password.Equals("123") || username.Equals("jesus") && password.Equals("microservicios123"))
            {
                //Validar el rol de la persona si es admin(empleado) o cliente para saber que vista mostrar(hacerlo en html)

                Session["username"] = username;
                return View("Success");//Vista Productos
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
            return RedirectToAction("Index");
        }
    }
}