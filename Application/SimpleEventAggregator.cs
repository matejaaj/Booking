using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application
{
    public class SimpleEventAggregator:  IEventAggregator
    {
        private readonly Dictionary<Type, Delegate> _subscribers = new Dictionary<Type, Delegate>();

        public void Subscribe<TEventData>(Action<TEventData> action)
        {
            if (_subscribers.ContainsKey(typeof(TEventData)))
            {
                _subscribers[typeof(TEventData)] = Delegate.Combine(_subscribers[typeof(TEventData)], action);
            }
            else
            {
                _subscribers[typeof(TEventData)] = action;
            }
        }

        public void Publish<TEventData>(TEventData eventData)
        {
            if (_subscribers.ContainsKey(typeof(TEventData)))
            {
                var action = _subscribers[typeof(TEventData)] as Action<TEventData>;
                action?.Invoke(eventData);
            }
        }
    }
}
