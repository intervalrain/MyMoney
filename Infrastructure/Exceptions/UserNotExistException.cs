namespace Infrastructure.Exceptions
{
	public class UserNotExistException : Exception
	{
		public UserNotExistException(string UserName)
			: base($"{UserName} does not exist.")
		{
		}
	}
}

