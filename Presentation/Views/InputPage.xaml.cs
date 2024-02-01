namespace Presentation.Views;

public partial class InputPage : ContentPage
{
	public InputPage()
	{
		InitializeComponent();
	}

    void OnSwiped(System.Object sender, Microsoft.Maui.Controls.SwipedEventArgs e)
    {
        switch (e.Direction)
        {
            case SwipeDirection.Left:
                Shell.Current.GoToAsync($"//{nameof(SummaryPage)}");
                break;
            case SwipeDirection.Right:
            case SwipeDirection.Up:
            case SwipeDirection.Down:
                break;
        }
    }
}
