using Prism.Mvvm;

namespace MetaTools.Models;

public class MenuModel : BindableBase
{
    private bool _isActive;
    private string _icon;
    private string _title;
    private string _iconGray;
    public int Id { get; set; }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Icon
    {
        get => _icon;
        set => SetProperty(ref _icon, value);
    }

    public string IconWhite
    {
        get => _iconGray;
        set => SetProperty(ref _iconGray, value);
    }

    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }

    public string ContentRegion { get; set; }
}