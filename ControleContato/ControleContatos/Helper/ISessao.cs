using ControleContatos.Models;

namespace ControleContatos.Helper
{
    public interface ISessao
    {
        void createSession(UsuarioModel usuario);
        void deleteSession();
        UsuarioModel getSession();
    }
}
