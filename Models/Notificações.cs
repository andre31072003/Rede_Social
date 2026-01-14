using System.ComponentModel.DataAnnotations;

namespace TrabalhoLab.Models
{
    public class Notificações
    {
        [Key] 
        public int NotificacaoId { get; set; }
        public string Destinatario { get; set; }
        public string Mensagem { get; set; }
        public DateTime Data { get; set; }

       
        public string NomeSolicitador { get; set; } 
        public int GrupoId { get; set; } 

    }
}
