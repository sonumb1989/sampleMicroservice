using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Messaging
{
    public interface IMessageHandler
    {
        void Start(IMessageHandlerCallback callback);
        void Stop();
    }
}
