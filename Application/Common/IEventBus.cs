using Domain.Common;

namespace Application.Common
{
	public interface EventBus<TEvent> where TEvent : DomainEvent
	{
		public Task PublishAsync(IEnumerable<TEvent> events);
	}
}

