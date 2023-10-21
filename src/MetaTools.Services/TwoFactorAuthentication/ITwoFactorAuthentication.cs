namespace MetaTools.Services.TwoFactorAuthentication;

public interface ITwoFactorAuthentication
{
    string GetCode2Fa(string secretKey);
}