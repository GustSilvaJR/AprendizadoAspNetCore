using ApiEscola.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEscola.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuider){
            modelBuider.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Gustavo Alessandro",
                    Email = "gust.offic@gmail.com",
                    Idade = 21
                },
                new Aluno 
                {
                    Id = 2,
                    Nome = "teste digisul",
                    Email = "teste@gmail.com",
                    Idade = 99
                }
            );
        }

    }
}
