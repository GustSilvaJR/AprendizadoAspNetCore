using ControleContatos.Enum;

namespace ControleContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public Perfil PerfilUser { get; set; }
        public string Senha { get; set; }
        public string DataCriacao { get; set; }
        public string? DataAtualizacao { get; set; }
    }
}
