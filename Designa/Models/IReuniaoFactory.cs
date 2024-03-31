namespace Designa.Models
{
    public interface IReuniaoFactory
    {
        public Reuniao CriaReuniao(string stringRTF, string semana, string issui);
    }
}
