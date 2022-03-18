using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Ionic.Toast;

public partial class IonicToasts
{
    [Inject] private IToastService ToastService { get; set; }

    public void VisibleChanged(bool visible)
    {
        if (visible == false && IsVisible== true)
        {
            RemoveToast();
            RefreshToast();
        }
    }

    public bool IsVisible { get; set; }
    public ToastSettings Settings { get; set; } = new ToastSettings();

    [Parameter] public string InfoClass { get; set; }
    [Parameter] public string InfoIcon { get; set; }
    [Parameter] public string SuccessClass { get; set; }
    [Parameter] public string SuccessIcon { get; set; }
    [Parameter] public string WarningClass { get; set; }
    [Parameter] public string WarningIcon { get; set; }
    [Parameter] public string ErrorClass { get; set; }
    [Parameter] public string ErrorIcon { get; set; }
    [Parameter] public ToastPosition Position { get; set; } = ToastPosition.Bottom;
    [Parameter] public int Timeout { get; set; } = 5;

    internal List<ToastSettings> ToastList { get; set; } = new List<ToastSettings>();

    protected override void OnInitialized()
    {
        ToastService.OnShow += ShowToast;

    }

    public void RemoveToast()
    {
        if (Settings.IsInitialized)
        {
            ToastList.Remove(Settings);
            Settings = new ToastSettings();
            IsVisible = false;
            StateHasChanged();
        }
    }

    private void ClearToasts(object sender, LocationChangedEventArgs args)
    {
        InvokeAsync(() =>
        {
            ToastList.Clear();
            IsVisible = false;
            Settings = new ToastSettings();
            StateHasChanged();
        });
    }

    private ToastSettings BuildToastSettings(ToastLevel level, string message, string heading, string duration, Action? onclick, Guid id)
    {
        switch (level)
        {
            case ToastLevel.Error:
                return new ToastSettings(heading: heading, message, "Danger", Position.ToString().ToLower(), duration,
                    "blazored-toast-error", ErrorClass, ErrorIcon, onclick, id);

            case ToastLevel.Info:
                return new ToastSettings(heading: heading, message, "Primary", Position.ToString().ToLower(), duration,
                    "blazored-toast-info", InfoClass, InfoIcon, onclick, id);

            case ToastLevel.Success:
                return new ToastSettings(heading: heading, message, "Success", Position.ToString().ToLower(), duration,
                    "blazored-toast-success", SuccessClass, SuccessIcon, onclick, id);

            case ToastLevel.Warning:
                return new ToastSettings(heading: heading, message, "Warning", Position.ToString().ToLower(), duration,
                    "blazored-toast-warning", WarningClass, WarningIcon, onclick, id);
        }

        throw new InvalidOperationException();
    }

    private void ShowToast(ToastLevel level, string message, string heading, Action? onClick)
    {
        InvokeAsync(() =>
        {
            var toastSettings = BuildToastSettings(level, message, heading, (Timeout*1000).ToString(), onClick, Guid.NewGuid());
            ToastList.Add(toastSettings);
            RefreshToast();
        });
    }

    private void RefreshToast()
    {
        if (ToastList.Count > 0)
        {
            if (!Settings.IsInitialized)
            {
                Settings = ToastList.FirstOrDefault();
                IsVisible = true;
                StateHasChanged();
            }
        }
        else
        {
            IsVisible = false;
            Settings = new ToastSettings();
            StateHasChanged();
        }
    }

    private void ToastClick()
    {
        Settings.OnClick?.Invoke();
    }
}
