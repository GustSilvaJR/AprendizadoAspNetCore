using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        void AtualizarContato(ContatoModel contato);
        ContatoModel ContatoPorId(int id);
        List<ContatoModel> Listar();
        ContatoModel Adicionar(ContatoModel contato);
        void Deletar(int id);
    }
}
