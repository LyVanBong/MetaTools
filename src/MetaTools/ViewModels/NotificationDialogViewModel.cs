namespace MetaTools.ViewModels
{
    public class NotificationDialogViewModel : BindableBase, IDialogAware
    {
        public NotificationDialogViewModel()
        {
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        public string Title { get; }

        public event Action<IDialogResult> RequestClose;
    }
}