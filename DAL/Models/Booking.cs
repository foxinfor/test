namespace DAL.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string InsuranceType { get; set; }
        public List<string> Extras { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
    }
}
