using Metika.Identity.Entities;
using Metika.Identity.Model;

namespace Metika.Identity.Services
{
    public interface IUserService<TUser, TRole> where TUser : User where TRole : Role
    {
        Task<string> LoginWithPassword(LoginWithPasswordRequestDto request);
        Task<string> LoginWithVerification(LoginWithVerificationRequestDto request);
        Task<string> Register(RegisterRequestDto request);
        Task<bool> SendEmailVerificationCode(EmailVerificationRequestDto request);
        Task<bool> SendSmsVerificationCode(SmsVerificationRequestDto request);
    }
}