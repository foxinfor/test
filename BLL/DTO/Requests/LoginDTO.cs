using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Requests
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Введите Email.")]
        [EmailAddress(ErrorMessage = "Неверный формат Email адреса.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Введите пароль.")]
        public string? Password { get; set; }
    }
}
