namespace Therapy.Domain.DTOs.Account
{
    public class AccessToken
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
