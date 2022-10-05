using ControleContatos.Models;
using Newtonsoft.Json;

namespace ControleContatos.Helper
{
    public class Sessao : ISessao
    {

        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void createSession(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            Console.WriteLine(valor);
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            _httpContext.HttpContext.Session.SetString("UserSessionLogged", valor);
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
        }

        public void deleteSession()
        {
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            _httpContext.HttpContext.Session.Remove("UserSessionLogged");
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
        }

        public UsuarioModel getSession()
        {
            string value = _httpContext.HttpContext.Session.GetString("UserSessionLogged");

            return (string.IsNullOrEmpty(value)) ? null : JsonConvert.DeserializeObject<UsuarioModel>(value);
        }
    }
}
