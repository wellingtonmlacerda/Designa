using Designa.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Designa.Helpers.Enums;

namespace Designa.Models
{
    public class PublicadorDTO
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        public EnumStatusPublicador Status { get; set; } = EnumStatusPublicador.Ativo;
        [Display(Name = "Observação")]
        [Column(TypeName = "varchar(8000)")]
        public string? Observacao { get; set; } = string.Empty;
        [Column(TypeName = "char(1)")]
        public string Sexo { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Celular { get; set; } = string.Empty;
        [Display(Name = "Celular Valido?")]
        public bool isCelularValido { get; set; }
        public string SexoDesc { get { return Sexo == "M" ? "Masculino" : "Feminino"; } }
        [Required, Display(Name = "É Menor?")]
        public EnumBoleano EMenorIdade { get; set; } = EnumBoleano.Não;
        [Display(Name = "Pai")]
        public int? PaiId { get; set; }
        [Display(Name = "Mãe")]
        public int? MaeId { get; set; }
        [ForeignKey("PaiId")]
        public virtual PublicadorDTO? Pai { get; set; }
        [ForeignKey("MaeId")]
        public virtual PublicadorDTO? Mae { get; set; }
        [Display(Name = "Privilégios")]
        [InverseProperty("Publicador")]
         public virtual ICollection<PublicadorPrivilegioDTO> PublicadorPrivilegios { get; set; } = new List<PublicadorPrivilegioDTO>();
        [NotMapped]
        public virtual List<EnumPrivilegio> Privilegios { get; set;} = new List<EnumPrivilegio>();
        [InverseProperty("Publicador")]
        public virtual ICollection<ListaNegraDTO> listaNegraPublicadores { get; set; } = new List<ListaNegraDTO>();
        [InverseProperty("Publicador")]
        public virtual ICollection<PublicadorParteDTO> PartesPublicador { get; set; } = new HashSet<PublicadorParteDTO>();
        [InverseProperty("PublicadorAjudante")]
        public virtual ICollection<PublicadorParteDTO> PartesPublicadorAjudante { get; set; } = new HashSet<PublicadorParteDTO>();
    }
}
