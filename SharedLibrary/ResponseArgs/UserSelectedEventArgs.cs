namespace SharedLibrary.ResponseArgs
{
	public class UserSelectedEventArgs : EventArgs
	{
		public required int UserId { get; init; }
		public required string UserName { get; init; }
	}
}

