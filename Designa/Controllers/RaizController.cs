using Microsoft.AspNetCore.Mvc;
using Designa.Models;
using Newtonsoft.Json;

namespace Designa.Controllers
{
    public class RaizController : Controller
    {
        public Raiz _raiz;
        public RaizController() 
        {
            _raiz = new Raiz();
        }
        public async Task<IActionResult> Index()
        {
            _raiz = await GetRoot();
            return View(_raiz);
        }

        public async Task<Raiz> GetRoot()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://b.jw-cdn.org/apis/pub-media/GETPUBMEDIALINKS?pub=mwb&langwritten=T&txtCMSLang=T&issue=202309");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<Raiz>(stringResponse);

            return (objeto ?? new Raiz());
        }
    }
}
