Console.OutputEncoding = Encoding.Unicode;
string uid = "100053489240893";
string pass = "thieu123aA@";
string code2fa = "HCIX RBJD T7CZ TB2T ITRT 66QX ZRT3 F5VN";
string ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36";
string email = "lnqopenny@hotmail.com";
string passEmail = "u39hL7mt2x6";

var cookie = await FacebookeHelper.Login(uid, pass, code2fa, ua);

Console.WriteLine(cookie);
if (string.IsNullOrEmpty(cookie))
{
    Console.WriteLine("Tk Mk sai");
}
else
{
    var checkPoint = FacebookeHelper.CheckPoint(cookie, ua);
    if (checkPoint)
    {
        Console.WriteLine("Tài khoản bị checkpoint");
        var unCheckPoint828 = FacebookeHelper.CheckPoint_828281030927956(cookie, ua, email, passEmail);
    }
    else
    {
        var tokenPage = FacebookeHelper.GetTokenEAAB(cookie, ua);
        Console.WriteLine(tokenPage);
        var tokenUser = await FacebookeHelper.GetAccessTokenEaab(cookie, ua);
        Console.WriteLine(tokenUser);

        var token = await FacebookeHelper.GetTokenPage(tokenUser, ua, cookie);

        var like = await FacebookeHelper.LikePost(tokenUser);
        if (like)
        {
            Console.WriteLine("Like Done");
        }
        foreach (var s in token)
        {
            like = await FacebookeHelper.LikePost(s);
            if (like)
            {
                Console.WriteLine("Like Done");
            }
        }
    }
}

Debug.WriteLine("Done");