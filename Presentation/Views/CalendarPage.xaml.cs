namespace Presentation.Views;

public partial class CalendarPage : ContentPage
{
	public CalendarPage()
	{
		InitializeComponent();
	}

    void OnSwiped(System.Object sender, Microsoft.Maui.Controls.SwipedEventArgs e)
    {
        switch (e.Direction)
        {
            case SwipeDirection.Up:
                Shell.Current.GoToAsync($"//{nameof(SummaryPage)}");
                break;
            case SwipeDirection.Left:
            case SwipeDirection.Down:
            case SwipeDirection.Right:
                break;
        }
    }
}
