using Newtonsoft.Json;
using SautinSoft;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;

namespace Designa.Models
{
    public interface IPublicacao
    {
        public Task<Publicacao> GetAsyncRoot(int periodoPub = 0);
        public string RetonaPubEmissao(int pegarPeriodo = 0);
        public string ObterMesesPorExtenso(string anoMes);
        public Task<string> GetArquivo(string url);
        public string CorrigirCaracteresEspeciaisRTF(string stringRTF);
        public string ConvertRtfToHtml(string fileRTF);
        public List<RTF> RetornaListaRTF();
        public Publicacao ThisPublicacao();
    }
}
