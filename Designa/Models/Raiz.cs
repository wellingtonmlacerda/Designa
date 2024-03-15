using Newtonsoft.Json;
using SautinSoft;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
namespace Designa.Models
{
    [JsonObject("root")]
    public class Raiz
    {
        public Raiz() 
        {
        }
        [JsonProperty("pubName")]
        public string PubName { get; set; } = string.Empty;

        [JsonProperty("parentPubName")]
        public string ParentPubName { get; set; } = string.Empty;

        [JsonProperty("booknum")]
        public int? Booknum { get; set; }

        [JsonProperty("pub")]
        public string Pub { get; set; } = string.Empty;

        [JsonProperty("issue")]
        public string Issue { get; set; } = string.Empty;

        [JsonProperty("formattedDate")]
        public string FormattedDate { get; set; } = string.Empty;

        [JsonProperty("fileformat")]
        public List<string>? Fileformat { get; set; }

        [JsonProperty("track")]
        public int? Track { get; set; }

        [JsonProperty("specialty")]
        public string Specialty { get; set; } = string.Empty;

        [JsonProperty("pubImage")]
        public PubImage PubImage { get; set; } = new PubImage();

        [JsonProperty("languages")]
        public Languages Languages { get; set; } = new Languages();

        [JsonProperty("files")]
        public Files Files { get; set; } = new Files();

        /// <summary>
        /// Retorna uma publica��o Nossa Vida e Minist�rio Crist�o.
        /// Use 0 para o per�odo atual, 1 para o pr�ximo, 2 para o seguinte e etc.
        /// </summary>
        /// <param name="periodoPub">Per�odo da publica��o</param>
        /// <returns>Retorna uma publica��o Nossa Vida e Minist�rio Crist�o</returns>
        public async Task<Raiz> GetAsyncRoot(int periodoPub = 0)
        {
            string issui = RetonaPubEmissao(periodoPub);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://b.jw-cdn.org/apis/pub-media/GETPUBMEDIALINKS?pub=mwb&langwritten=T&txtCMSLang=T&issue={issui}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<Raiz>(stringResponse);

            return (objeto ?? new Raiz());
        }
        /// <summary>
        /// Retorna uma publica��o Nossa Vida e Minist�rio Crist�o.
        /// </summary>
        /// <param name="periodoPub">Per�odo da publica��o</param>
        /// <returns>Retorna uma publica��o Nossa Vida e Minist�rio Crist�o</returns>
        public async Task<Raiz> GetAsyncRoot(string periodoPub)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://b.jw-cdn.org/apis/pub-media/GETPUBMEDIALINKS?pub=mwb&langwritten=T&txtCMSLang=T&issue={periodoPub}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<Raiz>(stringResponse);

            return (objeto ?? new Raiz());
        }
        /// <summary>
        /// Retorna o per�odo da Nossa Vida e Minist�rio Crist�o no formato para a requisi��o.
        /// Use 0 para o per�odo atual, 1 para o pr�ximo, 2 para o seguinte e etc.
        /// </summary>
        /// <param name="pegarPeriodo">Per�odo da publica��o</param>
        /// <returns>Retorna o per�odo de emiss�o da publica��o</returns>
        public string RetonaPubEmissao(int pegarPeriodo = 0)
        {
            // Obt�m a data atual
            // Obt�m a data atual
            DateTime dataAtual = DateTime.Now;

            // Calcula o n�mero de meses a ser adicionado com base no valor fornecido
            int mesesParaAdicionar = Math.Abs(pegarPeriodo) * 2;

            // Define o sinal de adi��o ou subtra��o com base no valor
            int sinal = Math.Sign(pegarPeriodo);

            // Calcula a data do per�odo
            DateTime dataPeriodo = dataAtual.AddMonths(sinal * mesesParaAdicionar);

            // Formata o resultado no formato "ano+mes"
            string resultado = $"{dataPeriodo.Year}{dataPeriodo.Month:D2}";

            return resultado;
        }
        public async Task<string> GetArquivo(string url)
        {
            // Create an HttpClientHandler object and set to use default credentials
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All,
            });
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode) 
                {
                    var dados = await response.Content.ReadAsStringAsync();

                    // Converte os bytes em uma string usando a codifica��o correta
                    return dados;
                }
                else
                {
                    throw new Exception($"Erro ao obter o conte�do RTF da API. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                handler.Dispose();
                client.Dispose();
                throw new Exception("Erro ao obter o conte�do RTF da API.", ex);
            }
        }

        public string CorrigirCaracteresEspeciaisRTF(string stringRTF)
        {
            // Substituir padr�es de caracteres especiais
            Dictionary<string, string> correcoes = new Dictionary<string, string>
            {
                { @"\u225?a0", " " },   // Espa�os indesejados
                { @"\u160?", " " },   // Espa�os indesejados
                { @"\u8216?", "'" },     // Aspas diretas
                { @"\u8212?", "�" },    // Tra�o longo
                { @"\u8220?", "�" },     // Aspas esquerdas
                { @"\u8221?", "�" },     // Aspas direitas
                { @"\u2022", "�" },     // Marcador de lista
                { @"\u225?", "�" },     // Letra 'a' com acento agudo
                { @"\u233?", "�" },     // Letra 'e' com acento agudo
                { @"\u201?", "�" },     // Letra 'E' com acento agudo
                { @"\u237?", "�" },     // Letra 'i' com acento agudo
                { @"\u243?", "�" },     // Letra 'o' com acento agudo
                { @"\u250?", "�" },     // Letra 'u' com acento agudo
                { @"\u227?", "�" },     // Letra 'a' com til
                { @"\u224?", "�" },     // Letra 'a' com til
                { @"\u245?", "�" },     // Letra 'o' com til
                { @"\u226?", "�" },     // Letra 'a' com acento circunflexo
                { @"\u234?", "�" },     // Letra 'e' com acento circunflexo
                { @"\u00f4", "�" },     // Letra 'o' com acento circunflexo
                { @"\u231?", "�" },     // Letra 'o' com acento circunflexo
            };

            foreach (var correcao in correcoes)
            {
                stringRTF = stringRTF.Replace(correcao.Key, correcao.Value);
            }

            // Remover c�digos RTF
            //texto = Regex.Replace(texto, @"\\[a-z0-9]{1, 32}", "");
            string padraoRtf = @"\\[a-z]+\d*|\\[{}\s]|[\r\n]";

            // Substituir o c�digo RTF por uma string vazia
            stringRTF = Regex.Replace(stringRTF, padraoRtf, "");

            return stringRTF.Replace("{", "").Replace("}", "");
        }

        /// <summary>
        /// Converts RTF file to HTML string.
        /// </summary>
        public string ConvertRtfToHtml(string fileRTF)
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