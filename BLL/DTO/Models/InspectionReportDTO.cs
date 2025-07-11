﻿namespace BLL.DTO.Models
{
    public class InspectionReportDTO
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public string InspectorId { get; set; }
        public DateTime ReturnDate { get; set; }
        public double FuelLevel { get; set; }
        public int Mileage { get; set; }
        public string DescriptionDamage { get; set; }
        public string SeverityDamage { get; set; }
        public int RepairCost { get; set; }
        public List<string> Photos { get; set; }
        public int FinalCharge { get; set; }
        public string Notes { get; set; }
        public int Rating { get; set; }

    }
}
