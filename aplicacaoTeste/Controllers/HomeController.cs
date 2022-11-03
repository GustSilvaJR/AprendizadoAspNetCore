using aplicacaoTeste.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using Newtonsoft.Json;

namespace aplicacaoTeste.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public async Task<IActionResult> GetFinancas()
        {
            
            HttpClient webRequest = new HttpClient();

            webRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("MASTER:303304")));

            var value = Request.QueryString.Value;
            var url = "http://195.1.1.110:53010/api/armazem/is/aberta"+value;

            HttpResponseMessage response = await webRequest.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            object dados = JsonConvert.DeserializeObject<object>(responseBody); 

            Console.WriteLine(dados);
            Console.WriteLine(dados.GetType());

            return View("Index", dados);
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string consulta="")
        {

            if (consulta != "")
            {
                Console.WriteLine(consulta);
            }
            else
            {
                Console.WriteLine("Sem retorno");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}