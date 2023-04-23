using Metika.Identity.Model;

namespace Metika.Identity.Services
{
    public class UserService : IUserService
    {
        public async Task<string> Register(RegisterRequestDto request)
        {
            return "";
        }
        public async Task<string> LoginWithPassword(LoginWithPasswordRequestDto request)
        {
            return "";
        }
        public async Task<string> LoginWithVerification(LoginWithVerificationRequestDto request)
        {
            return "";
        }
        public async Task<bool> SendSmsVerificationCode(SmsVerificationRequestDto request)
        {
            return false;
        }
        public async Task<bool> SendEmailVerificationCode(EmailVerificationRequestDto request)
        {
            return false;
        }
    }
}
