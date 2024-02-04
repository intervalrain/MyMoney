using Domain;

namespace Applications.Repositories
{
	public interface IUserRepository
	{
		List<User> GetAllUsers();

		User FindUserById(string userId);

		void Update(User user);

        void Create(User user);

        void Delete(User user);
    }
}

