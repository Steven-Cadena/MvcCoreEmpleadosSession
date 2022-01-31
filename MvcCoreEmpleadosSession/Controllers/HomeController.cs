using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcCoreEmpleadosSession.Extensions;
using MvcCoreEmpleadosSession.Models;
using MvcCoreEmpleadosSession.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Controllers
{
    public class HomeController : Controller
    {
        private RepositoryEmpleados repo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string apellido)
        {
            Empleado empleado = this.repo.BuscarEmpleado(apellido);
            HttpContext.Session.SetObject("EMPLEADO", empleado);
            return RedirectToAction("Index");
        }
        public IActionResult CloseSession() 
        {
            HttpContext.Session.Remove("EMPLEADO");
            return RedirectToAction("Index");
        }
        public IActionResult LimpiarCarritoEmpleados() 
        {
            HttpContext.Session.Remove("IDSEMPLEADOS");
            return RedirectToAction("Index");
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
    }
}
