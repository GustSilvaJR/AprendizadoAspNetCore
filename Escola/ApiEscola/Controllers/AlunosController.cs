using ApiEscola.Models;
using ApiEscola.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEscola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Aqui está herdando de controllerBase e nao de controller devido ao fato de Controller herdar de ControllerBase e conter algumas novas inserções especificas para tratamento de views. E como estou trbalhando com web Api's nao sera necessario
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            this._alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos() //IAsyncEnumerable<tipo> eu defino que vou retornar uma lista de um determinado tipo
        {
            try
            {
                var alunos = await this._alunoService.GetAlunos();
                return alunos.Count() == 0 ? StatusCode(StatusCodes.Status404NotFound, "Aluno não encontrado") : StatusCode(StatusCodes.Status200OK, alunos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoByName([FromQuery] string nome) //Com FromQuery eu defino que irei receber um parâmetro chave valor na url, como: http:foo.com/home?nome=teste Após o interroga se tem o query param.
        {
            try
            {
                var alunos = await this._alunoService.GetAlunoByNome(nome);
                return alunos.Count()==0 ? StatusCode(StatusCodes.Status404NotFound, "Aluno não encontrado") : StatusCode(StatusCodes.Status200OK, alunos);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpGet("{id:int}", Name="GetAluno")] //Por essa tag name eu defino o nome da minha rota e com {id:int} defini que a rota vai receneru um parametro
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAluno(int id)
        {
            try
            {
                var aluno = await this._alunoService.GetAlunoById(id);
                
                return aluno==null ? StatusCode(StatusCodes.Status404NotFound, "Aluno não encontrado") :  StatusCode(StatusCodes.Status200OK, aluno);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                var alunoValidator = await this._alunoService.GetAlunoByEmail(aluno.Email);

                if (!alunoValidator)
                {
                    await this._alunoService.CreateAluno(aluno);
                    return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Aluno já existente");
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao cadastrar aluno");
            }
        }

        [HttpPut("{id:int}", Name="UpdateAluno")]
        public async Task<ActionResult> Edit(int id, Aluno aluno)
        {
            try
            {
                if (await this._alunoService.GetAlunoByEmail(aluno.Nome))
                {
                    if (id == aluno.Id)
                    {
                        await this._alunoService.UpdateAluno(aluno);
                        return StatusCode(StatusCodes.Status200OK, $"Aluno com id: {id} atualizado com sucesso. \n{aluno}");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized, "Não é permitido alterar o id do usuário");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Aluno inexistente");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar aluno");
            }
            
        }

        [HttpDelete("{id:int}", Name="DeleteUser")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await this._alunoService.DeleteAluno(id);
                return StatusCode(StatusCodes.Status200OK, $"Aluno com id: {id} deletado com sucesso.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar aluno");
            }
        }
    }
}
