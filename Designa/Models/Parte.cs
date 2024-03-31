using System.Text.RegularExpressions;

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

    }
}
