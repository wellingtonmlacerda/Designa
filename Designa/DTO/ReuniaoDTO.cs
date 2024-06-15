using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Designa.Models
{
    public class ReuniaoDTO
    {
        [NotMapped]
        private string _stringRTF = string.Empty;
        public int Id { get; set; }
        public string Issue { get; set; } = string.Empty;
        public string Semana { get; set; } = string.Empty;
        public int? PresidenteId { get; set; }
        public int? PublicadorOracaoId { get; set; }
        [ForeignKey("PresidenteId")]
        public PublicadorDTO? Presidente { get; set; }
        [ForeignKey("PublicadorOracaoId")]
        public PublicadorDTO? PublicadorOracao { get; set; }
        public ICollection<ParteDTO> Partes { get; set; } = new List<ParteDTO>();
        // Lista de publicadores disponíveis
        [NotMapped]
        public List<PublicadorDTO>? Presidentes { get; set; }
        public ReuniaoDTO Inicializa(string stringRTF, string semana, string issui)
        {
            _stringRTF = stringRTF;
            Semana = semana;
            Issue = issui;
            this.ExtrairPartesEnumeradas();
            return this;
        }
        private void ExtrairPartesEnumeradas()
        {
            string padrao = string.Format(@"{0}|{1}|{2}|{3}|{4}"
                                            , @"(\d+)\s*\.\s([\p{L}\p{Pd}\p{Zs}—,‘’'./“”!?]+)\d+\s*\((\d+)\s*min\)"
                                            , @"(\d+)\s*\.\s([\p{L}\p{Pd}\p{Zs}—,‘’'./“”!?]+)\s*\((\d+)\s*min\)"
                                            , @"Tesouros da Palavra de Deus"
                                            , @"Faça seu melhor no ministério"
                                            , @"Nossa vida cristã"
                                         );

            Regex regex = new Regex(padrao);;
            MatchCollection matches = regex.Matches(_stringRTF);

            int index = 0;
            foreach (Match match in matches)
            {
                int numeroTitulo;
                string titulo;
                string minutos;

                // Verifica qual padrão foi correspondido
                if (!match.Groups[1].Success && !match.Groups[4].Success) 
                {
                    numeroTitulo = 0;
                    titulo = match.Groups[0].Value;
                    minutos = "";
                }
                else  if (match.Groups[1].Success)
                {
                    int.TryParse(match.Groups[1].Value, out numeroTitulo);
                    titulo = match.Groups[2].Value;
                    minutos = match.Groups[3].Value;
                }
                else
                {
                    int.TryParse(match.Groups[4].Value, out numeroTitulo);
                    titulo = match.Groups[5].Value;
                    minutos = match.Groups[6].Value;
                }

                ParteDTO parte = new ParteDTO
                {
                    Index = index,
                    Numero = numeroTitulo,
                    Titulo = titulo,
                    Minutos = minutos
                };
                this.Partes.Add(parte);
                index++;
            }
        }
    }
}
