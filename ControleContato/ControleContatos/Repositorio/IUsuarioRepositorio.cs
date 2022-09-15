using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        public void CreateUser(UsuarioModel usuario);

        public List<UsuarioModel> GetUsers();
    }
}
