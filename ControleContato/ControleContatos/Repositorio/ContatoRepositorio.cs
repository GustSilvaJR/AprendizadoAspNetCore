using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        
        public ContatoRepositorio(BancoContext banco)
        {
            this._bancoContext = banco;
        }
        
        public List<ContatoModel> Listar()
        {
            return this._bancoContext.Contatos.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            this._bancoContext.Contatos.Add(contato);
            this._bancoContext.SaveChanges();

            return contato;
        }
    }
}
