using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claudia.Domain.EventManager
{
    public interface IPublisher
    {
        void Publish(string channel, object message);
        void Subscribe(ISubscriber subscriber, string channel);
        void Unsubscribe(ISubscriber subscriber, string channel);
    }
}
