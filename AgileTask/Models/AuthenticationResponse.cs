namespace AgileTask.Models
{
    public class AuthenticationResponse
    {
        public ResponseResult Result { get; set; }
        public string TargetUrl { get; set; }
        public bool Success { get; set; }
        public object Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }
}
