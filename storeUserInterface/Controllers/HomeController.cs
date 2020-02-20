using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using storeUserInterface.Models;
using Newtonsoft.Json;
using System.Text;

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
            UsuarioConectado userLoged = new UsuarioConectado();
            Usuario usuario = new Usuario();
            usuario.UserName = username;
            usuario.Password = password;
            //Consumir api para obtener account y password y validar
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://academysecurity.azurewebsites.net/");
            //var response = client.PostAsync("api/Sessions/Login"+usuario, new StringContent(""));
            string json = JsonConvert.SerializeObject(usuario);
            //string jsonData = @"{
            //   'Username':'Paco',
            //   'Password':'churrumais'
            //}";
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
                return View("Productos");
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