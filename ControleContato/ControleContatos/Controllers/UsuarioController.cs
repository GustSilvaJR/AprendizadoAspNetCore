using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var usuarios = _usuarioRepositorio.GetUsers();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UsuarioModel usuario)
        {
            try
            {
                this._usuarioRepositorio.CreateUser(usuario);
            }
            catch(System.Exception error)
            {
                Console.WriteLine("Excessão estourada: " + error.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
