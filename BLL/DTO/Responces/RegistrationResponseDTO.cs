namespace BLL.DTO.Responces
{
    public class RegistrationResponseDTO
    {
        public bool IsRegistered { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
