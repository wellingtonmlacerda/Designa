using Aspose.Words;
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
            //var jsonObjetc = rtfToJson(stringResponse);
            var objeto = JsonConvert.DeserializeObject<Publicacao>(stringResponse);

            return (objeto ?? new Publicacao());
        }

        public string rtfToJson(string rtf)
        {

            // Converter a string RTF em um Stream
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(rtf);
                writer.Flush();
                stream.Position = 0;

                // Carregar o documento a partir do stream
                Document doc = new Document(stream);

                // Extrair texto completo
                string fullText = doc.GetText();

                // Fun��o para limpar o texto de partes irrelevantes
                string CleanText(string text)
                {
                    // Exemplo simples de remo��o de URLs e outras partes irrelevantes
                    text = Regex.Replace(text, @"https?:\/\/\S+", ""); // Remove URLs
                    text = Regex.Replace(text, @"Created with an evaluation copy.*", "", RegexOptions.Singleline); // Remove mensagens da avalia��o Aspose
                    text = Regex.Replace(text, @"\{[^}]+\}", ""); // Remove conte�do entre chaves

                    // Outras limpezas espec�ficas que forem necess�rias
                    // Por exemplo: remover cabe�alhos, rodap�s, etc.

                    return text.Trim();
                }

                // Limpar o texto
                fullText = CleanText(fullText);

                // Padr�es regex para identificar se��es e partes
                string secaoPattern = @"^[A-Za-z\s]+$";
                string partePattern = @"^(\d+)\.\s+(.*?)(\((\d+)\s+min\))?$";

                List<object> secoes = new List<object>();
                string[] lines = fullText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                int secaoId = 1;
                string currentSecao = null;
                List<object> partes = new List<object>();

                foreach (var line in lines)
                {
                    // Verifica se a linha � uma se��o
                    if (Regex.IsMatch(line.Trim(), secaoPattern))
                    {
                        // Salva a se��o anterior
                        if (currentSecao != null)
                        {
                            secoes.Add(new
                            {
                                id = secaoId++,
                                secao = currentSecao,
                                partes = partes
                            });
                        }

                        // Inicia uma nova se��o
                        currentSecao = line.Trim();
                        partes = new List<object>();
                    }
                    else if (Regex.IsMatch(line.Trim(), partePattern))
                    {
                        // Extrai informa��es da parte
                        var match = Regex.Match(line.Trim(), partePattern);
                        int numero = int.Parse(match.Groups[1].Value);
                        string titulo = match.Groups[2].Value.Trim();
                        int tempo = match.Groups[4].Success ? int.Parse(match.Groups[4].Value) : 0;
                        string texto = ""; // Placeholder para o texto associado

                        // Busca o texto associado � parte
                        if (line.Contains("min"))
                        {
                            int index = Array.IndexOf(lines, line) + 1;
                            if (index < lines.Length && !Regex.IsMatch(lines[index], secaoPattern))
                            {
                                texto = lines[index].Trim();
                            }
                        }

                        partes.Add(new
                        {
                            numero = numero,
                            titulo = titulo,
                            texto = texto,
                            tempo = tempo
                        });
                    }
                }

                // Adiciona a �ltima se��o
                if (currentSecao != null)
                {
                    secoes.Add(new
                    {
                        id = secaoId++,
                        secao = currentSecao,
                        partes = partes
                    });
                }

                // Serializar para JSON
                string jsonString = JsonConvert.SerializeObject(secoes, Formatting.Indented);
                return jsonString;
            }
        }

        /// <summary>
        /// Retorna o per�odo da Nossa Vida e Minist�rio Crist�o no formato para a requisi��o.
        /// Use 0 para o per�odo atual, 1 para o pr�ximo, 2 para o seguinte e etc.
        /// </summary>
        /// <param name="pegarPeriodo">Per�odo da publica��o</param>
        /// <returns>Retorna o per�odo de emiss�o da publica��o</returns>
        public string RetonaPubEmissao(int pegarPeriodo = 0)
        {
            if (pegarPeriodo > 100) // Significa que j� est� no formato correto e n�o precisa pegar o per�odo
                return pegarPeriodo.ToString();

            // Obt�m a data atual
            DateTime dataAtual = DateTime.Now;

            // Calcula o n�mero de meses a ser adicionado com base no valor fornecido
            int mesesParaAdicionar = Math.Abs(pegarPeriodo) * 2;

            // Define o sinal de adi��o ou subtra��o com base no valor
            int sinal = Math.Sign(pegarPeriodo);

            // Calcula a data do per�odo
            DateTime dataPeriodo = dataAtual.AddMonths(sinal * mesesParaAdicionar);

            // Corrigir o m�s para os valores espec�ficos
            int[] mesesValidos = { 1, 3, 5, 7, 9, 11 };
            if (!mesesValidos.Contains(dataPeriodo.Month))
            {
                // Procura o pr�ximo m�s v�lido
                int proximoMesValido = mesesValidos.FirstOrDefault(m => m > dataPeriodo.Month);
                if (proximoMesValido == 0) // Se n�o encontrou, o pr�ximo ser� o primeiro do pr�ximo ano
                {
                    proximoMesValido = mesesValidos.First();
                    dataPeriodo = new DateTime(dataPeriodo.Year + 1, proximoMesValido, 1);
                }
                else
                {
                    dataPeriodo = new DateTime(dataPeriodo.Year, proximoMesValido, 1);
                }
            }

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