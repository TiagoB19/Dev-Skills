using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsAndSkill.Model
{
    [Table("DESENVOLVEDOR")]
    public partial class Desenvolvedor
    {
        public Desenvolvedor()
        {
            Habilidades = new HashSet<Habilidade>();
        }

        [Key]
        [Column("ID")]

        public int Id { get; set; }
        [Required]
        [Column("NOME")]
        [StringLength(200)]
        public string Nome { get; set; }
        [Required]
        [Column("EMAIL")]
        [StringLength(200)]
        public string Email { get; set; }
        [Column("SITE")]
        [StringLength(200)]
        public string Site { get; set; }

        [InverseProperty("Dev")]
        public virtual ICollection<Habilidade> Habilidades { get; set; }
    }
}
