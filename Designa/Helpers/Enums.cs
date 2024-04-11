using System.ComponentModel;

namespace Designa.Helpers
{
    public class Enums
    {
        public enum StatusPublicador
        {
            Inativo,
            Ativo,
            Desassociado,
            Adivertido
        }
        public enum EnumBoleano
        {
            Não,
            Sim
        }
        public enum Privilegio
        {
            [Description("Publicador")]
            Publicador,
            [Description("Pioneiro Auxiliar")]
            PioneiroAuxiliar,
            [Description("Pioneiro")]
            Pioneiro,
            [Description("Pioneiro Especial")]
            PioneiroEspecial,
            [Description("Ancião")]
            Anciao,
            [Description("Servo Ministerial")]
            ServoMinisterial
        }
    }
}
