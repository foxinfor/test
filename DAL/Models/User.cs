namespace DAL.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriversLicense { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int LoyaltyPoints { get; set; }
        public List<string> PaymentMethods { get; set; }
    }
}
