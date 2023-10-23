#region Config

using static System.Net.Mime.MediaTypeNames;

Console.Title = "MetaTools";
Console.InputEncoding = Encoding.Unicode;
Console.OutputEncoding = Encoding.Unicode;

#endregion

#region Todo

string via = "100055097605078|HDDiep@!1600369523|SFVMTSAG42LCSBEYHW62FBVCTBILDMVQ|maksimigdkn@hotmail.com|p1tD7dke0x3";
via = "100055163693038|HDDiep@!1600380448|BMJ4HTK3DE6ARBCJUO4UEES7BTLH2Z24|d6cisaev@hotmail.com|zfs8Afe70g";
string[] data = via.Split('|', StringSplitOptions.RemoveEmptyEntries);
string uid = data[0];
string pass = data[1];
string code2Fa = data[2];
string email = data[3];
string passEmail = data[4];
string ua = "Mozilla/5.0 (Linux; Android 10) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.5993.65 Mobile Safari/537.36";

var cookie = await FacebookHelper.Login(uid, pass, code2Fa, ua);
var ck = await FacebookHelper.CheckPoint_828281030927956(cookie, ua, email, passEmail);

Console.ReadKey();

#endregion