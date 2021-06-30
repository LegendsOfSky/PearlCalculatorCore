using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearlCalculatorCP
{
    public class EventManager
    {
        private static EventManager _instance;
        public static EventManager Instance => _instance ??= new EventManager();

        interface IEventHandlerWrapper
        {
            public void Invoke(object sender, PCEventArgs e);
        }

        class EventHandlerWrapper<T> : IEventHandlerWrapper where T : PCEventArgs
        {

            public event PCEventHandler<T> Handler;

            public void Invoke(object sender, PCEventArgs e) => Handler?.Invoke(sender, (T)e);
        }


        Dictionary<string, Dictionary<Type, IEventHandlerWrapper>> _eventCon;


        private EventManager()
        {
            _eventCon = new Dictionary<string, Dictionary<Type, IEventHandlerWrapper>>();
        }

        public static void AddListener<T>(string eventKey, PCEventHandler<T> eventHandler) where T : PCEventArgs
        {
            if (string.IsNullOrEmpty(eventKey) || string.IsNullOrWhiteSpace(eventKey))
                throw new ArgumentNullException(nameof(eventKey));

            if (eventHandler is null)
                throw new ArgumentNullException(nameof(eventHandler));

            if (!Instance._eventCon.TryGetValue(eventKey, out var handlerDict))
            {
                handlerDict = new Dictionary<Type, IEventHandlerWrapper>();
                Instance._eventCon.Add(eventKey, handlerDict);
            }

            EventHandlerWrapper<T> wrapper;
            if (!handlerDict.TryGetValue(typeof(T), out var w))
                handlerDict.Add(typeof(T), wrapper = new EventHandlerWrapper<T>());
            else
                wrapper = (EventHandlerWrapper<T>)w;

            wrapper.Handler += eventHandler;
        }

        public static void RemoveListener<T>(string eventKey, PCEventHandler<T> eventHandler) where T : PCEventArgs
        {
            if (string.IsNullOrEmpty(eventKey) || string.IsNullOrWhiteSpace(eventKey))
                throw new ArgumentNullException(nameof(eventKey));

            if (eventHandler is null)
                throw new ArgumentNullException(nameof(eventHandler));

            if (!Instance._eventCon.TryGetValue(eventKey, out var handlerDict)) return;

            if (handlerDict.TryGetValue(typeof(T), out var w))
            {
                var wrapper = (EventHandlerWrapper<T>)w;
                wrapper.Handler -= eventHandler;
            }
        }

        public static void PublishEvent<T>(object sender, string eventKey, T args) where T : PCEventArgs
        {
            if (string.IsNullOrEmpty(eventKey) || string.IsNullOrWhiteSpace(eventKey))
                throw new ArgumentNullException(nameof(eventKey));

            if (args is null)
                throw new ArgumentNullException(nameof(args));

            if (Instance._eventCon.TryGetValue(eventKey, out var dict) &&
                dict.TryGetValue(typeof(T), out var wrapper))
            {
                wrapper?.Invoke(sender, args);
            }
        }
    }

    public delegate void PCEventHandler<in T>(object sender, T e);
}
