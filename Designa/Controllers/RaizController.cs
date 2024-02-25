using Designa.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.rtf.document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SautinSoft;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Designa.Controllers
{
    public class RaizController : Controller
    {
        public Raiz _raiz;
        public Parte _parte;
        public RaizController()
        {
            _raiz = new Raiz();
            _parte = new Parte();
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                _raiz = await GetRoot("202405");
                if (_raiz?.Files?.T?.RTF?.FirstOrDefault(x => x.Track == 1) is RTF rtf)
                {
                    var url = rtf.File.Url;
                    var stringRTF = await _raiz.GetArquivo(url);

                    // Carrega o texto RTF
                    string textoCorrigido = CorrigirCaracteresEspeciais(stringRTF);

                    // Extrair partes enumeradas
                    List<Parte> partesEnum = ExtrairPartesEnumeradas(textoCorrigido);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(_raiz);
        }
        private string CorrigirCaracteresEspeciais(string texto)
        {
            // Substituir padrões de caracteres especiais
            Dictionary<string, string> correcoes = new Dictionary<string, string>
            {
                { @"\u225?a0", " " },   // Espaços indesejados
                { @"\u2019", "'" },     // Aspas diretas
                { @"\u8212?", "—" },    // Traço longo
                { @"\u201c", "“" },     // Aspas esquerdas
                { @"\u201d", "”" },     // Aspas direitas
                { @"\u2022", "•" },     // Marcador de lista
                { @"\u00e1", "á" },     // Letra 'a' com acento agudo
                { @"\u00e9", "é" },     // Letra 'e' com acento agudo
                { @"\u237?", "í" },     // Letra 'i' com acento agudo
                { @"\u243?", "ó" },     // Letra 'o' com acento agudo
                { @"\u00fa", "ú" },     // Letra 'u' com acento agudo
                { @"\u227?", "ã" },     // Letra 'a' com til
                { @"\u00f5", "õ" },     // Letra 'o' com til
                { @"\u00e2", "â" },     // Letra 'a' com acento circunflexo
                { @"\u00ea", "ê" },     // Letra 'e' com acento circunflexo
                { @"\u00f4", "ô" },     // Letra 'o' com acento circunflexo
                { @"\u231?", "ç" },     // Letra 'o' com acento circunflexo
            };

            foreach (var correcao in correcoes)
            {
                texto = texto.Replace(correcao.Key, correcao.Value);
            }

            // Remover códigos RTF
            //texto = Regex.Replace(texto, @"\\[a-z0-9]{1, 32}", "");
            string padraoRtf = @"\\[a-z]+\d*|\\[{}\s]|[\r\n]";

            // Substituir o código RTF por uma string vazia
            texto = Regex.Replace(texto, padraoRtf, "");//.Replace("{", "").Replace("}", "");

            return texto;
        }

        private List<Parte> ExtrairPartesEnumeradas(string texto)
        {
            List<Parte> partesEnum = new List<Parte>();

            // Regex para encontrar padrões como "1. Texto (min)"
            string padrao = @"(\d+)\.?\.\s(.*?)(?=\s\((\d+)\smin\))";            
            MatchCollection matches = Regex.Matches(texto, padrao);

            foreach (Match match in matches)
            {
                Parte parte = new Parte
                {
                    Numero = match.Groups[1].Value,
                    Titulo = match.Groups[2].Value,
                    Minutos = match.Groups[3].Value
                };
                partesEnum.Add(parte);
            }

            return partesEnum;
        }


    public async Task<Raiz> GetRoot(string issui)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://b.jw-cdn.org/apis/pub-media/GETPUBMEDIALINKS?pub=mwb&langwritten=T&txtCMSLang=T&issue={issui}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<Raiz>(stringResponse);

            return (objeto ?? new Raiz());
        }

        /// <summary>
        /// Converts RTF file to HTML string.
        /// </summary>
        static string ConvertRtfToHtml(string fileRTF)
        {
            string htmlString = String.Empty;

            RtfToHtml r = new RtfToHtml();

            using (var inpMs = new MemoryStream(Encoding.UTF8.GetBytes(fileRTF)))
            {
                using (MemoryStream resultStream = new MemoryStream())
                {
                    r.Convert(inpMs, resultStream, new RtfToHtml.HtmlFixedSaveOptions() { Title = "SautinSoft Example." });
                    htmlString = Encoding.UTF8.GetString(resultStream.ToArray());
                }
            }
            return htmlString;
        }
    }
}
