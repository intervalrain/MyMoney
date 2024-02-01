using Domain.Common;

namespace Applications.Common
{
	public interface EventBus<TEvent> where TEvent : DomainEvent
	{
		public Task PublishAsync(IEnumerable<TEvent> events);
	}
}

