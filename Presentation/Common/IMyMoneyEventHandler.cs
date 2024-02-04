using Domain.Common;

namespace Presentation.Common
{
	public interface IMyMoneyEventHandler
	{
		public Type EventType { get; }
		Task HandleAsync(DomainEvent e);
	}
}

