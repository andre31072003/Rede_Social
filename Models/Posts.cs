using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoLab.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }

        public string? NomeCriadorPost { get; set; }
        public string? Texto { get; set; }
        public string? FotoPublicacao { get; set; }
        [Required]
        public string TipoPost { get; set; }

        public DateTime DataPost { get; set; } = DateTime.Now;
        public int? GrupoId { get; set; }

        //public virtual ICollection<Comenta> Comentas { get; set; }

       
    }
}

