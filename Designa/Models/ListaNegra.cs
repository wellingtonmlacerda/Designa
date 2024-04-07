using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Designa.Models
{
    public class ListaNegra
    {
        [Key]
        public int Id { get; set; }
        public int IrmaoId { get; set; }
        public int IrmaoListaNegraId { get; set;}
        public string Observacao { get; set; } = string.Empty;
        [ForeignKey("IrmaoId")]
        public Irmao Irmao { get; set; } = new();
        [ForeignKey("IrmaoListaNegraId")]
        public Irmao IrmaoListaNegra { get; set; } = new();
    }
}
