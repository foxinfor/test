using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Requests
{
    public class CreateBookingDTO//не до конца
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string CarId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "2020-01-01", "9999-12-31", ErrorMessage = "Дата начала должна быть не раньше сегодняшней.")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Место получения должно содержать минимум 3 символа.")]
        public string PickupLocation { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Место возврата должно содержать минимум 3 символа.")]
        public string DropoffLocation { get; set; }

        public string InsuranceType { get; set; }

        public List<string> Extras { get; set; }
    }
}
