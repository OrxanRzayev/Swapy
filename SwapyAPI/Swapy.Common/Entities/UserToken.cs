namespace Swapy.Common.Entities
{
    public class UserToken
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public UserToken() { }

        public UserToken(string accessToken, string refreshToken, DateTime expiresAt, string userId)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresAt = expiresAt;
            UserId = userId;
        }
    }
}
