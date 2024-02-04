using System.Reflection;
using Applications.Common;
using Applications.Repositories;
using Domain.Common;
using Infrastructure.Repositories;
using Presentation.Common;

namespace Presentation
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddMyMoneyPresentation(this IServiceCollection services)
		{
			services.AddSingleton<IUserRepository, UserRepository>()
					.AddSingleton<IEventBus<DomainEvent>, MyMoneyEventBus>();
			return services;
		}

		public static IServiceCollection AddMyMoneyEventHandler(this IServiceCollection services)
		{
			var handlers = Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(IMyMoneyEventHandler)))
				.ToList();
			foreach (var handler in handlers)
			{
				services.AddSingleton(typeof(IMyMoneyEventHandler), handler);
			}
			return services;
		}
	}
}

