using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Example.Application.Interfaces;

namespace Example.Application.Notification
{
    public class NotificationManager : INotificationManager
    {
        public IReadOnlyCollection<INotification> Events => _events.AsReadOnly();

        private readonly IMediator _mediator;
        private readonly List<INotification> _events = new();

        public NotificationManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void AddEvent(INotification eventItem)
        {
            _events.Add(eventItem);
        }

        public void RemoveEvent(INotification eventItem)
        {
            _events.Remove(eventItem);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }

        public async Task PublishAsync()
        {
            foreach (var item in Events)
            {
                await _mediator.Publish(item);
            }
        }
    }
}
