using FirstAppCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstAppCrud.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Index()
        {
            HomeModel home = new HomeModel();
            home.setNome("Gustavo");
            home.setEmail("gust.offic@gmail.com");
            return View(home);
        }
       
    }
}