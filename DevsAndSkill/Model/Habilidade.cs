using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsAndSkill.Model
{
    [Table("HABILIDADE")]
    public partial class Habilidade
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NOME")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Column("DEV_ID")]
        [System.Text.Json.Serialization.JsonIgnore]
        public int? DevId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey(nameof(DevId))]
        [InverseProperty(nameof(Desenvolvedor.Habilidades))]
        public virtual Desenvolvedor Dev { get; set; }
    }
}
