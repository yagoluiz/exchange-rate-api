using System.Collections.Generic;
using System.Linq;
using Exchange.Rate.Domain.Interfaces.Notifications;

namespace Exchange.Rate.Domain.Notifications
{
    public class DomainNotification : IDomainNotification
    {
        private readonly List<NotificationMessage> _notifications;

        public DomainNotification()
        {
            _notifications = new List<NotificationMessage>();
        }

        public IReadOnlyCollection<NotificationMessage> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new NotificationMessage(key, message));
        }
    }
}
