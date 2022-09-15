using ControleContatos.Models;
using ControleContatos.Data;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ControleContatos.Repositorio
{
    public class UsuarioRepositorio:IUsuarioRepositorio
    {

        private readonly BancoContext _bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel GetUserById(int id)
        {
            var user = this._bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public List<UsuarioModel> GetUsers()
        {
            var usuarios = _bancoContext.Usuarios.ToList();
            return usuarios;
        }

        public string GetHashPass(string pass)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public void CreateUser(UsuarioModel usuario)
        {
            var date = (DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
            

            usuario.PerfilUser = Enum.Perfil.Common;
            usuario.Senha = GetHashPass(usuario.Senha);
            usuario.DataCriacao = date;
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
        }

        public void EditUser(UsuarioModel userModified)
        {
            var userBD = this.GetUserById(userModified.Id);

            if(userBD != null)
            {
                userBD.Name = userModified.Name;
                userBD.Email = userModified.Email;
                userBD.Login = userModified.Login;
                userBD.Senha = this.GetHashPass(userModified.Senha);

                this._bancoContext.Usuarios.Update(userBD);
                this._bancoContext.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = this.GetUserById(id);
            this._bancoContext.Usuarios.Remove(user);
            this._bancoContext.SaveChanges();
        }
    }
}
