namespace MetaTools.Repositories;

public class SqLiteSetting
{
    public static SqLiteSetting Instance { get; } = Instance ?? new SqLiteSetting();
    public SQLiteAsyncConnection SqLiteAsyncConnection { get; set; }

    public SqLiteSetting()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MetaTools\\";

        if (!File.Exists(path: path))
        {
            Directory.CreateDirectory(path: path);
        }

        SQLiteOpenFlags flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        string databasePath = Path.Combine(path, "MetaTools.db");

        string databaseKey = "3e46372e01b0d19ab5c21ab96a8200bae5726bd6af5d9cd85dff62096daa083d2fc23693ccedc6556b815766d24ae2adec632bcae85ffd13014057a8d30d3fe0";
        var options = new SQLiteConnectionString(databasePath: databasePath,
            openFlags: flags,
            key: databaseKey,
            storeDateTimeAsTicks: true);
        SqLiteAsyncConnection = new SQLiteAsyncConnection(options);
        SqLiteAsyncConnection.EnableWriteAheadLoggingAsync();
    }
}