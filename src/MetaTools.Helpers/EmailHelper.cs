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
            string body;
            using (ImapClient client = new ImapClient())
            {
                await client.ConnectAsync("outlook.office365.com", 993, true);
                await client.AuthenticateAsync(email, pass);
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                var message = await inbox.GetMessageAsync(inbox.Count - 1);

                body = message?.TextBody;

                await client.DisconnectAsync(true);
            }

            if (!string.IsNullOrEmpty(body))
            {
                return Regex.Match(body, @"\d{6,}")?.Value;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return string.Empty;
    }
}