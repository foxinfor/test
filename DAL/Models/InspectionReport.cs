namespace DAL.Models
{
    public class InspectionReport
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public string InspectorId { get; set; }
        public DateTime ReturnDate { get; set; }
        public double FuelLevel { get; set; }
        public int Mileage { get; set; }
        public List<Damage> Damages { get; set; }
        public List<string> Photos { get; set; }
        public int FinalCharge { get; set; }
        public string Notes { get; set; }
    }
}
