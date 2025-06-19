namespace BLL.DTO.Responces
{
    public class AuthResponseDTO//сместить в api responce
    {
        public bool IsAuthenticated { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
