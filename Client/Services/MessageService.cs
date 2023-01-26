using System;
using Radzen;
using Radzen.Blazor;

namespace HelpDesk.Client.Services
{
    public class MessageService
    {
        public void ShowNotification(NotificationService notificationService, string title, string message, NotificationSeverity severity)
        {

            var notification = new NotificationMessage { Severity = severity, Summary = title, Detail= message, Style = "border-radius: 0.5rem;" };
            notificationService.Notify(notification);
        }

        public void ShowNotification(NotificationService notificationService, string message, NotificationSeverity severity)
        {

            var notification = new NotificationMessage { Severity = severity, Summary = message, Style = "border-radius: 0.5rem;" };
            notificationService.Notify(notification);
        }
    }
}
