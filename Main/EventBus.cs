using System.Collections.Generic;
using UnityEngine;

namespace SimpleBus
{
    public static class EventBus<T> where T : IEvent
    {
        static readonly HashSet<IEventListener<T>> bindings = new();

        public static void Register(IEventListener<T> listener)
        {
            bindings.Add(listener);
        }

        public static void Deregister(IEventListener<T> listener)
        {
            bindings.Remove(listener);
        }

        public static void Raise(T @event)
        {
            foreach (var eventBinding in bindings)
            {
                eventBinding.OnEvent.Invoke(@event);
                eventBinding.OnEventNoArgs.Invoke();
            }
        }

        static void Clear()
        {
            Debug.Log("Event Binding cleared: " + typeof(T).Name);
            bindings.Clear();
        }
    }
}

