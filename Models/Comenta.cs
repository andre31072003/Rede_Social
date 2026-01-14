using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoLab.Models
{
    public class Comenta
    {

        [Key]
        public int IdComentário { get; set; }

        public string? NomeAutorComentário { get; set; }
        public string Comentário { get; set; }

        public DateTime DataComentario { get; set; } = DateTime.Now;

        public int IdPost { get; set; }

    }
}
