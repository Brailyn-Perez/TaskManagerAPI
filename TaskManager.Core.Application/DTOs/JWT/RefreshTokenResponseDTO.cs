namespace TaskManager.Core.Application.DTOs.JWT
{
    public class RefreshTokenResponseDTO
    {
        public string UserId { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
