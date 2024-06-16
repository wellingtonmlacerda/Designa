using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Designa.Helpers.Enums;

namespace Designa.Models
{
    public class Publicador
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
        public string? Celular { get; set; }
        [Display(Name = "Celular Valido?")]
        public bool isCelularValido { get; set; } = false;
        public string SexoDesc { get { return Sexo == "M" ? "Masculino" : "Feminino"; } }
        [Required, Display(Name = "É Menor?")]
        public EnumBoleano EMenorIdade { get; set; } = EnumBoleano.Não;
        [Display(Name = "Pai")]
        public int? PaiId { get; set; }
        [Display(Name = "Mãe")]
        public int? MaeId { get; set; }
        [ForeignKey("PaiId")]
        public virtual Publicador? Pai { get; set; }
        [ForeignKey("MaeId")]
        public virtual Publicador? Mae { get; set; }
        [Display(Name = "Privilégios")]
        [InverseProperty("Publicador")]
        //[JsonIgnore] //Essa propriedade ajuda a resolver o erro "A possible object cycle was detected."

        public virtual ICollection<PublicadorPrivilegio> PublicadorPrivilegios { get; set; } = new List<PublicadorPrivilegio>();
        [NotMapped]
        public virtual List<EnumPrivilegio> Privilegios { get; set;} = new List<EnumPrivilegio>();
        [InverseProperty("Publicador")]
        public virtual ICollection<ListaNegra> listaNegraPublicadores { get; set; } = new List<ListaNegra>();
        [InverseProperty("Publicador")]
        public virtual ICollection<PublicadorParte> PartesPublicador { get; set; } = new HashSet<PublicadorParte>();
        [InverseProperty("PublicadorAjudante")]
        public virtual ICollection<PublicadorParte> PartesPublicadorAjudante { get; set; } = new HashSet<PublicadorParte>();
    }
}
