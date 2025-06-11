namespace DAL.Models
{
    public class Car
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public string Vin { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public int Seats { get; set; }
        public string Color { get; set; }
        public int DailyRate { get; set; }
        public int HourlyRate { get; set; }
        public bool IsAvailable { get; set; }
        public string Location { get; set; }
        public List<string> Features { get; set; }
        public List<string> Images { get; set; }
    }
}
