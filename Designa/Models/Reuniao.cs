using System.Text.RegularExpressions;

namespace Designa.Models
{
    public class Reuniao
    {
        public Reuniao(string stringRTF)
        {
            _stringRTF = stringRTF;
            ExtrairPartesEnumeradas();
        }
        private string _stringRTF;
        public string Semana { get; set; } = "";
        public List<Parte> Partes { get; set; } = new List<Parte>();
        private void ExtrairPartesEnumeradas()
        {
            string padrao = string.Format(@"{0}|{1}"
                                            , @"(\d+)\s*\.\s([\p{L}\p{Pd}\p{Zs}—]+)\?\s*\((\d+)\s*min\)"
                                            , @"(\d+)\s*\.\s*([\p{L}\p{Pd}\p{Zs}—]+)\s*\(\s*(\d+)\s*min\s*\)"
                                         );

            Regex regex = new Regex(padrao);
            MatchCollection matches = regex.Matches(_stringRTF);

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
                this.Partes.Add(parte);
            }
        }
    }
}
