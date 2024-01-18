namespace AgileTask.Models
{
    public class ResponseResult
    {
        public string AccessToken { get; set; }
        public string EncryptedAccessToken { get; set; }
        public int ExpireInSeconds { get; set; }
        public bool ShouldResetPassword { get; set; }
        public object PasswordResetCode { get; set; }
        public int UserId { get; set; }
        public bool RequiresTwoFactorVerification { get; set; }
        public object TwoFactorAuthProviders { get; set; }
        public object TwoFactorRememberClientToken { get; set; }
        public object ReturnUrl { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenExpireInSeconds { get; set; }
    }
}
