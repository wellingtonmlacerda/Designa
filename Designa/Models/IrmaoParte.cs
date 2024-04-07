using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Designa.Models
{
    public class IrmaoParte
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public int ParteId { get; set; }
        public int PublicadorId { get; set; }
        public int? AjudanteId { get; set; }
        [ForeignKey("ParteId")]
        public Parte Parte { get; set; } = new();
        [ForeignKey("PublicadorId")]
        public Irmao Publicador { get; set; } = new();
        [ForeignKey("AjudanteId")]
        public Irmao Ajudante { get; set; } = new();
    }
}
