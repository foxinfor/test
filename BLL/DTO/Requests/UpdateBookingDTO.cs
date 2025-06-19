using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Requests
{
    public class UpdateBookingDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string CarId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Цена не может быть отрицательной.")]
        public int TotalPrice { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [StringLength(30)]
        public string InsuranceType { get; set; }

        public List<string> Extras { get; set; } = new();

        [StringLength(100)]
        public string PickupLocation { get; set; }

        [StringLength(100)]
        public string DropoffLocation { get; set; }
    }
}
