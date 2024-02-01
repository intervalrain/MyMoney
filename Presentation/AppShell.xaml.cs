using Presentation.Views;

namespace Presentation;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(SummaryPage), typeof(SummaryPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        Routing.RegisterRoute(nameof(InputPage), typeof(InputPage));
        Routing.RegisterRoute(nameof(ChartPage), typeof(ChartPage));
        Routing.RegisterRoute(nameof(CalendarPage), typeof(CalendarPage));
    }
}

