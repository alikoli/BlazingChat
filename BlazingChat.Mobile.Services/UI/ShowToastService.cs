namespace BlazingChat.Mobile.Services;

public class ShowToastService : IShowToastService
{
    private readonly IToastService _service;

    public ShowToastService(IToastService service)
    {
        this._service = service;
    }

    public void ShowError(string message, string heading = "", Action? onClick = null)
    {
        _service.ShowError(message, heading, onClick);
    }

    public void ShowInfo(string message, string heading = "", Action? onClick = null)
    {
        _service.ShowError(message, heading, onClick);
    }


    public void ShowSuccess(string message, string heading = "", Action? onClick = null)
    {
        _service.ShowSuccess(message, heading, onClick);
    }

    public void ShowWarning(string message, string heading = "", Action? onClick = null)
    {
        _service.ShowWarning(message,heading, onClick);
    }

}
