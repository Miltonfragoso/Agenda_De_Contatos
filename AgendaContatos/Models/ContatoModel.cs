using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digite o nome docontato")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Digite o e-mail docontato")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido")]
        public string Email { get; set; }
         


        [Required(ErrorMessage = "Digite o Celular docontato")]
        [Phone(ErrorMessage ="O celular informado não é valido")]
        public string Celular { get; set; }

        public int? UsuarioId { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}
