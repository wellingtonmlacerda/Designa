using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Designa.Helpers
{
    public class Enums
    {
        public enum EnumStatusPublicador
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
        public enum EnumPrivilegio
        {
            [Display(Name ="Publicador")]
            Publicador,
            [Display(Name = "Pioneiro Auxiliar")]
            PioneiroAuxiliar,
            [Display(Name = "Pioneiro")]
            Pioneiro,
            [Display(Name = "Pioneiro Especial")]
            PioneiroEspecial,
            [Display(Name = "Ancião")]
            Anciao,
            [Display(Name = "Servo Ministerial")]
            ServoMinisterial
        }
    }
}
