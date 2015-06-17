using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claudia.Domain.EventManager
{





    public class EventManager : IPublisher
    {
        static EventManager _instance = new EventManager();
        Dictionary<string, List<ISubscriber>> _subscribers = new Dictionary<string, List<ISubscriber>>();
        Dictionary<string, Dictionary<ISubscriber, Action>> _subs = new Dictionary<string, Dictionary<ISubscriber, Action>>();

        Dictionary<string, Events> _events = new Dictionary<string, Events>();

        public static EventManager Instance { get { return _instance; } }

        public void Publish(string channel, object message)
        {
            if (!Exists(channel)) return;
            foreach (var subscriber in _subscribers[channel])
                subscriber.SubscriptionUpdate(message);
        }
        public void Publish(string channel)
        {
            if (!_subs.ContainsKey(channel)) return;
            foreach (var subscriber in _subs[channel])
                subscriber.Value();
        }

        public void Subscribe(ISubscriber subscriber, string channel)
        {
            if (!Exists(channel))
                _subscribers[channel] = new List<ISubscriber>();
            if (!_subscribers[channel].Contains(subscriber))
                _subscribers[channel].Add(subscriber);
        }
        public void Subscribe(ISubscriber subscriber, string channel, Action callback)
        {
            if (!_subs.ContainsKey(channel))
            {
                _subs.Add(channel, new Dictionary<ISubscriber, Action>() { { subscriber, callback } });
            } else if (!_subs.First(s => s.Key.Contains(channel)).Value.ContainsKey(subscriber)){
                _subs.First(s => s.Key.Contains(channel))
                               .Value.Add(subscriber, callback);
            }
        }

        //public void Subscribe(string channel, Action callback)
        //{
        //    if (!_events.ContainsKey(channel))
        //        _events[channel] = new Events();

        //    if (!_events[channel].Keys.Contains(subscriber))
        //    //_events.Add(channel,
        //}

        public void Unsubscribe(ISubscriber subscriber, string channel)
        {
            if (Exists(channel))
                _subscribers[channel].Remove(subscriber);
        }

        bool Exists(string channel)
        {
            return _subscribers.ContainsKey(channel);
        }

        internal class Events
        {
            private event EventHandler _eve = delegate { };
            internal event EventHandler OnEvents
            {
                add { lock (_eve) { _eve += value; } }
                remove { lock (_eve) { _eve -= value; } }
            }

            internal void Raise()
            {
                _eve(this, EventArgs.Empty);
            }
        }
    }
}
