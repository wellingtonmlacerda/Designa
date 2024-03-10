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
                var _reuniao = new List<Reuniao>();
                foreach (var rtf in _raiz?.Files?.T?.RTF?.Where(x => x.Mimetype.Equals("application/rtf")))
                {
                    var url = rtf.File.Url;
                    var stringRTF = await _raiz.GetArquivo(url);

                    // Carrega o texto RTF
                    string textoCorrigido = CorrigirCaracteresEspeciais(stringRTF);
                    _reuniao.Add(new Reuniao()
                    {
                        Semana = rtf.Title,
                        Partes = ExtrairPartesEnumeradas(textoCorrigido)
                    });
                    
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
                { @"\u160?", " " },   // Espaços indesejados
                { @"\u8216?", "'" },     // Aspas diretas
                { @"\u8212?", "—" },    // Traço longo
                { @"\u8220?", "“" },     // Aspas esquerdas
                { @"\u8221?", "”" },     // Aspas direitas
                { @"\u2022", "•" },     // Marcador de lista
                { @"\u225?", "á" },     // Letra 'a' com acento agudo
                { @"\u233?", "é" },     // Letra 'e' com acento agudo
                { @"\u201?", "É" },     // Letra 'E' com acento agudo
                { @"\u237?", "í" },     // Letra 'i' com acento agudo
                { @"\u243?", "ó" },     // Letra 'o' com acento agudo
                { @"\u250?", "ú" },     // Letra 'u' com acento agudo
                { @"\u227?", "ã" },     // Letra 'a' com til
                { @"\u224?", "à" },     // Letra 'a' com til
                { @"\u245?", "õ" },     // Letra 'o' com til
                { @"\u226?", "â" },     // Letra 'a' com acento circunflexo
                { @"\u234?", "ê" },     // Letra 'e' com acento circunflexo
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
            texto = Regex.Replace(texto, padraoRtf, "");

            return texto.Replace("{", "").Replace("}", "");
        }

        private List<Parte> ExtrairPartesEnumeradas(string texto)
        {
            List<Parte> partesEnum = new List<Parte>();
            
            //string padrao = @"\{\s*(\d+)\.\s([\p{L}\p{Pd}\p{Zs}—]+) \((\d+)\smin\)\s*\}|\{\s*(\d+)\.\s*([\p{L}\p{Pd}\p{Zs}—]+)\s*\}\s*\{\s*\(\s*(\d+)\s*min\s*\)\s*\}";
            string padrao = string.Format(@"{0}|{1}"
                                            , @"(\d+)\s*\.\s([\p{L}\p{Pd}\p{Zs}—]+)\?\s*\((\d+)\s*min\)"
                                            , @"(\d+)\s*\.\s*([\p{L}\p{Pd}\p{Zs}—]+)\s*\(\s*(\d+)\s*min\s*\)"
                                         );
            
            Regex regex = new Regex(padrao);
            MatchCollection matches = regex.Matches(texto);

            foreach (Match match in matches)
            {
                string numeroTitulo;
                string titulo;
                string minutos;

                // Verifica qual padrão foi correspondido
                if (match.Groups[1].Success)
                {
                    numeroTitulo = match.Groups[1].Value;
                    titulo = match.Groups[2].Value;
                    minutos = match.Groups[3].Value;
                }
                else
                {
                    numeroTitulo = match.Groups[4].Value;
                    titulo = match.Groups[5].Value;
                    minutos = match.Groups[6].Value;
                }

                Parte parte = new Parte
                {
                    Numero = numeroTitulo,
                    Titulo = titulo,
                    Minutos = minutos
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
