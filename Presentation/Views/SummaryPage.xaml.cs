using Applications.Usecases;
using Presentation.Presenters;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class SummaryPage : ContentPage
{
    private static UserSummary _userSummary = new UserSummary(
        "Rain Hu",
        new List<Balance>
        {
            new Balance("Asset", 12234143, 5272934),
            new Balance("Annual", 2124590, 540200),
            new Balance("Month", 75000, 37000),
            new Balance("Week", 0, 8500),
            new Balance("Day", 0, 1300)
        }
    );

    private readonly IServiceProvider _serviceProvider;

    public SummaryPage(IServiceProvider serviceProvider)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
	}

    void OnSwiped(System.Object sender, Microsoft.Maui.Controls.SwipedEventArgs e)
    {
		switch (e.Direction)
		{
			case SwipeDirection.Left:
				Shell.Current.GoToAsync(nameof(AccountPage));
				break;
			case SwipeDirection.Right:
                Shell.Current.GoToAsync(nameof(InputPage));
                break;
            case SwipeDirection.Up:
                Shell.Current.GoToAsync(nameof(ChartPage));
                break;
            case SwipeDirection.Down:
                Shell.Current.GoToAsync(nameof(CalendarPage));
                break;
		}
    }

    async void ContentPage_Loaded(System.Object sender, System.EventArgs e)
    {
        var list = _userSummary.Balances;
        summaryList.ItemsSource = list;

        var presenter = new DefaultPresenter<ListAllUsersResponse>();
        var service = _serviceProvider.GetService<ListAllUsersUsecase>();
        await service.ExecuteAsync(new ListAllUsersRequest(), presenter);
        var users = presenter.Value.UserIds;
        userPicker.ItemsSource = users;
        userPicker.SelectedIndex = 0;
    }
}
