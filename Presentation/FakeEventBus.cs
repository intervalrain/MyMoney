using System;
using Applications.Common;
using Domain.Common;

namespace Presentation
{
	public class FakeEventBus : IEventBus<DomainEvent>
	{
		public FakeEventBus()
		{
		}

        public Task PublishAsync(IEnumerable<DomainEvent> events)
        {
			return Task.CompletedTask;
        }
    }
}

