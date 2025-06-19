using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Requests
{
    public class CreateCarDTO
    {
        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9-]{1,10}$", ErrorMessage = "Неверный формат номера.")]
        public string LicensePlate { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 17)]
        public string Vin { get; set; }

        [Required]
        [StringLength(20)]
        public string FuelType { get; set; }

        [Required]
        [StringLength(20)]
        public string Transmission { get; set; }

        [Range(1, 9)]
        public int Seats { get; set; }

        [Required]
        [StringLength(30)]
        public string Color { get; set; }

        [Range(0, 100000)]
        public int DailyRate { get; set; }

        [Range(0, 10000)]
        public int HourlyRate { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        public List<string> Features { get; set; } = new();

        public List<string> Images { get; set; } = new();
    }
}
