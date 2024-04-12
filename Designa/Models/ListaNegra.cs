using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Designa.Models
{
    public class ListaNegra
    {
        [Key]
        public int Id { get; set; }
        public int PublicadorId { get; set; }
        public int PublicadorListaNegraId { get; set;}
        public string Observacao { get; set; } = string.Empty;
        [ForeignKey("PublicadorId")]
        public virtual Publicador Publicador { get; set; } = new();
        [ForeignKey("PublicadorListaNegraId")]
        public virtual Publicador PublicadorListaNegra { get; set; } = new();
    }
}
