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
        private readonly Dictionary<Type, List<Delegate>> _subscribersAsync = new Dictionary<Type, List<Delegate>>();


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

        public async Task PublishAsync<TEventData>(TEventData eventData)
        {
            Type eventType = typeof(TEventData);
            if (_subscribersAsync.TryGetValue(eventType, out var delegates))
            {
                foreach (var del in delegates)
                {
                    if (del is Func<TEventData, Task> asyncAction)
                    {
                        await asyncAction(eventData);
                    }
                    else if (del is Action<TEventData> syncAction)
                    {
                        syncAction(eventData);
                    }
                }
            }
        }

        public void SubscribeAsync<TEventData>(Action<TEventData> action)
        {
            var eventType = typeof(TEventData);
            if (!_subscribersAsync.TryGetValue(eventType, out var delegates))
            {
                delegates = new List<Delegate>();
                _subscribersAsync[eventType] = delegates;
            }
            delegates.Add(action);
        }
    }
}
