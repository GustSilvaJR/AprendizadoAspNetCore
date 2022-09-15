using Microsoft.EntityFrameworkCore;
using ControleContatos.Models;

namespace ControleContatos.Data
{
    //Herdando contexto a partir do modulo entity framework core
    public class BancoContext : DbContext
    {
        //base-> Estou passando as informações vindas como parametros no construtor BancoContext para o DbContext do qual estou herdando
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {}

        //Criando referencia para uma tabela que eu va ter no banco. Como tipo passo o modelo que representa minha tabela
        public DbSet <ContatoModel>? Contatos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
