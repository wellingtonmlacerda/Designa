using Newtonsoft.Json;
using SautinSoft;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
namespace Designa.Models
{
    [JsonObject("root")]
    public class Publicacao : IPublicacao
    {
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
        public string PeriodoReferencia
        {
            get
            {
                return ObterMesesPorExtenso(this.Issue);
            }
        }

        /// <summary>
        /// Retorna uma publica��o Nossa Vida e Minist�rio Crist�o.
        /// Use 0 para o per�odo atual, 1 para o pr�ximo, 2 para o seguinte e etc.
        /// </summary>
        /// <param name="periodoPub">Per�odo da publica��o</param>
        /// <returns>Retorna uma publica��o Nossa Vida e Minist�rio Crist�o</returns>
        public async Task<Publicacao> GetAsyncRoot(int periodoPub = 0)
        {
            var issui = RetonaPubEmissao(periodoPub);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://b.jw-cdn.org/apis/pub-media/GETPUBMEDIALINKS?pub=mwb&langwritten=T&txtCMSLang=T&issue={issui}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<Publicacao>(stringResponse);

            return (objeto ?? new Publicacao());
        }
        /// <summary>
        /// Retorna o per�odo da Nossa Vida e Minist�rio Crist�o no formato para a requisi��o.
        /// Use 0 para o per�odo atual, 1 para o pr�ximo, 2 para o seguinte e etc.
        /// </summary>
        /// <param name="pegarPeriodo">Per�odo da publica��o</param>
        /// <returns>Retorna o per�odo de emiss�o da publica��o</returns>
        public string RetonaPubEmissao(int pegarPeriodo = 0)
        {
            if(pegarPeriodo > 100) //Significa que j� est� no formato correto e n�o precisa pegar o per�odo
                return pegarPeriodo.ToString();

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
            var resultado = $"{dataPeriodo.Year}{dataPeriodo.Month:D2}";

            return resultado;
        }
        public string ObterMesesPorExtenso(string anoMes)
        {
            if (anoMes.Length != 6)
            {
                throw new ArgumentException("O formato do ano e m�s deve ser 'yyyyMM'");
            }

            string ano = anoMes.Substring(0, 4);
            string mes = anoMes.Substring(4, 2);

            string[] nomesMeses = {
            "Janeiro", "Fevereiro", "Mar�o", "Abril",
            "Maio", "Junho", "Julho", "Agosto",
            "Setembro", "Outubro", "Novembro", "Dezembro"
            };

            int mesInt = int.Parse(mes);
            string mesAtual = nomesMeses[mesInt - 1];
            string proximoMes = nomesMeses[(mesInt % 12)];
            if (mesInt == 12)
            {
                proximoMes = nomesMeses[0];
            }

            return $"{mesAtual}-{proximoMes} de {ano}";
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
                { @"\u8216?", "�" },     // Aspas diretas esquerda
                { @"\u8217?", "�" },     // Aspas diretas direita
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
        public List<RTF> RetornaListaRTF()
        {
            if (Files.T.RTF is List<RTF> listaRTF)
                return listaRTF;

            return new List<RTF>();
        }
        public Publicacao ThisPublicacao()
        {
            return this;
        }
    }
}