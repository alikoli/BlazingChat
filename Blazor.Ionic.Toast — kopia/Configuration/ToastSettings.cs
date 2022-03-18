using System;
using Microsoft.AspNetCore.Components;

namespace Blazor.Ionic.Toast
{
    public class ToastSettings
    {
        public ToastSettings(
            string heading,
            string message,
            string color,
            string position,
            string duration,
            string baseClass,
            string additionalClasses,
            string icon,
            Action? onClick)
        {
            Heading =  heading;
            Message = (icon != null ? "<ion-icon name=\"" + icon + "\" color=\"warning\" size=\"large\"></ion-icon> " : "") + message;
            Color = color.ToLower();
            Position = position.ToLower();
            Duration = duration;
            BaseClass = baseClass;
            AdditionalClasses = additionalClasses;
            Icon = icon;

            OnClick = onClick;
            if (onClick != null)
            {
                AdditionalClasses += " blazored-toast-action";
            }
        }

        public string Heading { get; set; }
        public string Message { get; set; }
        public string Color { get; set; }
        public string Position { get; set; }
        public string BaseClass { get; set; }
        public string AdditionalClasses { get; set; }
        public string Icon { get; set; }
        public string Duration { get; set; }
        public Action? OnClick { get; set; }
    }
}
