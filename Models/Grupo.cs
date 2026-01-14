using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoLab.Models
{
    public class Grupo
    {
        [Key]
        public int GrupoId { get; set; }
        [Required]
        public string NomeGrupo { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string TipoAcesso { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public string? NomeDoCriadorGrupo { get; set; }

        public bool eMembro { get; set; }

    }
}
