using System;
using Microsoft.AspNetCore.Components;

namespace Blazor.Ionic.Toast
{
    public interface IToastService
    {
        /// <summary>
        /// A event that will be invoked when showing a toast
        /// </summary>
        event Action<ToastLevel, string, string, Action> OnShow;

        void ShowError(string message, string heading = "", Action onClick = null);
        void ShowInfo(string message, string heading = "", Action onClick = null);
        void ShowSuccess(string message, string heading = "", Action onClick = null);
        void ShowWarning(string message, string heading = "", Action onClick = null);
    }
}
