namespace Metika.Identity.Model
{
    public record RegisterRequestDto(string Username, string Password, string VerificationCode);
}
