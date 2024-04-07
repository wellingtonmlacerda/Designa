using Designa.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Designa.Models
{
    public class Irmao
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte Status { get; set; } = (byte)Enums.StatusIrmao.Ativo;
        public string Observacao { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public byte EMenorIdade { get; set; } = 0;
        public int PaiId { get; set; }
        public int MaeId { get; set; }
        [ForeignKey("PaiId")]
        public Irmao Pai { get; set; } = new ();
        [ForeignKey("MaeId")]
        public Irmao Mae { get; set; } = new ();
        [InverseProperty("Irmao")]
        public ICollection<ListaNegra> listaNegraIrmaos { get; set; } = new List<ListaNegra>();
        [InverseProperty("Publicador")]
        public ICollection<IrmaoParte> PartesPublicador { get; set; } = new HashSet<IrmaoParte>();
        [InverseProperty("Ajudante")]
        public ICollection<IrmaoParte> PartesAjudante { get; set; } = new HashSet<IrmaoParte>();
    }
}
