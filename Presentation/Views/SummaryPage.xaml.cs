using System.Windows.Input;

namespace Presentation.Views;

public partial class SummaryPage : ContentPage
{
	public ICommand NavigateCommand { get; private set; } 

	public SummaryPage()
	{
		InitializeComponent();
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
                break;
		}
    }
}
