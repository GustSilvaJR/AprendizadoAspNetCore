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

        public void CreateUser(UsuarioModel usuario)
        {
            var date = (DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(usuario.Senha));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            usuario.PerfilUser = Enum.Perfil.Common;
            usuario.Senha = sBuilder.ToString();
            usuario.DataCriacao = date;
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
        }

        public List<UsuarioModel> GetUsers()
        {
            var usuarios = _bancoContext.Usuarios.ToList();
            return usuarios;
        }

    }
}
