﻿using Domain.Common;

namespace Applications.Common
{
	public interface IEventBus<TEvent> where TEvent : DomainEvent
	{
		public Task PublishAsync(IEnumerable<TEvent> events);
	}
}

