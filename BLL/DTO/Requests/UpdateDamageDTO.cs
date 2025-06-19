using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Requests
{
    public class UpdateDamageDTO
    {

        [Required]
        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов.")]
        public string Description { get; set; }

        [Required]
        [RegularExpression("(?i)^(Low|Medium|High)$", ErrorMessage = "Допустимые значения: Low, Medium или High.")]
        public string Severity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Стоимость ремонта должна быть неотрицательной.")]
        public int RepairCost { get; set; }
    }
}
