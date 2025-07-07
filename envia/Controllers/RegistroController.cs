using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using envia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace envia.Controllers
{
    public class RegistroController : Controller
    {
        private readonly RegistroData _data;

        public RegistroController(IConfiguration config)
        {
            _data = new RegistroData(config);
        }

        private bool EstaAutenticado() => !string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"));

        public IActionResult Index()
        {
            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");
            var lista = _data.GetAll();
            return View(lista);
        }

        public IActionResult Create()
        {
            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Registro r)
        {
            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");
            if (ModelState.IsValid)
            {
                _data.Insert(r);
                return RedirectToAction(nameof(Index));
            }
            return View(r);
        }

        public IActionResult Edit(int id)
        {
            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");
            return View(_data.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Registro r)
        {
            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");
            if (ModelState.IsValid)
            {
                _data.Update(r);
                return RedirectToAction(nameof(Index));
            }
            return View(r);
        }

        public IActionResult Delete(int id)
        {
            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");
            return View(_data.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");
            _data.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

