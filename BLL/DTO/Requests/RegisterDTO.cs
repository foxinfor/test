using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Requests
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Введите Email.")]
        [EmailAddress(ErrorMessage = "Неверный формат Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите номер телефона.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите имя.")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию.")]
        [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите номер водительского удостоверения.")]
        [StringLength(20, ErrorMessage = "Номер водительского удостоверения не должен превышать 20 символов.")]
        public string DriversLicense { get; set; }

        [Required(ErrorMessage = "Введите дату рождения.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количество бонусных баллов не может быть отрицательным.")]
        public int LoyaltyPoints { get; set; } = 0;

        public List<string> PaymentMethods { get; set; }

        [Required(ErrorMessage = "Введите пароль.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string? ConfirmPassword { get; set; }

    }
}
