namespace BlazingChat.Shared.Services;

public interface IShowToastService
{
    void ShowError(string message, string heading = "", Action onClick = null);
    void ShowInfo(string message, string heading = "", Action onClick = null);
    void ShowSuccess(string message, string heading = "", Action onClick = null);
    void ShowWarning(string message, string heading = "", Action onClick = null);
}
