using System.Collections.Generic;
using Exchange.Rate.Domain.Notifications;

namespace Exchange.Rate.Domain.Interfaces.Notifications
{
    public interface IDomainNotification
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
    }
}
