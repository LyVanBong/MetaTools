using OtpNet;

namespace MetaTools.Services.TwoFactorAuthentication;

public class TwoFactorAuthentication : ITwoFactorAuthentication
{
    public string GetCode2Fa(string secretKey)
    {
        Totp totp = new Totp(Base32Encoding.ToBytes(secretKey.Replace(" ", "")));
        return totp.ComputeTotp(DateTime.UtcNow);
    }
}