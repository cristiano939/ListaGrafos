using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            var grafoVooRotas =  _grafoHandler.GetGrafoFromDB();
            return View();
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
