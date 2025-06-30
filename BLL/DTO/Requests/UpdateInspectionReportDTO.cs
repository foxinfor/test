using BLL.DTO.Models;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Requests
{
    public class UpdateInspectionReportDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(36)]
        public string BookingId { get; set; }

        [Required]
        [StringLength(36)]
        public string InspectorId { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Range(0, 100)]
        public double FuelLevel { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Пробег не может быть отрицательным.")]
        public int Mileage { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов.")]
        public string Description { get; set; }

        [Required]
        [RegularExpression("(?i)^(Minor|Medium|High)$", ErrorMessage = "Допустимые значения: Minor, Medium или High.")]
        public string Severity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Стоимость ремонта должна быть неотрицательной.")]
        public int RepairCost { get; set; }
        public List<string> Photos { get; set; } = new();

        [Range(0, int.MaxValue, ErrorMessage = "Сумма начислений не может быть отрицательной.")]
        public int FinalCharge { get; set; }

        [StringLength(1000, ErrorMessage = "Примечание не должно превышать 1000 символов.")]
        public string Notes { get; set; }

        [Range(0, 10, ErrorMessage = "Рейтинг должен быть от 0 до 10.")]
        public int Rating { get; set; }
    }
}
