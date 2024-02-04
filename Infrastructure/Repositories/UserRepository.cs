using System.Data;
using Applications.Repositories;
using Infrastructure.Entities;
using Infrastructure.Common;
using Infrastructure.Exceptions;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserDbContext userDbContext)
            : base(userDbContext)
        {
        }

        public List<Domain.User> GetAllUsers()
        {
            var users = GetAll();
            return users.Select(x => new Domain.User(x.UserId, x.UserName)).ToList();
        }

        public Domain.User FindUserById(int userId)
        {
            var user = Find(new { UserId = userId });
            return new Domain.User(user.UserId, user.UserName);
        }

        public void Update(Domain.User user)
        {
            var dtoUser = Find(new { user.UserId });
            if (dtoUser is null)
            {
                throw new UserNotExistException(user.UserName);
            }
            Update(new User
            {
                UserId = user.UserId,
                UserName = user.UserName
            });
        }

        public int Create(string userName)
        {
            var users = GetAll();
            if (users.Any(u => u.UserName == userName))
            {
                throw new DuplicatedUserNameException(userName);
            }
            var count = GetSerialNumber(users);
            Insert(new User
            {
                UserId = count,
                UserName = userName
            });
            return count;
        }

        public void Delete(Domain.User user)
        {
            base.Delete(user.UserId);
        }

        private int GetSerialNumber(IEnumerable<User> users)
        {
            int curr = 1;
            var hashSet = new HashSet<int>();
            foreach (var user in users)
            {
                hashSet.Add(user.UserId);
                while (hashSet.Contains(curr))
                {
                    curr++;
                }
            }
            return curr;
        }
    }
}

