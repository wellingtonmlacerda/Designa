using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Designa.Helpers.Enums;

namespace Designa.Models
{
    public class PublicadorPrivilegio
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Privilégio")]
        public EnumPrivilegio Privilegio { get; set; } = EnumPrivilegio.Publicador;
        [Display(Name ="Publicador")]
        public int PublicadorId { get; set; }
        [ForeignKey("PublicadorId")]
        public Publicador? Publicador { get; set; }
    }
}
