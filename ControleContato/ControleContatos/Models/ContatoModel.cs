using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ControleContatos.Models
{
    public class ContatoModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do contato")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do contato")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage = "O celular informado não é válido!")]
        public string? Cellphone{ get; set; }

    }
}
