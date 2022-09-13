using ControleContatos.Data;
using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        
        public ContatoRepositorio(BancoContext banco)
        {
            this._bancoContext = banco;
        }

        public ContatoModel ContatoPorId(int id)
        {
            var contato = this._bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
            return contato;
        }
        
        public List<ContatoModel> Listar()
        {
            return this._bancoContext.Contatos.ToList();
        }

        public void AtualizarContato(ContatoModel contato)
        {
            var contatoAlterar = this.ContatoPorId(contato.Id);


            if(contatoAlterar == null) throw new System.Exception("Houve um erro na atualização do contato!");
           
            contatoAlterar.Name = contato.Name;
            contatoAlterar.Email = contato.Email;
            contatoAlterar.Cellphone = contato.Cellphone;

            this._bancoContext.Contatos.Update(contatoAlterar);
            this._bancoContext.SaveChanges();

        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            this._bancoContext.Contatos.Add(contato);
            this._bancoContext.SaveChanges();

            return contato;
        }

        public void Deletar(int id)
        {
            var contatoApagar = this.ContatoPorId(id);
            this._bancoContext.Contatos.Remove(contatoApagar);
            this._bancoContext.SaveChanges();
        }
    }
}
