using SharedLibrary.ResponseArgs;

namespace SharedLibrary
{
	public interface IMyMoneyResponse
	{
		Task UserSelectedEvent(UserSelectedEventArgs e);
	}
}

