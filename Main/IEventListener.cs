using System;

namespace SimpleBus
{
    public interface IEventListener<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
}