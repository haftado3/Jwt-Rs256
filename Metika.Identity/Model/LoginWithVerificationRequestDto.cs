namespace Metika.Identity.Model
{
    public record LoginWithVerificationRequestDto(string PhoneNumber, string VerificationCode);
}
