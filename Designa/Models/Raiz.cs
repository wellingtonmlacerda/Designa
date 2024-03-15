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
        /// Retorna uma publicação Nossa Vida e Ministério Cristão.
        /// Use 0 para o período atual, 1 para o próximo, 2 para o seguinte e etc.
        /// </summary>
        /// <param name="periodoPub">Período da publicação</param>
        /// <returns>Retorna uma publicação Nossa Vida e Ministério Cristão</returns>
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
        /// Retorna uma publicação Nossa Vida e Ministério Cristão.
        /// </summary>
        /// <param name="periodoPub">Período da publicação</param>
        /// <returns>Retorna uma publicação Nossa Vida e Ministério Cristão</returns>
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
        /// Retorna o período da Nossa Vida e Ministério Cristão no formato para a requisição.
        /// Use 0 para o período atual, 1 para o próximo, 2 para o seguinte e etc.
        /// </summary>
        /// <param name="pegarPeriodo">Período da publicação</param>
        /// <returns>Retorna o período de emissão da publicação</returns>
        public string RetonaPubEmissao(int pegarPeriodo = 0)
        {
            // Obtém a data atual
            // Obtém a data atual
            DateTime dataAtual = DateTime.Now;

            // Calcula o número de meses a ser adicionado com base no valor fornecido
            int mesesParaAdicionar = Math.Abs(pegarPeriodo) * 2;

            // Define o sinal de adição ou subtração com base no valor
            int sinal = Math.Sign(pegarPeriodo);

            // Calcula a data do período
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

                    // Converte os bytes em uma string usando a codificação correta
                    return dados;
                }
                else
                {
                    throw new Exception($"Erro ao obter o conteúdo RTF da API. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                handler.Dispose();
                client.Dispose();
                throw new Exception("Erro ao obter o conteúdo RTF da API.", ex);
            }
        }

        public string CorrigirCaracteresEspeciaisRTF(string stringRTF)
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
                stringRTF = stringRTF.Replace(correcao.Key, correcao.Value);
            }

            // Remover códigos RTF
            //texto = Regex.Replace(texto, @"\\[a-z0-9]{1, 32}", "");
            string padraoRtf = @"\\[a-z]+\d*|\\[{}\s]|[\r\n]";

            // Substituir o código RTF por uma string vazia
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