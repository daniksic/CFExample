using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claudia.Domain.EventManager
{
    public interface ISubscriber
    {
        void SubscriptionUpdate(object message);
    }
}
