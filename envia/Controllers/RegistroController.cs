using Microsoft.AspNetCore.Mvc;
using envia.Models;

namespace MiApp.Controllers
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
            var registro = _data.GetById(id);
            if (registro == null)
            {
                return NotFound();
            }
            return View(registro);
        }

        [HttpPost]
        public IActionResult Edit(Registro r)
        {
            Console.WriteLine(">> POST Edit llamado");
            Console.WriteLine($">> ID recibido: {r.Id}");
            Console.WriteLine($">> Nombre: {r.Nombre}, Apellido: {r.Apellido}, Cargo: {r.Cargo}");

            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");

            if (ModelState.IsValid)
            {
                Console.WriteLine(">> ModelState es válido, actualizando...");
                _data.Update(r);
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine(">> ModelState inválido");
            return View(r);
        }

        public IActionResult Delete(int id)
        {
            if (!EstaAutenticado()) return RedirectToAction("Login", "Auth");
            var registro = _data.GetById(id);
            if (registro == null)
            {
                return NotFound();
            }
            return View(registro);
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

