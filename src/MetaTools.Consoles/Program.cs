// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using MetaTools.Consoles;

Console.WriteLine("Hello, World!");


string email = "bonglv@outlook.com";
string pass = "B0nglv@s0ftty.n3t";
string code2fa = "HCIX RBJD T7CZ TB2T ITRT 66QX ZRT3 F5VN";
string ua = "Mozilla/5.0 (X11; Linux i686; rv:49.0) Gecko/20100101 Firefox/49.0";

var cookie = await FacebookeHelper.Login(email, pass, code2fa, ua);

Debug.WriteLine($"Cookie={cookie}");