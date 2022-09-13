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
            this._contatoRepositorio.Adicionar(contato);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(ContatoModel contato)
        {
            this._contatoRepositorio.AtualizarContato(contato);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(IFormCollection value)
        {
            Int32.TryParse(value["id"], out int ident);
            Console.WriteLine(ident);
            Console.WriteLine(ident.GetType());
            this._contatoRepositorio.Deletar(ident);
            return RedirectToAction("Index");
        }


    }
}
