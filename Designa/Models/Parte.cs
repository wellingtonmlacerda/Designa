using System.Diagnostics.CodeAnalysis;

namespace Designa.Models
{
    public class Parte
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int Numero { get; set; }
        public string Titulo { get; set; } = "";
        public string Minutos { get; set; } = "";
        public string Texto { get; set; } = "";
        public int ReuniaoId { get; set; }
        public int? PublicadorParteId { get; set; }
        public Reuniao Reuniao { get; set; } = new ();
        public PublicadorParte? PublicadorParte { get; set; }
    }
}
