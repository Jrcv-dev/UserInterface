using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using storeUserInterface.Models;

namespace storeUserInterface.Controllers
{
    public class RegistrarUsuarioController : Controller
    {
        // GET: RegistrarUsuario
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(RegisterUser model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://academysecurity.azurewebsites.net/");
            //Mandamos la peticion por el body hacia la api de security
            string json = JsonConvert.SerializeObject(model);
            var httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync("api/NewUser/CreateUser", httpcontent);
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            //var resultadoFinal = JsonConvert.DeserializeObject<Boolean>(readresult);
            return RedirectToAction("Index", "Home");
        }
    }
}