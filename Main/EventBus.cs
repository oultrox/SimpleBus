using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SimpleBus
{
    public static class EventBus<T> where T : IEvent
    {
        static readonly HashSet<IEventListener<T>> bindings = new();

        public static void Register(IEventListener<T> listener)
        {
            if (EventBusLogger.IsLoggingEnabled)
            {
                Debug.Log("Event Listener Registered : " + typeof(T).Name);
            }
            
            bindings.Add(listener);
        }

        public static void Deregister(IEventListener<T> listener)
        {
            if (EventBusLogger.IsLoggingEnabled)
            {
                Debug.Log("Event Listener Deregistered : " + typeof(T).Name);
            }
            
            bindings.Remove(listener);
        }

        public static void Raise(T @event)
        {
            foreach (var eventBinding in bindings.ToList())
            {
                eventBinding.OnEvent.Invoke(@event);
                eventBinding.OnEventNoArgs.Invoke();
            }
        }

        static void Clear()
        {
            if (EventBusLogger.IsLoggingEnabled)
            {
                Debug.Log("Event Listener cleared: " + typeof(T).Name);
            }
            
            bindings.Clear();
        }
    }
}

