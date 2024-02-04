using Applications.Common;
using Domain.Common;
using Presentation.Common;

namespace Presentation
{
	internal class MyMoneyEventBus : IEventBus<DomainEvent>
	{
        private readonly Dictionary<Type, IMyMoneyEventHandler> _handlers; 

        internal MyMoneyEventBus(IEnumerable<IMyMoneyEventHandler> handlers)
        {
            _handlers = handlers.ToDictionary(h => h.EventType, h => h);
        }

        public async Task PublishAsync(IEnumerable<DomainEvent> events)
        {
            foreach (var e in events)
            {
                var handler = GetHandler(e);
                await handler!.HandleAsync(e);
            }
        }

        private IMyMoneyEventHandler GetHandler(DomainEvent e)
        {
            var type = e.GetType();
            if (!_handlers.TryGetValue(type, out var handler))
            {
                throw new InvalidOperationException($"Handler for {type} not registered");
            }
            return handler;
        }
    }
}

