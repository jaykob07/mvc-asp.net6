using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using envia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace envia.Controllers
{
    public class AuthController : Controller
    {
        private readonly UsuarioData _usuarioData;

        public AuthController(IConfiguration config)
        {
            _usuarioData = new UsuarioData(config);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (_usuarioData.ValidarUsuario(email, password))
            {
                HttpContext.Session.SetString("usuario", email);
                return RedirectToAction("Index", "Registro");
            }
            ViewBag.Error = "Credenciales inválidas";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

