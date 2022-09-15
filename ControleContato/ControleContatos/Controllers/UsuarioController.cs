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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = this._usuarioRepositorio.GetUserById(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult VerifyDelete(int id, string name)
        {
            string[] data = null;

            data = new string[2];

            data[0] = id.ToString();
            data[1] = name;
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._usuarioRepositorio.CreateUser(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                }
                else
                {
                    return View();
                }

                
            }
            catch(System.Exception error)
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar o usuário, tente novamente. Detalhe do erro: {error.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(UsuarioModel user)
        {
            try
            {
                this._usuarioRepositorio.EditUser(user);
                TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
            }
            catch(System.Exception error)
            {
                Console.WriteLine("Erro Estourado: " + error);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                this._usuarioRepositorio.DeleteUser(id);
                TempData["MensagemSucesso"] = "Usuário apagado com sucesso";
            }
            catch(System.Exception error)
            {
                Console.WriteLine("Erro estourado: " + error);
            }

            return RedirectToAction("Index");
        }
    }
}
