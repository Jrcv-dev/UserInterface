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



        public ActionResult Todos()
        {
            List<Producto> products = new List<Producto>();

            //Consumir api para obtener los productos
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            var response = client.GetAsync("/api/products/page/1");
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<List<Producto>>(readresult);
            return View(resultadoFinal);
        }
    }
}
