using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Designa.Models
{
    public class PublicadorParte
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public int ParteId { get; set; }
        public int PublicadorId { get; set; }
        public int? PublicadorAjudanteId { get; set; }
        [ForeignKey("ParteId")]
        public virtual Parte? Parte { get; set; }
        [ForeignKey("PublicadorId")]
        public virtual Publicador? Publicador { get; set; }
        [ForeignKey("PublicadorAjudanteId")]
        public virtual Publicador? PublicadorAjudante { get; set; }
    }
}
