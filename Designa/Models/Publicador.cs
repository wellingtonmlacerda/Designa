using Designa.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Designa.Models
{
    public class Publicador
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public byte Status { get; set; } = 1;
        public string? Observacao { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public byte EMenorIdade { get; set; } = 0;
        public int? PaiId { get; set; }
        public int? MaeId { get; set; }
        [ForeignKey("PaiId")]
        public virtual Publicador? Pai { get; set; }
        [ForeignKey("MaeId")]
        public virtual Publicador? Mae { get; set; }
        [InverseProperty("Publicador")]
        public virtual ICollection<ListaNegra> listaNegraPublicadores { get; set; } = new List<ListaNegra>();
        [InverseProperty("Publicador")]
        public virtual ICollection<PublicadorParte> PartesPublicador { get; set; } = new HashSet<PublicadorParte>();
        [InverseProperty("PublicadorAjudante")]
        public virtual ICollection<PublicadorParte> PartesPublicadorAjudante { get; set; } = new HashSet<PublicadorParte>();
    }
}
