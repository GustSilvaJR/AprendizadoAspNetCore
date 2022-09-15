using ControleContatos.Models;
using System.Drawing;

namespace ControleContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        public void CreateUser(UsuarioModel usuario);

        public void EditUser(UsuarioModel userModified);

        public void DeleteUser(int id);

        public string GetHashPass(string pass);

        public UsuarioModel GetUserById(int id);

        public List<UsuarioModel> GetUsers();

        
    }
}
