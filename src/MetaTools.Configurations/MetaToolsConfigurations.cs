namespace MetaTools.Configurations;

public class MetaToolsConfigurations
{
    /// <summary>
    /// Đăng ký sử dụng dịch vụ của AppCenter
    /// </summary>
    public static async Task RegisterAppCenter()
    {
        // Config AppCenter
        var countryCode = RegionInfo.CurrentRegion.TwoLetterISORegionName;

        AppCenter.SetCountryCode(countryCode);
        await AppCenter.SetEnabledAsync(true);
        await Crashes.SetEnabledAsync(true);
        await Analytics.SetEnabledAsync(true);

        AppCenter.Start("1eaba9dd-3cb4-40c0-8757-124b3b247488",
            typeof(Analytics), typeof(Crashes));

        // Định danh app
        //var installId = AppCenter.GetInstallIdAsync();

        // Tự đặt id
        //AppCenter.SetUserId("your-user-id");
        var app = AppDomain.CurrentDomain;
        Analytics.TrackEvent("AppDomain", new Dictionary<string, string>()
        {
            {"Name",app.FriendlyName},
            {"BaseDirectory",app.BaseDirectory},
            {"Identifier",await AppCenter.GetInstallIdAsync()+""}
        });
    }

    public static Task RegisterSyncfusion()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc3Mjk1NUAzMjMzMmUzMDJlMzBsMDZuRWk0cVlMVk5wdjhvM1dpZjMwM0JDQS9wOEZCRVhzN0Q4cHBORjVzPQ==");
        return Task.CompletedTask;
    }
}