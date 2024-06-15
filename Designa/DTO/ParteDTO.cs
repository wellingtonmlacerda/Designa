using System.Diagnostics.CodeAnalysis;

namespace Designa.Models
{
    public class ParteDTO
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int Numero { get; set; }
        public string Titulo { get; set; } = "";
        public string Minutos { get; set; } = "";
        public string Texto { get; set; } = "";
        public int ReuniaoId { get; set; }
        public int? PublicadorParteId { get; set; }
        public ReuniaoDTO Reuniao { get; set; } = new ();
        public PublicadorParteDTO? PublicadorParte { get; set; }
    }
}
