using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Designa.Helpers.Enums;

namespace Designa.Models
{
    public class PublicadorPrivilegioDTO
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Privilégio")]
        public EnumPrivilegio Privilegio { get; set; } = EnumPrivilegio.Publicador;
        [Display(Name ="Publicador")]
        public int PublicadorId { get; set; }
        [ForeignKey("PublicadorId")]
        public PublicadorDTO? Publicador { get; set; }
    }
}
