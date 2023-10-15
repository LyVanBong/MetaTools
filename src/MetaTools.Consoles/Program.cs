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

//var cookie = await FacebookeHelper.Login(email, pass, code2fa, ua);

//var token =  FacebookeHelper.GetTokenEAAB(cookie, ua);


var client = new HttpClient();
var request = new HttpRequestMessage(HttpMethod.Get, "http://headers.scrapeops.io/v1/browser-headers?api_key=2fcb18a7-1402-4b38-a6ab-2eb2d77c54ec&num_results=1");
var response = await client.SendAsync(request);
response.EnsureSuccessStatusCode();
var json = await response.Content.ReadAsStringAsync();

var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>[]>>(json);

var header = data["result"][0];

Debug.WriteLine("Done");