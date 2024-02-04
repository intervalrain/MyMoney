using Applications;
using Applications.Repositories;
using Applications.Common;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Presentation.Views;
using Domain.Common;
using Infrastructure.DbContexts;

namespace Presentation;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Add services to the container
		builder.Services.AddMyMoneyApplication();
		builder.Services.AddTransient<SummaryPage>();
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddSingleton<IEventBus<DomainEvent>, FakeEventBus>();
		var connStr = "Host=localhost;Port=5432;Username=postgres;Password=#rain1011;Database=mym";
        builder.Services.AddDbContextPool<UserDbContext>(opt => opt.UseNpgsql(connStr));
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}

