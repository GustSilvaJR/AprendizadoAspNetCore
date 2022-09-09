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
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContatoModel contato)
        {
            this._contatoRepositorio.Adicionar(contato);
            return RedirectToAction("Index");
        }


    }
}
