namespace ClientManagerBTG.Features.Base;

public partial class BaseViewModel : ObservableObject
{
    #region Services
    public readonly IWindowService _windowService;
    #endregion

    #region Commands
    [RelayCommand]
    public void Cancel(Page page)
    {
        _windowService.ClosePage(page);
    }
    #endregion

    #region Ctor

    public BaseViewModel(IWindowService windowService)
    {
        _windowService = windowService;
    }
    #endregion
}