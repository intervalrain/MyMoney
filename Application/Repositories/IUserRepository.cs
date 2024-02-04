using Domain;

namespace Applications.Repositories
{
	public interface IUserRepository
	{
		List<User> GetAllUsers();

		User FindUserById(int userId);

		void Update(User user);

        int Create(string userName);

        void Delete(User user);
    }
}

