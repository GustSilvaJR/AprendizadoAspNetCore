using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> Listar();
        ContatoModel Adicionar(ContatoModel contato);
    }
}
