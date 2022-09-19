using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(IFormCollection value)
        {
            try
            {
                var hashPass = _usuarioRepositorio.GetHashPass(value["senha"]);

                if (this._usuarioRepositorio.ValidaSignIn(value["login"], hashPass))
                {
                    TempData["MensagemSucesso"] = $"Login: {value["login"]} \n | Senha: {value["senha"]} \n Logado com Sucesso!!";
                    return RedirectToAction("Index", "Home");
                }

                TempData["MensagemErro"] = $"Login e/ou senha inválidos.";  
                return RedirectToAction("Index");
            }
            catch(System.Exception error)
            {
                TempData["MensagemErro"] = $"Não foi possível efetuar o login, tente novamente mias tarde. Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
