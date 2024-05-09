using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application
{
    public interface IEventAggregator
    {
        void Subscribe<TEventData>(Action<TEventData> action);
        void Publish<TEventData>(TEventData eventData);

        Task PublishAsync<TEventData>(TEventData eventData);

        void SubscribeAsync<TEventData>(Action<TEventData> action);
    }
}
