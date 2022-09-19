using ControleContatos.Models;
using System.Drawing;

namespace ControleContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {

        public UsuarioModel GetUserById(int id);

        public List<UsuarioModel> GetUsers();

        public void CreateUser(UsuarioModel usuario);

        public void EditUser(UsuarioModel userModified);

        public void DeleteUser(int id);

        public bool ValidaSignIn(string login, string senha);

        public string GetHashPass(string pass);
 
    }
}
