using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TI.SsistemaVoo.Web.Interfaces;
using TI.SsistemaVoo.Web.Models;

namespace TI.SsistemaVoo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGrafoHandler _grafoHandler;

        public HomeController(IGrafoHandler grafoHandler)
        {
            _grafoHandler = grafoHandler;
        }

        public IActionResult Index()
        {
            var grafoVooRotas = _grafoHandler.GetGrafoFromDB();
            ViewData["Grafo"] = grafoVooRotas;
            return View();
        }

   
        [HttpPost]
        [Route("SearchFlights")]
        public IActionResult SearchFlights(IFormCollection parameters)
        {
            //var grafoVooRotas = _grafoHandler.GetGrafoFromDB();
            //var caminho = grafoVooRotas.EncontraMelhorCaminho(idOrigemDD, idDestinoDD);
            //ViewData["Caminho"] = caminho;
            //return Redirect("");
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
