// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net;
using System.Text.Json;
using MetaTools.Consoles;

Console.WriteLine("Hello, World!");


string email = "100051462545105";
string pass = "Tiênyuh76hang";
string code2fa = "MVEO CSRP CEZ5 JJSS 4HNR OLOZ JTDM V55Z";
string ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36";

var cookie = await FacebookeHelper.Login(email, pass, code2fa, ua);

//var token =  FacebookeHelper.GetTokenEAAB(cookie, ua);

var token = await FacebookeHelper.GetAccessTokenEaab(cookie, ua);

Debug.WriteLine("Done");