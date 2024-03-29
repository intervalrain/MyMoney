﻿using Applications.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Applications
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddMyMoneyApplication(this IServiceCollection services)
		{
			return services.AddUseCases();
		}

		private static IServiceCollection AddUseCases(this IServiceCollection services)
		{
			var assembly = typeof(DependencyInjection).Assembly;
			var types = assembly.GetTypes();
			var usecase = typeof(Usecase<,>);

			foreach (var type in types.Where(t => t.BaseType?.IsGenericType is true && t.IsAbstract == false))
			{
				if (type.BaseType?.GetGenericTypeDefinition() == usecase)
				{
					services.AddTransient(type, type);
				}
			}
			return services;
		}
	}
}

