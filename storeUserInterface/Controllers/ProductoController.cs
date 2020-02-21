using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using storeUserInterface.Models;

namespace storeUserInterface.Controllers
{
    public class ProductoController : Controller
    {
        string Baseurl = "http://academyproducts.azurewebsites.net/swagger/ui/index#/";

        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            //Consumir api para obtener un solo producto
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            var response = client.GetAsync("/api/products/" + id.ToString());
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<Producto>(readresult);
            return View(resultadoFinal);

        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

<<<<<<< HEAD


        public ActionResult Todos(int id)
=======
       
        public ActionResult Todos()
>>>>>>> c0db04cae3e683c2da096b5909b6eee7b80cef80
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Producto> products = new List<Producto>();

            //Consumir api para obtener los productos
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            var response = client.GetAsync("/api/products/page/" + id.ToString());
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<List<Producto>>(readresult);//Modelo productos


            //Consumir api para obtener el número de páginas
            var clientpag = new HttpClient();
            clientpag.BaseAddress = new Uri(Baseurl);
            var responsepag = clientpag.GetAsync("/api/products/page/numPages");
            responsepag.Wait();
            var resultpag = responsepag.Result;
            var readresultpag = resultpag.Content.ReadAsStringAsync().Result;
            int numPaginas = Convert.ToInt32(readresultpag);
            //var numPaginas = (int) JsonConvert.DeserializeObject<int>(readresult);//Número de páginas


            var tupleData = new Tuple<List<Producto>, int>(resultadoFinal, numPaginas);
            return View(tupleData);

            //return View(resultadoFinal);
        }
        
    }
}
