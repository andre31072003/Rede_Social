using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoLab.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Nome { get; set; }


        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string Sexo { get; set; }

        public string? FotoPerfil { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCriação { get; set; } = DateTime.Now;
        public DateTime? DataFimSuspensao { get; set; }
    }
}
