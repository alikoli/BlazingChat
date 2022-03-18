using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Blazor.Ionic.Toast
{
    public enum IconType { FontAwesome, Material };

    public partial class IonicToasts
    {
        public bool IsVisible { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Parameter] public IconType? IconType { get; set; }
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
        [Parameter] public bool RemoveToastsOnNavigation { get; set; }
        [Parameter] public bool ShowProgressBar { get; set; }

        private string PositionClass { get; set; } = string.Empty;
        internal List<ToastInstance> ToastList { get; set; } = new List<ToastInstance>();

        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;

            if (RemoveToastsOnNavigation)
            {
                NavigationManager.LocationChanged += ClearToasts;
            }

            PositionClass = $"position-{Position.ToString().ToLower()}";

            if ((   !string.IsNullOrEmpty(InfoIcon)
                 || !string.IsNullOrEmpty(SuccessIcon)
                 || !string.IsNullOrEmpty(WarningIcon)
                 || !string.IsNullOrEmpty(ErrorIcon)
                ) && IconType == null)
            {
                throw new ArgumentException("If an icon is specified then IconType is a required parameter.");
            }
        }

        public void RemoveToast(Guid toastId)
        {
            InvokeAsync(() =>
            {
                var toastInstance = ToastList.SingleOrDefault(x => x.Id == toastId);
                ToastList.Remove(toastInstance);
                IsVisible = (ToastList.Count>0)? true: false;
                StateHasChanged();
            });
        }

        private void ClearToasts(object sender, LocationChangedEventArgs args)
        {
            InvokeAsync(() =>
            {
                ToastList.Clear();
                IsVisible= false;
                StateHasChanged();
            });
        }

        private ToastSettings BuildToastSettings(ToastLevel level, string message, string heading, string duration, Action? onclick)
        {
            switch (level)
            {
                case ToastLevel.Error:
                    return new ToastSettings(heading : heading, message, "Danger", Position.ToString().ToLower(), duration,
                        "blazored-toast-error", ErrorClass, ErrorIcon, onclick);

                case ToastLevel.Info:
                    return new ToastSettings(heading : heading, message, "Primary", Position.ToString().ToLower(), duration,
                        "blazored-toast-info", InfoClass, InfoIcon, onclick);

                case ToastLevel.Success:
                    return new ToastSettings(heading: heading, message, "Success", Position.ToString().ToLower(), duration,
                        "blazored-toast-success", SuccessClass, SuccessIcon, onclick);

                case ToastLevel.Warning:
                    return new ToastSettings(heading: heading, message, "Warning", Position.ToString().ToLower(), duration,
                        "blazored-toast-warning", WarningClass, WarningIcon, onclick);
            }

            throw new InvalidOperationException();
        }

        private void ShowToast(ToastLevel level, string message, string heading, Action? onClick)
        {
            InvokeAsync(() =>
            {
                var settings = BuildToastSettings(level, message, heading, ((Timeout-2)*1000).ToString(),onClick);
                var toast = new ToastInstance
                {
                    Id = Guid.NewGuid(),
                    TimeStamp = DateTime.Now,
                    ToastSettings = settings
                };
                IsVisible = true;
                ToastList.Add(toast);

                StateHasChanged();
            });

        }
    }
}
