namespace Presentation.Views;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();
	}

    void OnSwiped(System.Object sender, Microsoft.Maui.Controls.SwipedEventArgs e)
    {
        switch (e.Direction)
        {
            case SwipeDirection.Right:
                Shell.Current.GoToAsync($"//{nameof(SummaryPage)}");
                break;
            case SwipeDirection.Left:
            case SwipeDirection.Up:
            case SwipeDirection.Down:
                break;
        }
    }
}
