#region Config

Console.Title = "MetaTools";
Console.InputEncoding = Encoding.Unicode;
Console.OutputEncoding = Encoding.Unicode;

#endregion

#region Todo

var via = "100053306310504|thieu123aA@|3YKD2XK2MWCEUO3IN6H3M5Z5VCHDBULO|charlenecp6lk@outlook.com|Facebook.com|Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36";

var cookie = FacebookHelper.LoginMFacebook("100053306310504", "thieu123aA@", "3YKD2XK2MWCEUO3IN6H3M5Z5VCHDBULO", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36");

Console.ReadKey();

#endregion