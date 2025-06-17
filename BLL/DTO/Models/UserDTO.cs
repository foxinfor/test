namespace BLL.DTO.Models
{
    public class UserDTO
    {
        public string Id { get; set; }


        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriversLicense { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public int LoyaltyPoints { get; set; }
        public List<string> PaymentMethods { get; set; }


        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
