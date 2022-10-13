using ApiEscola.Context;
using ApiEscola.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEscola.Services
{
    public class AlunosService : IAlunoService
    {

        private readonly AppDbContext _apiContext;

        public AlunosService(AppDbContext dbContext)
        {
            this._apiContext = dbContext;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                var alunos = await this._apiContext.Alunos.AsNoTracking().ToListAsync();
                return alunos;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Aluno> GetAlunoById(int id)
        {
            try
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return await this._apiContext.Alunos.FindAsync(id);
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Aluno>> GetAlunoByNome(string nome)
        {
            try
            {
                IEnumerable<Aluno> alunos;

                if (!string.IsNullOrEmpty(nome))
                {
                    alunos = await this._apiContext.Alunos.Where(aluno => aluno.Nome == nome).ToListAsync();
                    return alunos;
                }
                else
                {
                    alunos = await this.GetAlunos();
                    return alunos;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task CreateAluno(Aluno aluno)
        {
            try
            {
                this._apiContext.Alunos.Add(aluno); // Aqui estou apenas trabalhando no contexto da memória
                await this._apiContext.SaveChangesAsync(); //Aqui estou especificamente persistindo o aluno no banco de dados
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            try
            {
                this._apiContext.Entry(aluno).State = EntityState.Modified;
                await this._apiContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task DeleteAluno(int id)
        {
            try
            {
                Aluno aluno = await GetAlunoById(id).ConfigureAwait(false);
                this._apiContext.Alunos.Remove(aluno);
                await this._apiContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
