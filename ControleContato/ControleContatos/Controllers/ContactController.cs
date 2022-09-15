using ControleContatos.Data;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContact.Controllers
{
    
    public class ContactController : Controller
    {

        private  readonly IContatoRepositorio _contatoRepositorio;

        public ContactController(IContatoRepositorio contatoRepositorio)
        {
            this._contatoRepositorio = contatoRepositorio;
        }

        //Rotas GET
        //Por padrao essas rotas que nao recebem nenhum parametro utilizando o metodo GET

        [HttpGet]
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.Listar();

            return View(contatos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contato = _contatoRepositorio.ContatoPorId(id);
            return View(contato);
        }

        [HttpGet]
        public IActionResult VerifyDelete(int id)
        {
            var value = id;
            return View(value);
        }

        [HttpPost]
        public IActionResult Create(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(contato);
                }
            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar o contato, tente novamente. Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._contatoRepositorio.AtualizarContato(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $"Não foi possível alterar o contato, tente novamente. Detalhe do erro: {error.Message}";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(IFormCollection value)
        {
            try
            {
                Int32.TryParse(value["id"], out int ident);
                this._contatoRepositorio.Deletar(ident);
                TempData["MensagemSucesso"] = "Contato excluído com sucesso";

                return RedirectToAction("Index");
            }
            catch(System.Exception error)
            {
                TempData["MensagemErro"] = $"Não foi possível excluir o contato, tente novamente. Detalhe do erro: {error.Message}";

                return RedirectToAction("Index");
            }
        }

    }
}
