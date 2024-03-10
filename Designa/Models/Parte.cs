namespace Designa.Models
{
    public class Reuniao
    {
        public string Semana { get; set; }
        public List<Parte> Partes { get; set; } = new List<Parte>();
    }
    public class Parte
    {
        public string Numero { get; set; }
        public string Titulo { get; set; }
        public string Minutos { get; set; }
        public string Texto { get; set; }
    }
}
