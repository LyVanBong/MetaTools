namespace MetaTools.Configurations;

public class MetaToolsConfigurations
{
    /// <summary>
    /// Đăng ký sử dụng dịch vụ của AppCenter
    /// </summary>
    public static void RegisterAppcenter()
    {
        // Config AppCenter
        var countryCode = RegionInfo.CurrentRegion.TwoLetterISORegionName;

        AppCenter.SetCountryCode(countryCode);
        AppCenter.SetEnabledAsync(true);
        Crashes.SetEnabledAsync(true);
        Analytics.SetEnabledAsync(true);

        AppCenter.Start("1eaba9dd-3cb4-40c0-8757-124b3b247488",
            typeof(Analytics), typeof(Crashes));

        // Định danh app
        //var installId = AppCenter.GetInstallIdAsync();

        // Tự đặt id
        //AppCenter.SetUserId("your-user-id");
    }
}