namespace TrabalhoLab.Models
{
    public class Denuncia
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Motivo { get; set; }
        public string Status { get; set; } // Pode ser um enum (Pendente, Aprovada, Rejeitada)

    }
}
