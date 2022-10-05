using ControleContatos.Helper;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            this._sessao = sessao;
        }

        public IActionResult Index()
        {
            //Se ja estiver logado, irá redirecionar para a pagina home de uma vez. Sem que seja necessario refazer o login
            
            return this._sessao.getSession()!=null ? RedirectToAction("Index","Home") : View("Index");
        }

        [HttpPost]
        public IActionResult SignIn(IFormCollection value)
        {
            try
            {
                var hashPass = _usuarioRepositorio.GetHashPass(value["senha"]);

                if (this._usuarioRepositorio.ValidaSignIn(value["login"], hashPass))
                {
                    var usuario = _usuarioRepositorio.GetUserByLogin(value["login"]);
                    _sessao.createSession(usuario);

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
    
        public IActionResult Logout()
        {
            this._sessao.deleteSession();
            return RedirectToAction("Index");
        }
    }
}
