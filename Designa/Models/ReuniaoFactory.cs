namespace Designa.Models
{
    public class ReuniaoFactory:IReuniaoFactory
    {
        public Reuniao CriaReuniao(string stringRTF, string semana, string issui)
        {
            return new Reuniao().Inicializa(stringRTF, semana, issui);
        }
    }
}
