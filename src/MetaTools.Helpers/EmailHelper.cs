using Microsoft.AppCenter.Crashes;

namespace MetaTools.Helpers;

public class EmailHelper
{
    public static async Task<string> ReadEmailAsync(string email, string pass)
    {
        //Server name: outlook.office365.com
        //Port: 993
        //Encryption method: TLS

        try
        {
            await Task.Delay(5000);
            using (ImapClient client = new ImapClient())
            {
                await client.ConnectAsync("outlook.office365.com", 993, true);
                if (client.IsConnected)
                {
                    await client.AuthenticateAsync(email, pass);
                    if (client.IsAuthenticated)
                    {
                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadOnly);

                        var message = await inbox.GetMessageAsync(inbox.Count - 1);

                        var body = message?.TextBody;

                        if (!string.IsNullOrEmpty(body))
                        {
                            return Regex.Match(body, @"\d{6,}")?.Value;
                        }
                    }
                }

                await client.DisconnectAsync(true);
            }

        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return string.Empty;
    }
}