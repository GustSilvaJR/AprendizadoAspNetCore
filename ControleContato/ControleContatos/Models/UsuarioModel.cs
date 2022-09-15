using ControleContatos.Enum;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ControleContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome do Usuario")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o Login do Usuario")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o E-mail do contato")]
        [EmailAddress(ErrorMessage = "O E-mail informado não é válido!")]
        public string Email { get; set; }
        public Perfil PerfilUser { get; set; }

        [Required(ErrorMessage = "Digite a Senha do Usuario")]
        public string Senha { get; set; }
        public string DataCriacao { get; set; }
        public string? DataAtualizacao { get; set; }
    }
}
