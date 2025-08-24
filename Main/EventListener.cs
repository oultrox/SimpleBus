using System;

namespace SimpleBus
{
    public class EventListener<T> : IEventListener<T> where T : IEvent
    {
        Action<T> onEvent = _ => { };
        Action onEventNoArgs = () => { };
    
        Action<T> IEventListener<T>.OnEvent { get => onEvent; set => onEvent = value; }
        Action IEventListener<T>.OnEventNoArgs { get => onEventNoArgs; set => onEventNoArgs = value;}


        public EventListener(Action<T> onEvent)
        {
            this.onEvent = onEvent;
        }

        public EventListener(Action onEventNoArgs)
        {
            this.onEventNoArgs = onEventNoArgs;
        }
    
        public void Add(Action<T> onEvent)
        {
            this.onEvent += onEvent;
        }

        public void Remove(Action<T> onEvent)
        {
            this.onEvent -= onEvent;
        }

        public void Add(Action onEvent)
        {
            onEventNoArgs += onEvent;
        }

        public void Remove(Action onEvent)
        {
            onEventNoArgs -= onEvent;
        }
    }
}