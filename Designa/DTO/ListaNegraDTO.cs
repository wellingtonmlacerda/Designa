using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Designa.Models
{
    public class ListaNegraDTO
    {
        [Key]
        public int Id { get; set; }
        public int PublicadorId { get; set; }
        public int PublicadorListaNegraId { get; set;}
        public string Observacao { get; set; } = string.Empty;
        [ForeignKey("PublicadorId")]
        public virtual PublicadorDTO Publicador { get; set; } = new();
        [ForeignKey("PublicadorListaNegraId")]
        public virtual PublicadorDTO PublicadorListaNegra { get; set; } = new();
    }
}
