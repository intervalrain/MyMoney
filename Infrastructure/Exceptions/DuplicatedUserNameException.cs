namespace Infrastructure.Exceptions
{
	public class DuplicatedUserNameException : Exception
	{
		public DuplicatedUserNameException()
		{
		}

		public DuplicatedUserNameException(string userName)
			: base($"{userName} is duplicated")
		{

		}
    }
}

