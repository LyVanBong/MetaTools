using MetaTools.Models;

namespace MetaTools.Consoles;

public class PageHelper
{
    public static async Task SendMessagerAsync()
    {
        try
        {
            string cookie = "sb=oMKRYkzysM6HwZPpZUdY2qyX; datr=83rbYoY0JoxvW__xrpK4P3dK; locale=vi_VN; wl_cbv=v2%3Bclient_version%3A2335%3Btimestamp%3A1697397274; m_pixel_ratio=2; c_user=100027295904383; wd=2560x923; xs=35%3Ax8MizmG4Hi8y-Q%3A2%3A1697651411%3A-1%3A2769%3A%3AAcUitT92MEekxjGYRqQomd8gV_o1kycCY9xNssFfCw; usida=eyJ2ZXIiOjEsImlkIjoiQXMydXoyeGw0ZjFhMSIsInRpbWUiOjE2OTc4NTgzNjB9; fr=1I8SdRm0MO3PD71fI.AWWmgeyZ2NLMlWv6pehqPmLrZOc.BlMz5r.kv.AAA.0.0.BlM0Nt.AWXEK2dFvog";
            string ua =
                "Mozilla/5.0 (iPad; CPU OS 14_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) FxiOS/118.0 Mobile/15E148 Safari/605.1.15";
            string tokenUser;
            string pathMessage;

            // cookie
            Console.Write("Nhập cookie: ");
            while (true)
            {
                var ck = Console.ReadLine();
                if (string.IsNullOrEmpty(ck))
                {
                    Console.Write("Nhập lại cookie: ");
                }
                else
                {
                    cookie = ck;
                    break;
                }
            }

            // User agent
            Console.WriteLine("Nhập User-Agent (có thể để trống): ");

            var u = Console.ReadLine();
            if (!string.IsNullOrEmpty(u))
            {
                ua = u;
            }

            // lấy token
            Console.WriteLine("Đang lấy AccessToken User");

            var tk = await FacebookHelper.GetAccessTokenEaab(cookie, ua);
            //var tk = "EAABwzLixnjYBOw0sDRmx52yRhQy4lMbjsim5hYdqfJ4XqK0ZCwJ5LubcH4cKGcgE6Dj7mhkSABxvNDcThIZArHmu0rv9xZBpbWZBZBvsStwZAzcjdBHV64FxPNIzuFZBgpP8pk3oUputwagK85JzmIYG9ygPyjNUDH4K0cEO1F6QOBBcDvht8t8lO8GqOOSKI3URSvZCuztc59fH";

            if (string.IsNullOrEmpty(tk))
            {
                Console.Write("Không lấy được AccessToken bạn vui lòng nhập tay: ");
                while (true)
                {
                    tk = Console.ReadLine();
                    if (string.IsNullOrEmpty(tk))
                    {
                        Console.Write("Nhập lại AccessToken: ");
                    }
                    else
                    {
                        tokenUser = tk;
                        break;
                    }
                }
            }

            tokenUser = tk;
            Console.WriteLine("AccessToken: " + tokenUser);

            // Kiểm tra page

            var jsonPageInfo = await FacebookHelper.GetPageInfoAsync(tokenUser);

            if (string.IsNullOrEmpty(jsonPageInfo))
            {
                Console.WriteLine("Không lấy được thông tin page");
            }
            else
            {
                var pageInfo = JsonSerializer.Deserialize<FacebookModel>(jsonPageInfo);
                if (pageInfo != null)
                {
                    Console.WriteLine("Bạn có: " + pageInfo.Accounts.Data.Count + " page");
                    Console.WriteLine("Bạn vui long chọn page");
                    var pages = pageInfo.Accounts.Data;
                    for (int i = 0; i < pages.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {pages[i].Name}");
                    }

                    int chose;
                    while (true)
                    {
                        var c = Console.ReadLine();
                        if (int.TryParse(c, out int result))
                        {
                            chose = result;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Bạn vui lòng chọn lại");
                        }
                    }

                    var page = pages[chose - 1];

                    Console.WriteLine("Bắt đầu kiểm tra tin nhắn page: " + page.Name);
                    List<Datum> messages = new List<Datum>();
                    string url = string.Empty;
                    while (true)
                    {
                        var conver = await FacebookHelper.GetConversationsAsync(page.AccessToken, page.Id, url);
                        var conversation = JsonSerializer.Deserialize<FacebookModel>(conver);
                        if (conversation != null)
                        {
                            if (conversation.Conversations.Data.Any())
                            {
                                messages.AddRange(conversation.Conversations.Data);
                                Console.WriteLine("Đã kiểm tra số tin nhắn: " + messages.Count);
                                url = conversation.Conversations.Paging.Next;
                            }

                            if (conversation.Data.Any())
                            {
                                messages.AddRange(conversation.Data);
                                Console.WriteLine("Đã kiểm tra số tin nhắn: " + messages.Count);
                                url = conversation.Paging.Next;
                            }

                            if (string.IsNullOrEmpty(url))
                            {
                                break;
                            }

                            await Task.Delay(Random.Shared.Next(1000, 2000));
                        }
                    }

                    if (messages.Any())
                    {
                        var jsonString = JsonSerializer.Serialize(messages);

                        await File.AppendAllTextAsync(AppDomain.CurrentDomain.BaseDirectory + "/message.txt", jsonString);

                        Console.WriteLine("Nhập đường dẫn file nội dung tin nhắn (*.txt):");
                        while (true)
                        {
                            var pathM = Console.ReadLine();
                            if (string.IsNullOrEmpty(pathM))
                            {
                                Console.WriteLine("Nhập lại đường dẫn file:");
                            }
                            else
                            {
                                pathMessage = pathM;
                                break;
                            }
                        }

                        var contents = await File.ReadAllTextAsync(pathMessage);
                        if (string.IsNullOrEmpty(contents))
                        {
                            Console.WriteLine("File không có nội dung gì");
                        }
                        else
                        {
                            var listMessage = contents.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Page không có tin nhặn nào");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi phát sinh vui lòng thử lại sau");
            await File.AppendAllTextAsync(AppDomain.CurrentDomain.BaseDirectory + "/error.txt", $"[{DateTime.Now}] " + e.StackTrace);
        }

        Console.WriteLine("Hẹn gặp lại");
    }
}