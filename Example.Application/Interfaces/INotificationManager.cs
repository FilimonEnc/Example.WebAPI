using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace Example.Application.Interfaces
{
    public interface INotificationManager
    {
        IReadOnlyCollection<INotification> Events { get; }

        void AddEvent(INotification eventItem);
        void ClearEvents();
        Task PublishAsync();
        void RemoveEvent(INotification eventItem);
    }
}