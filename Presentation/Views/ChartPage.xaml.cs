namespace Presentation.Views;

public partial class ChartPage : ContentPage
{
	public ChartPage()
	{
		InitializeComponent();
	}

    void OnSwiped(System.Object sender, Microsoft.Maui.Controls.SwipedEventArgs e)
    {
        switch (e.Direction)
        {
            case SwipeDirection.Down:
                Shell.Current.GoToAsync($"//{nameof(SummaryPage)}");
                break;
            case SwipeDirection.Left:
            case SwipeDirection.Up:
            case SwipeDirection.Right:
                break;
        }
    }
}
