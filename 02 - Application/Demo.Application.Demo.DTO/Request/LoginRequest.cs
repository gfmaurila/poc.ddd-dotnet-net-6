using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Demo.DTO.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O login não pode vazio.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha não pode vazio.")]
        public string Password { get; set; }
    }
}
